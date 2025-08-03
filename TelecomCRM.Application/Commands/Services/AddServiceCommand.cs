using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.ResponseModels;

namespace TelecomCRM.Application.Commands.Services
{
    public class AddServiceCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}
