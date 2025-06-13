// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAspire.Application.Features.Artists.DTOs;
public class ArtistDto
{
    public int ArtistId { get; set; }
    public string Name { get; set; } = string.Empty;
}
