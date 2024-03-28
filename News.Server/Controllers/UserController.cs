using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Server.Entity;
using News.Server.Repositories.Contracts;

namespace News.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers()
        {
            try
            {
                var result = await this.userRepository.GetAllUsers();

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

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<RoleEntity>>> GetAllRoles()
        {
            try
            {
                var result = await this.userRepository.GetAllRoles();

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

        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<AuthorEntity>>> GetAllAuthors()
        {
            try
            {
                var result = await this.userRepository.GetAllAuthors();

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

        [HttpGet("GetAuthorById/{author_id}")]
        public async Task<ActionResult<IEnumerable<AuthorByIdEntity>>> GetAuthorById(int author_id)
        {
            try
            {
                var result = await this.userRepository.GetAuthorById(author_id);

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

        [HttpGet]
        [Route("GetAllEditors")]
        public async Task<ActionResult<IEnumerable<EditorEntity>>> GetAllEditors()
        {
            try
            {
                var result = await this.userRepository.GetAllEditors();

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

        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<ActionResult<IEnumerable<AdminEntity>>> GetAllAdmins()
        {
            try
            {
                var result = await this.userRepository.GetAllAdmins();

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

        [HttpGet("GetAllUserStatuses")]
        public async Task<ActionResult<IEnumerable<UserStatusEntity>>> GetAllUserStatuses()
        {
            try
            {
                var result = await this.userRepository.GetAllUserStatuses();

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
    }
}
