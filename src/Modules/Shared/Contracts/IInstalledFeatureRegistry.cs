﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soft.Square.Modules.Shared.Abstraction;

namespace Soft.Square.Modules.Shared.Contracts;

/// <summary>
/// Represents a registry of installed features.
/// </summary>
public interface IInstalledFeatureRegistry
{
    /// <summary>
    /// Adds a feature descriptor to the registry.
    /// </summary>
    /// <param name="descriptor">The feature descriptor.</param>
    void Add(FeatureDescriptor descriptor);

    /// <summary>
    /// Gets all installed features.
    /// </summary>
    /// <returns>All installed features.</returns>
    IEnumerable<FeatureDescriptor> List();

    /// <summary>
    /// Finds a feature descriptor by its full name.
    /// </summary>
    /// <param name="fullName">The full name of the feature.</param>
    /// <returns>The feature descriptor or null if not found.</returns>
    FeatureDescriptor? Find(string fullName);
}