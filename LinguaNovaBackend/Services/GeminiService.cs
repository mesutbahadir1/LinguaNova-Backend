using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LinguaNova.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        // Updated API endpoint to use the stable v1 API version with the correct format
        private readonly string _apiEndpoint = "https://generativelanguage.googleapis.com/v1/models/gemini-2.0-flash:generateContent";
        private readonly ILogger<GeminiService> _logger;

        // Dictionary to map model names to their endpoints
        private readonly Dictionary<string, string> _modelEndpoints = new Dictionary<string, string>
        {
            { "gemini-1.5-pro", "https://generativelanguage.googleapis.com/v1/models/gemini-1.5-pro:generateContent" },
            { "gemini-1.5-flash", "https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent" },
            { "gemini-1.0-pro", "https://generativelanguage.googleapis.com/v1/models/gemini-1.0-pro:generateContent" },
            { "gemini-2.0-flash", "https://generativelanguage.googleapis.com/v1/models/gemini-2.0-flash:generateContent" }
        };

        public GeminiService(IConfiguration configuration, HttpClient httpClient, ILogger<GeminiService> logger)
        {
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "Gemini API key is not configured");
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("GeminiService initialized with endpoint: {Endpoint}", _apiEndpoint);
            
            // Try to get custom model from configuration
            var configuredModel = configuration["Gemini:Model"];
            if (!string.IsNullOrEmpty(configuredModel) && _modelEndpoints.ContainsKey(configuredModel))
            {
                _apiEndpoint = _modelEndpoints[configuredModel];
                _logger.LogInformation("Using configured model endpoint: {Endpoint}", _apiEndpoint);
            }
        }

        // Added method to list available models to help with debugging
        public async Task<string> ListAvailableModels()
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"https://generativelanguage.googleapis.com/v1/models?key={_apiKey}");
                
                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Available models: {responseString}");
                
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing available models");
                return "Error listing models";
            }
        }

        public async Task<string> GetLanguageLearningResponse(string prompt, string targetLanguage)
        {
            try
            {
                var request = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = $"You are a language learning assistant for {targetLanguage}. {prompt}" }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        topK = 40,
                        topP = 0.95,
                        maxOutputTokens = 1024
                    }
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                _logger.LogDebug($"Sending request to Gemini API: {_apiEndpoint}?key=[REDACTED]");
                _logger.LogDebug($"Request body: {json}");
                
                var response = await _httpClient.PostAsync(
                    $"{_apiEndpoint}?key={_apiKey}", content);
                
                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"Gemini API raw response: {responseString}");
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Gemini API error: {response.StatusCode} - {responseString}");
                    return $"Sorry, an error occurred while processing your request. Status: {response.StatusCode}";
                }
                
                _logger.LogInformation($"Successful response content: {responseString}");
                
                try 
                {
                    var responseObject = JsonSerializer.Deserialize<GeminiResponse>(responseString,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Büyük-küçük harf duyarsız hale getir
                        });
                    
                    if (responseObject?.Candidates == null)
                    {
                        _logger.LogWarning("No candidates property found in response");
                        _logger.LogWarning($"Response keys: {string.Join(", ", JsonDocument.Parse(responseString).RootElement.EnumerateObject().Select(p => p.Name))}");
                        return "API response did not contain candidates property";
                    }
                    
                    if (responseObject.Candidates.Length == 0)
                    {
                        _logger.LogWarning("Candidates array is empty");
                        return "API response contained empty candidates array";
                    }
                    
                    var candidate = responseObject.Candidates[0];
                    if (candidate.Content == null)
                    {
                        _logger.LogWarning("First candidate has no content");
                        return "API response candidate contains no content";
                    }
                    
                    if (candidate.Content.Parts == null || !candidate.Content.Parts.Any())
                    {
                        _logger.LogWarning("Candidate content has no parts");
                        return "API response content contains no parts";
                    }
                    
                    var text = candidate.Content.Parts[0].Text;
                    if (string.IsNullOrEmpty(text))
                    {
                        _logger.LogWarning("No text found in first part");
                        return "API response did not contain text in content parts";
                    }
                    
                    return text;
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error deserializing response");
                    // Log full response for debugging
                    _logger.LogError($"Response that couldn't be deserialized: {responseString}");
                    return "Error processing API response format";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Gemini API");
                return "Sorry, an error occurred while processing your request.";
            }
        }
        
        // Method for general chatbot conversation
        public async Task<string> GetChatbotResponse(string userMessage)
        {
            return await GetLanguageLearningResponse(
                $"Respond as a helpful English language tutor to this message: {userMessage}", 
                "English");
        }
        
        // Method for grammar correction
        public async Task<string> GetGrammarCorrection(string text)
        {
            return await GetLanguageLearningResponse(
                $"Correct any grammar mistakes in this text and explain the corrections: {text}", 
                "English");
        }
        
        // Method for vocabulary explanation
        public async Task<string> GetVocabularyExplanation(string word)
        {
            return await GetLanguageLearningResponse(
                $"Explain the meaning, usage, and provide examples for this word: {word}", 
                "English");
        }
    }

    // Updated response classes for deserialization based on actual API response
    public class GeminiResponse
    {
        public Candidate[] Candidates { get; set; }
        public PromptFeedback PromptFeedback { get; set; }
        public UsageMetadata UsageMetadata { get; set; }
        public string ModelVersion { get; set; }
    }

    public class UsageMetadata
    {
        public int PromptTokenCount { get; set; }
        public int CandidatesTokenCount { get; set; }
        public int TotalTokenCount { get; set; }
    }

    public class PromptFeedback
    {
        public BlockedReason[] BlockedReasons { get; set; }
        public SafetyRating[] SafetyRatings { get; set; }
    }

    public class BlockedReason
    {
        public string Reason { get; set; }
    }

    public class SafetyRating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
    }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public SafetyRating[] SafetyRatings { get; set; }
        public double AvgLogprobs { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }
}