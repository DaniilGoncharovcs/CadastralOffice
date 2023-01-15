namespace IdentityWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto dto)
    {
        if (dto is null || !ModelState.IsValid)
            return BadRequest();

        var user = new User
        {
            UserName = dto.UserName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description);

            return BadRequest(new RegistrationResposeDto { Errors = errors });
        }

        await _userManager.AddToRoleAsync(user, "manager");

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new AuthResposeDto { ErrorMessage = "Неправильный логин или пароль"});

        var signingCredentials = _tokenService.GetSigningCredentials();
        var claims = await _tokenService.GetClaims(user);
        var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        user.RefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _userManager.UpdateAsync(user);

        return Ok(new AuthResposeDto
        {
            IsAuthSuccessful = true,
            Token = token,
            RefreshToken = user.RefreshToken
        });
    }
}