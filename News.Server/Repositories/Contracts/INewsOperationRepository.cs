using News.Server.Entity;

public interface INewsOperationRepository
{
    Task<NewsEntity> CreateArticle(NewsEntity newsEntity);
    Task<string> UpdateArticle(ArticleUpdateEntity articleUpdateEntity,
    int articleId, int categoryId, int permissionId, string articleTitle, string articleContent, string image_url);
    Task<IEnumerable<SearchArticlesEntity>> SearchArticles(int page_number, string search);
    Task<bool> UpdateArticleView(int article_id);
    Task<string> UpdateArticleForEditor(ArticleUpdateForEditor articleUpdateForEditor, int article_id, string article_show, int status_id);
}