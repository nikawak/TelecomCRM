using System.ComponentModel.DataAnnotations;

namespace TelecomCRM.WebClient.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email обязателен")]
        [MaxLength(70, ErrorMessage = "Максимальная длина 70 символов")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(150, ErrorMessage = "Максимальная длина 150 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ0-9\s\-,\.\/№#]+$", ErrorMessage = "Адрес содержит недопустимые символы")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Минимум 6 символов")]
        public string Password { get; set; }
    }
}
