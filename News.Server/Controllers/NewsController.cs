using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Server.Entity;
using News.Server.Repositories.Contracts;
using System.IO;
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
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository newsRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public NewsController(INewsRepository newsRepository,
                            IWebHostEnvironment webHostEnvironment)
        {
            this.newsRepository = newsRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        #region GET Methods

        [HttpGet("GetAllNews/{page_number:int}")]
        public async Task<ActionResult<IEnumerable<ArticleWithHoleInfoEntity>>> GetAllNews(int page_number)
        {
            try
            {
                var news = await this.newsRepository.GetAllNews(page_number);

                if (news == null)
                    return NotFound();
                else
                    return Ok(news);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpGet("GetNewsByCategoryId/{page_number:int}, {category_id:int}")]
        public async Task<ActionResult<IEnumerable<NewsEntity>>> GetNewsByCategoryId(int page_number, int category_id)
        {
            try
            {
                var news = await this.newsRepository.GetNewsByCategoryId(page_number, category_id);

                if (news == null)
                    return NotFound();
                else
                    return Ok(news);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpGet("GetNewsByAuthorId/{page_number:int}, {author_id:int}")]
        public async Task<ActionResult<IEnumerable<ArticleWithAuthorEntity>>> GetNewsByAuthorId(int page_number, int author_id)
        {
            try
            {
                var items = await this.newsRepository.GetNewsByAuthorId(page_number, author_id);

                if (items == null)
                    return NotFound();
                else
                    return Ok(items);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
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

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetAllCategories()
        {
            try
            {
                var items = await this.newsRepository.GetAllCategories();

                if (items == null) 
                    return NotFound();
                else 
                    return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetAllPermissions")]
        public async Task<ActionResult<IEnumerable<NewsPermissionEntity>>> GetAllPermissions()
        {
            try
            {
                var items = await this.newsRepository.GetAllPermissions();

                if (items == null) 
                    return NotFound();
                else 
                    return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
            }
        }

        [HttpGet]
        [Route("GetTopArticles")]
        public async Task<ActionResult<IEnumerable<ArticleTopViewEntity>>> GetTopArticles()
        {
            try
            {
                var items = await this.newsRepository.GetTopArticles();

                if (items == null) 
                    return NotFound();
                else 
                    return Ok(items);
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



/*
 * The API method was written to bring all the information of the news. Descriptions were written for the created classes. By Gokhan
 */