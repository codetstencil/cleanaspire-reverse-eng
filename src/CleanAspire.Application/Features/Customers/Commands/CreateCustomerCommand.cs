// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.Customers.DTOs;
using CleanAspire.Application.Features.Customers.EventHandlers;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Customers.Commands;
public record CreateCustomerCommand(string Name, string PhoneNumber, string Email, string Address) : IFusionCacheRefreshRequest<CustomerDto>, IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "customers" };
}


public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    readonly IApplicationDbContext _context;
    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address

        };
        customer.AddDomainEvent(new CustomerCreatedEvent(customer));
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return new CustomerDto()
        {
            Id = customer.Id,
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            Address = customer.Address,
            Created = customer.Created,
        };
    }
}
