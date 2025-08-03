using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application.DTOs
{
    public class CreateServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}
