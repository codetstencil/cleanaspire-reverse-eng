// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CleanAspire.Domain.Common;

namespace CleanAspire.Domain.Entities;
public class Customer : BaseAuditableEntity, IAuditTrial
{
    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string Address { get; set; } = string.Empty;


    public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();


    public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();


}
