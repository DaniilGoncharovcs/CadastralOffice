using Microsoft.EntityFrameworkCore.Query.Internal;

namespace IdentityWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public TokenController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
    {
        if (dto is null)
            return BadRequest(new AuthResposeDto
            {
                ErrorMessage = "Invalid client request",
                IsAuthSuccessful = false
            });

        var principal = _tokenService.GetPrincipalFromExpiredToken(dto.Token);
        var username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);
        if(user == null || user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest(new AuthResposeDto
            {
                ErrorMessage = "Invalid client request",
                IsAuthSuccessful = false
            });

        var signingCredentials = _tokenService.GetSigningCredentials();
        var claims = await _tokenService.GetClaims(user);
        var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        user.RefreshToken = _tokenService.GenerateRefreshToken();

        await _userManager.UpdateAsync(user);

        return Ok(new AuthResposeDto
        {
            IsAuthSuccessful = true,
            Token = token,
            RefreshToken = user.RefreshToken,
        });
    }
}