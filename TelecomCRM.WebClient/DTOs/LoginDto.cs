using System.ComponentModel.DataAnnotations;

namespace TelecomCRM.WebClient.DTOs;
public class LoginDTO
{
    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль обязателен")]
    [MinLength(6, ErrorMessage = "Минимум 6 символов")]
    public string Password { get; set; }
}
