using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using News.Server.Data;
using News.Server.Entity;
using News.Server.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// summary
/// 
/// This class communicates with the database upon request from the API and brings the requested data. 
/// It does this through the "NewsDbContext" class. NewsDbContext class creates the database connection 
/// with the connection string received from "appsettings". The desired data is fetched with Linq queries.
/// 
/// Bu sınıf API' den gelen istek üzerine veritabanı ile iletişime geçerek istenilen verileri getirir. 
/// Bu işlemi "NewsDbContext" sınıfı aracılığı ile yapar. NewsDbContext sınıfı "appsettings" den aldığı 
/// connection string ile veri tabanı bağlantısını oluşturur. Linq sorguları ile istenilen veriler getirilmiş olur.
/// 
/// summary

namespace News.Server.Repositories
{
    public class NewsOperationRepository :INewsOperationRepository
    {
        private readonly NewsDbContext newsDbContext;

        public NewsOperationRepository(NewsDbContext newsDbContext) 
        {
            this.newsDbContext = newsDbContext;
        }

        public async Task<NewsEntity> CreateArticle(NewsEntity newsEntity)
        {
            if (await ArticleExists(newsEntity.Id) == false)
            {
                var items = await (from article in this.newsDbContext.Articles
                                where article.Id == newsEntity.Id
                                select article).SingleOrDefaultAsync();

                if (items == null)
                {
                    var result = await this.newsDbContext.Articles.AddAsync(newsEntity);
                    await this.newsDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<IEnumerable<SearchArticlesEntity>> SearchArticles(int page_number, string search)
        {
            var items = await newsDbContext.SearchArticles
                .FromSqlRaw("EXECUTE dbo.SP_Search_Articles @page_number, @search", 
                new SqlParameter("@page_number", page_number),
                new SqlParameter("@search", search)).ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<SearchArticlesEntity>();
            }
        }

        public async Task<string> UpdateArticle(ArticleUpdateEntity articleUpdateEntity,
        int articleId, int categoryId, int permissionId, string articleTitle, string articleContent, string image_url)
        {
            string Message = "";
            try
            {
                if (await ArticleExists(articleUpdateEntity.ArticleID) == false)
                {
                    var items = await newsDbContext.UpdateArticle
                        .FromSqlRaw("EXECUTE dbo.SP_Update_Article @article_id, @category_id, @permission_id, @article_title, @article_content, @image_url",
                        new SqlParameter("@article_id", articleId),
                        new SqlParameter("@category_id", categoryId),
                        new SqlParameter("@permission_id", permissionId),
                        new SqlParameter("@article_title", articleTitle),
                        new SqlParameter("@article_content", articleContent),
                        new SqlParameter("@image_url", image_url)).ToListAsync();

                    if (items != null || items.Count < 0)
                        Message = "İşlem BAŞARILI!";
                }
            }
            catch (System.Exception ex)
            {
                Message = "HATA MESAJI:\n" + ex.Message;
            }
            
            return Message;
        }

        public async Task<string> UpdateArticleForEditor(ArticleUpdateForEditor articleUpdateForEditor, int article_id, string article_show, int status_id)
        {
            string Message = "İşlem başarılı!";

            try
            {
                if (await ArticleExists(articleUpdateForEditor.ArticleID) == false)
                {
                    var items = await newsDbContext.UpdateForEditor
                        .FromSqlRaw("EXEC dbo.SP_Update_For_Editor @article_id, @article_show, @status_id",
                        new SqlParameter("@article_id", article_id),
                        new SqlParameter("@article_show", article_show),
                        new SqlParameter("@status_id", status_id)).ToListAsync();
                }
            }
            catch (System.Exception ex)
            {
                Message = "HATA MESAJI:\n" + ex.Message;
            }
            
            return Message;
        }

        public async Task<bool> UpdateArticleView(int article_id)
        {
            bool result = true;
            
            var items = await newsDbContext.UpdateArticleView
                .FromSqlRaw("EXEC dbo.SP_Update_Article_View @article_id",
                new SqlParameter("@article_id", article_id)).ToListAsync();

            if (items.Count < 0)
                result = false;
            
            return result;
        }

        private async Task<bool> ArticleExists(int articleID)
        {
            return await this.newsDbContext.Articles.AnyAsync(a => a.Id == articleID);
        }
    }
}
