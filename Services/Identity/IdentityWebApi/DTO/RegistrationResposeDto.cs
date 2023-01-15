namespace IdentityWebApi.DTO;

public class RegistrationResposeDto
{
    public bool IsSuccessfulRegistration { get; set; }
    public IEnumerable<string> Errors { get; set; }
}