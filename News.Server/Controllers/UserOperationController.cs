using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Server.Entity;
using News.Server.Repositories.Contracts;
using static System.Net.Mime.MediaTypeNames;

[Route("api/[controller]")]
[ApiController]
public class UserOperationController : ControllerBase
{
    private readonly IUserOperationRepository userOperationRepository;

    public UserOperationController(IUserOperationRepository userOperationRepository)
    {
        this.userOperationRepository = userOperationRepository;
    }
    #region POST Methods

    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<string>> CreateUser([FromBody] UserEntity userEntity)
    {
        try
        {
            if (userEntity == null)
                return BadRequest();

            var result = await this.userOperationRepository.CreateUser(userEntity);

             if (result == null)
                    return NotFound();
                else
                    return Ok(result);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
        }
    }

    [HttpPost]
    [Route("RegisterUser")]
    public async Task<ActionResult<string>> RegisterUser([FromBody] UserEntity userEntity)
    {
        try
        {
            if (userEntity == null)
                return BadRequest();

            var result = await this.userOperationRepository.RegisterUser(userEntity);

             if (result == null)
                    return NotFound();
                else
                    return Ok(result);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error retrieving data from the database" + "\nSystem Message: " + ex.ToString());
        }
    }

    #endregion

    [HttpPut("UpdateUserPassword/{userId}")]
    public async Task<IActionResult> UpdateUserPassword(int userId, [FromBody] UserPasswordEntity userPasswordEntity)
    {
        if (userPasswordEntity == null)
        {
            return BadRequest("Gönderilen nesne boş.");
        }

        var UserPassword = userPasswordEntity.UserPassword;

        try
        {
            var result = await userOperationRepository.UpdateUserPassword(userPasswordEntity, userId, UserPassword);

            if (result != null)
            {
                return Ok("Güncelleme Detayı: \n" + result);
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

    [HttpPut("UpdateUserStatus/{userId}")]
    public async Task<IActionResult> UpdateUserStatus(int userId, [FromBody] UserUpdateStatusEntity userUpdateStatusEntity)
    {
        if (userUpdateStatusEntity == null)
        {
            return BadRequest("Gönderilen Nesne Boş");
        }

        var StatusId = userUpdateStatusEntity.StatusId;

        try
        {
            var result = await userOperationRepository.UpdateUserStatus(userUpdateStatusEntity, userId, StatusId);
            
            if (result != null)
            {
                return Ok("Güncelleme Detayı: \n" + result);
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

    [HttpPut("UpdateUser/{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserEntity userEntity) //int article_id, int role_id, string user_name, string user_surname, string user_password, string user_email, int status_id, string user_pp_url
    {
        if (userEntity == null)
        {
            return BadRequest("Gönderilen Nesne Boş");
        }

        var RoleId = userEntity.RoleId;
        var UserName = userEntity.UserName;
        var UserSurname = userEntity.UserSurname;
        var UserPassword = userEntity.UserPassword;
        var UserEmail = userEntity.UserEmail;
        var StatusID = userEntity.StatusID;
        var UserPPUrl = userEntity.UserPPUrl;

        try
        {
            var result = await userOperationRepository.UpdateUser(userEntity, userId, RoleId, UserName, UserSurname, UserPassword, UserEmail, StatusID, UserPPUrl);
            
            if (result != null)
            {
                return Ok("Güncelleme Detayı: \n" + result);
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
}
