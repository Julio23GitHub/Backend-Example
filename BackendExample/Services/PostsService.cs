using System.Text.Json;

namespace BackendExample.Services
{
    public class PostsService : IPostsService
    {

        private HttpClient _httpClient;


        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<DTOs.PostDto>> Get()
        {
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);

            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var posts = JsonSerializer.Deserialize<IEnumerable<DTOs.PostDto>>(body, options);

            // Fix for CS8603: Ensure posts is not null before returning
            return posts ?? Enumerable.Empty<DTOs.PostDto>();
        }
    }

}
