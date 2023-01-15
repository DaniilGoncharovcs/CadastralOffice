namespace IdentityWebApi.DTO;

public class UserForRegistrationDto
{
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Пароль обязателен")]
    public string Password { get; set; }
    [Compare("Password",ErrorMessage = "Введенные пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}