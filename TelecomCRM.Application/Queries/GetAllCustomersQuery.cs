using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application.Queries
{
    public record GetAllCustomersQuery : IRequest<List<CustomerDTO>>;
}
