using Soft.Square.Modules.Shared.Abstraction;
using Soft.Square.Modules.Shared.Contracts;

namespace Soft.Square.Modules.Shared.Services;

/// <inheritdoc />
public class InstalledFeatureRegistry : IInstalledFeatureRegistry
{
    private readonly IDictionary<string, FeatureDescriptor> _descriptors = new Dictionary<string, FeatureDescriptor>();

    /// <inheritdoc />
    public void Add(FeatureDescriptor descriptor) => _descriptors[descriptor.FullName] = descriptor;

    /// <inheritdoc />
    public IEnumerable<FeatureDescriptor> List() => _descriptors.Values;

    /// <inheritdoc />
    public FeatureDescriptor? Find(string fullName) => _descriptors.TryGetValue(fullName, out var descriptor) ? descriptor : null;
}
