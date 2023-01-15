namespace IdentityWebApi.DTO;

public class AuthResposeDto
{
    public bool IsAuthSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}