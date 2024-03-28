using News.Server.Entity;

namespace News.Server.Repositories.Contracts
{
    public interface INewsRepository
    {
        Task<IEnumerable<ArticleWithHoleInfoEntity>> GetAllNews(int page_number);
        Task<IEnumerable<ArticleWithHoleInfoEntity>> GetNewsByCategoryId(int page_number, int category_id);
        Task<IEnumerable<ArticleDetailsByIdEntity>> GetNewsById(int id);
        Task<IEnumerable<CategoryEntity>> GetAllCategories();
        Task<IEnumerable<NewsPermissionEntity>> GetAllPermissions();
        Task<IEnumerable<ArticleWithAuthorEntity>> GetNewsByAuthorId(int page_number, int author_id);
        Task<IEnumerable<ArticleTopViewEntity>> GetTopArticles();
    }
}
