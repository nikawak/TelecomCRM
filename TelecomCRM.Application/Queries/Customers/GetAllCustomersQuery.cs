using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.ResponseModels;

namespace TelecomCRM.Application.Queries.Customers
{
    public record GetAllCustomersQuery : IRequest<Result<List<CustomerDTO>>>;
}
