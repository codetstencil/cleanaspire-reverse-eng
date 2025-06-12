// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAspire.Application.Features.Customers.DTOs;
public class CustomerDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    public string Id { get; set; } = string.Empty;
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
    /// Gets or sets the date and time when the customer was created.
    /// </summary>


    public string Address { get; set; }

    public DateTime? Created { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the customer was last modified.
    /// </summary>
    public DateTime? LastModified { get; set; }
}
