using News.Server.Entity;

namespace News.Server.Services.Contracts
{
    public interface INewsService
    {
        Task<NewsEntity> GetItem(int id);
    }
}
