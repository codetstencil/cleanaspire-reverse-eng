// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanAspire.Application.Features.Customers.DTOs;

namespace CleanAspire.Application.Features.SalesOrders.DTOs;

public class SalesOrderDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the sales order.
    /// </summary>

    public string? Id { get; set; }

    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier associated with the sales order.
    /// </summary>
    public string? CustomerId { get; set; }

    public CustomerDto? Customer { get; set; }
    /// <summary>
    /// Gets or sets the total amount of the sales order.
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// Gets or sets the date when the sales order was created.
    /// </summary>

    /// 
    public string? Status { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public int Quantity { get; set; }

    public DateTime CreatedDate { get; set; }
}
