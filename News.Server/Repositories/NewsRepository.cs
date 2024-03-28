using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using News.Server.Data;
using News.Server.Entity;
using News.Server.Repositories.Contracts;

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
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext newsDbContext;

        public NewsRepository(NewsDbContext newsDbContext) 
        {
            this.newsDbContext = newsDbContext;
        }

        public async Task<IEnumerable<ArticleWithHoleInfoEntity>> GetAllNews(int page_number)
        {
            // var items = await this.newsDbContext.Articles.ToListAsync();
            // return items;

            var items = await newsDbContext.ArticleWithHoleInfos
                .FromSqlRaw("EXECUTE dbo.SP_Articles_Paging @page_number", 
                new SqlParameter("@page_number", page_number)).ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<ArticleWithHoleInfoEntity>();
            }
        }

        public async Task<IEnumerable<ArticleDetailsByIdEntity>> GetNewsById(int id)
        {
            var item = await newsDbContext.ArticleDetails
                .FromSqlRaw("EXECUTE dbo.SP_Article_Details_By_Id @article_id",
                new SqlParameter("@article_id", id))
                .ToListAsync();

            return item;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategories()
        {
            var items = await this.newsDbContext.Categories.ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<CategoryEntity>();
            }
        }

        public async Task<IEnumerable<NewsPermissionEntity>> GetAllPermissions()
        {
            var items = await this.newsDbContext.ArticlePermissions.ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<NewsPermissionEntity>();
            }
        }

        public async Task<IEnumerable<ArticleWithHoleInfoEntity>> GetNewsByCategoryId(int page_number, int category_id)
        {
            var items = await newsDbContext.ArticleWithHoleInfos
                .FromSqlRaw("EXECUTE dbo.SP_Articles_Paging_OrderBy_Category @page_number, @category_id ", 
                new SqlParameter("@page_number",  page_number),
                new SqlParameter("@category_id", category_id)).ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<ArticleWithHoleInfoEntity>();
            }
        }

        public async Task<IEnumerable<ArticleWithAuthorEntity>> GetNewsByAuthorId(int page_number, int author_id)
        {
            var items = await newsDbContext.ArticleWithAuthor
            .FromSqlRaw("EXEC dbo.SP_Articles_OrderBy_Author @page_number, @author_id",
            new SqlParameter("@page_number", page_number),
            new SqlParameter("@author_id", author_id)).ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<ArticleWithAuthorEntity>();
            }
        }

        public async Task<IEnumerable<ArticleTopViewEntity>> GetTopArticles()
        {
            var items = await newsDbContext.TopView
                .FromSqlRaw("EXEC dbo.SP_Top_Articles").ToListAsync();

            if (items != null && items.Any())
            {
                return items;
            }
            else
            {
                return Enumerable.Empty<ArticleTopViewEntity>();
            }
        }
    }
}
