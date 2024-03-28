using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Server.Repositories.Contracts;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private string signinKey = ""; //Your signinKey
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet("GetToken")]
    //public string GetToken(string userEmail, string userID, string authorID, string roleID)
    public string GetToken(string userID, string roleID)
    {
        var claims = new[]
        {
            //new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim("UserID", userID),
            //new Claim("AuthorID", authorID),
            new Claim("RoleID", roleID)
        };


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
        var credantials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: "https://www.haberto.com",
            audience: "https://www.haberto.com",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            notBefore: DateTime.Now,
            signingCredentials: credantials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }

    [HttpGet("ValidateToken")]
    public bool ValidateToken(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));

        try
        {
            JwtSecurityTokenHandler handler = new();
            handler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = true,
                ValidateAudience = true, 
                ValidAudience = "https://www.haberto.com",
                ValidateIssuer = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            //var claims = jwtToken.Claims.ToList();

            //Kullanıcının token bilgisini veri tabanında tut ve onu kontrol et.

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    [HttpGet("CheckedUser/{email}, {password}")]
    public async Task<ActionResult<IEnumerable<LoginEntity>>> CheckedUser(string email, string password)
    {
        try
        {
            var result = await this.userRepository.CheckedUser(email, password);

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
