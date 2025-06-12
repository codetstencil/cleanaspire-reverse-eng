// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.SalesOrders.Commands;

namespace CleanAspire.Application.Features.SalesOrders.Validators;
public class UpdateSalesOrderCommandValidator : AbstractValidator<UpdateSalesOrderCommand>
{
    public UpdateSalesOrderCommandValidator()
    {
        // Validate Sales Order ID (required)
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Sales Order ID is required.");
        RuleFor(command => command.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(command => command.OrderDate)
            .NotEmpty().WithMessage("Order date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");
        RuleFor(command => command.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
