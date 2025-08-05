using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application.DTOs
{
    public class SubscriptionDTO
    {
        public Customer Customer { get; set; }
        public Service Service { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
