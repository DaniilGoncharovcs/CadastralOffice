namespace IdentityWebApi.DTO;

public class UserForAuthenticationDto
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }
}