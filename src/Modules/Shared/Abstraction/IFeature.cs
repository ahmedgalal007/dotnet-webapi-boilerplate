﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Modules.Shared.Abstraction;

/// <summary>
/// Represents a feature.
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.Members)]
public interface IFeature
{
    /// <summary>
    /// Gets the module that this feature belongs to.
    /// </summary>
    IModule Module { get; }

    /// <summary>
    /// Configures the feature.
    /// </summary>
    void Configure();

    /// <summary>
    /// Configures the hosted services.
    /// </summary>
    void ConfigureHostedServices();

    /// <summary>
    /// Applies the feature by adding services to the service collection.
    /// </summary>
    void Apply();

}
