using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application.DTOs
{
    public class CreateSubscriptionDTO
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
