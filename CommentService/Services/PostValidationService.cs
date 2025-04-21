using System.Net.Http.Json;

namespace CommentService.Services
{
    public class PostValidationService : IPostValidationService
    {
        private readonly HttpClient _httpClient;

        public PostValidationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("PostService");
        }

        public async Task<bool> ValidatePostExistsAsync(Guid postId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"posts/exists/{postId}");
                if (!response.IsSuccessStatusCode)
                    return false;

                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch
            {
                // In a production app, log the exception
                return false;
            }
        }
    }
}