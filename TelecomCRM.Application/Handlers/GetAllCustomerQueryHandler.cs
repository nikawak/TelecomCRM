using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Queries;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers
{
    public class GetAllCustomerQueryHandler(TelecomDbContext _context) : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.FullName,
                }).ToListAsync(cancellationToken);
            return customers ?? [];
        }
    }
}
