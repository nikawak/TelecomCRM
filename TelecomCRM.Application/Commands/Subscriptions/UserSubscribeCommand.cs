using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.ResponseModels;

namespace TelecomCRM.Application.Commands.Subscriptions
{
    public class UserSubscribeCommand : IRequest<Result<Unit>>
    {
        public int ServiceId { get; set; }
        public string Token { get; set; }
    }
}
