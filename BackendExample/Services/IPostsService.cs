namespace BackendExample.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<DTOs.PostDto>> Get();
    }
}
