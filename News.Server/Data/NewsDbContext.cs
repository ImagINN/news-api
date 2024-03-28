using Microsoft.EntityFrameworkCore;
using News.Server.Entity;

namespace News.Server.Data
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<NewsEntity> Articles { get; set; }
        public DbSet<ArticleWithHoleInfoEntity> ArticleWithHoleInfos { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<EditorEntity> Editors { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<NewsPermissionEntity> ArticlePermissions { get; set; }
        public DbSet<ArticleDetailsByIdEntity> ArticleDetails { get; set; }
        public DbSet<ArticleUpdateEntity> UpdateArticle { get; set;}
        public DbSet<ArticleWithAuthorEntity> ArticleWithAuthor { get; set; }
        public DbSet<SearchArticlesEntity> SearchArticles { get; set; }
        public DbSet<UserStatusEntity> UserStatus { get; set; }
        public DbSet<ArticleTopViewEntity> TopView { get; set; }
        public DbSet<ArticleUpdateForEditor> UpdateForEditor { get; set; }
        public DbSet<UserPasswordEntity> UpdateUserPassword { get; set; }
        public DbSet<UserUpdateStatusEntity> UpdateUserStatus { get; set; }
        public DbSet<UpdateArticleView> UpdateArticleView { get; set; }
        public DbSet<AuthorByIdEntity> AuthorById { get; set; }
        public DbSet<LoginEntity> Login { get; set; }
    }
}
