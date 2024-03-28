using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Server.Entity;
using News.Server.Repositories.Contracts;
using static System.Net.Mime.MediaTypeNames;

/// summary
/// 
/// This class is the class where the requests coming from the client side of the project are interpreted.
/// 
/// Bu sınıf projenin client tarafından gelecek isteklerin anlamlandırıldığı sınıftır. 
/// 
/// summary

namespace News.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsOperationController : ControllerBase
    {
        private readonly INewsOperationRepository newsOperationRepository;
        private readonly INewsRepository newsRepository;

        public NewsOperationController(INewsOperationRepository newsOperationRepository,
                                    INewsRepository newsRepository)
        {
            this.newsOperationRepository = newsOperationRepository;
            this.newsRepository = newsRepository;
        }

        [HttpGet]
        [Route("GetNewsById/{id:int}")]
        public async Task<ActionResult<IEnumerable<ArticleDetailsByIdEntity>>> GetNewsById(int id)
        {
            try
            {
                var newsById = await this.newsRepository.GetNewsById(id);

                if (newsById == null)
                    return NotFound();
                else
                    return Ok(newsById);                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpGet("SearchArticles/{page_number:int}, {search}")]
        public async Task<ActionResult<IEnumerable<SearchArticlesEntity>>> SearchArticles(int page_number, string search)
        {
            try
            {
                var result = await this.newsOperationRepository.SearchArticles(page_number, search);

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpPut("UpdateArticle/{articleId}")]
        public async Task<IActionResult> UpdateArticle(int articleId, [FromBody] ArticleUpdateEntity articleUpdateEntity)
        {
            if (articleUpdateEntity == null)
            {
                return BadRequest();
            }

            var categoryId = articleUpdateEntity.CategoryID;
            var permissionId = articleUpdateEntity.PermissionID;
            var articleTitle = articleUpdateEntity.ArticleTitle;
            var articleContent = articleUpdateEntity.ArticleContent;
            var imageUrl = articleUpdateEntity.ImageUrl;

            try
            {
                var updatedArticle = await newsOperationRepository.UpdateArticle(articleUpdateEntity, articleId, categoryId, permissionId, articleTitle, articleContent, imageUrl);

                if (updatedArticle != null)
                {
                    return Ok(updatedArticle);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateArticleForEditor/{articleId}")]
        public async Task<IActionResult> UpdateArticleForEditor(int articleId, [FromBody] ArticleUpdateForEditor articleUpdateForEditor)
        {
            if (articleUpdateForEditor == null)
            {
                return BadRequest("Gönderilen nesne boş!");
            }

            var articleShow = articleUpdateForEditor.ArticleShow;
            var statusId = articleUpdateForEditor.StatusID;

            try
            {
                var result = await newsOperationRepository.UpdateArticleForEditor(articleUpdateForEditor, articleId, articleShow, statusId);
                
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateArticleView/{article_id}")]
        public async Task<IActionResult> UpdateArticleView(int article_id)
        {
            try
            {
                var result = await newsOperationRepository.UpdateArticleView(article_id);
                
                if (result == true)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("false döndü.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region POST Methods

        [HttpPost]
        [Route("CreateArticle")]
        public async Task<ActionResult<NewsEntity>> CreateArticle([FromBody] NewsEntity newsEntity)
        {
            try
            {
                if (newsEntity == null)
                    return BadRequest("News data is null");

                var createdNews = await this.newsOperationRepository.CreateArticle(newsEntity);

                return CreatedAtAction(nameof(GetNewsById), new { id = createdNews.Id }, createdNews);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        #endregion
    }
}