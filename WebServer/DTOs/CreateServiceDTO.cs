using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TelecomCRM.WebServer.DTOs
{
    public class CreateServiceDTO
    {
        [Required (ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Название должно быть от 3 до 100 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(500,
            ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }
        [Range(0, 100000, ErrorMessage = "Плата должна быть от 0 до 100 000")]
        public decimal MonthlyFee { get; set; } = 0;
    }
}
