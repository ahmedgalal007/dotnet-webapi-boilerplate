﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Modules.Shared.Features;

/// <summary>
/// Specifies that the feature is enabled automatically when the specified feature is enabled.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependencyOfAttribute : Attribute
{
    /// <inheritdoc />
    public DependencyOfAttribute(Type type)
    {
        Type = type;
    }

    /// <summary>
    /// The type of the feature this feature is a dependency of.
    /// </summary>
    public Type Type { get; set; }
}