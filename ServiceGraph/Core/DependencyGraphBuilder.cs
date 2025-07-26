using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickGraph;
using QuickGraph.Graphviz;

namespace ServiceGraph.Core;

internal class DependencyGraphBuilder
{
    private readonly IServiceCollection _serviceCollection;
    private readonly ServiceGraphOption? _graphOption;
    private readonly ILogger? _logger;
    private Dictionary<Type, List<Type>>? _cachedDependencies;

    public DependencyGraphBuilder(IServiceCollection serviceCollection, ServiceGraphOption? graphOption, ILogger? logger = null)
    {
        _serviceCollection = serviceCollection;
        _graphOption = graphOption;
        _logger = logger;
    }
    
    public GraphvizAlgorithm<Type, Edge<Type>> BuildGraph()
    {
        try
        {
            Dictionary<Type, List<Type>> dependencies = ResolveDependencies(_serviceCollection, _graphOption);
            
            var graph = new AdjacencyGraph<Type, Edge<Type>>();

            foreach (KeyValuePair<Type,List<Type>> dependency in dependencies)
            {
                Type serviceType = dependency.Key;
                graph.AddVertex(serviceType);

                foreach (Type dependencyType in dependency.Value)
                {
                    graph.AddVerticesAndEdge(new Edge<Type>(serviceType, dependencyType));
                }
            }
            
            var graphviz = new GraphvizAlgorithm<Type, Edge<Type>>(graph);
            
            graphviz.FormatVertex += (sender, args) =>
            {
                args.VertexFormatter.Label = args.Vertex.FullName;
            };

            graphviz.FormatEdge += (sender, args) =>
            {
                args.EdgeFormatter.Label.Value = string.Empty;
            };

            _logger?.LogInformation("Successfully built dependency graph with {ServiceCount} services", dependencies.Count);
            return graphviz;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error building dependency graph");
            throw;
        }
    }
    
    private Dictionary<Type, List<Type>> ResolveDependencies(IServiceCollection services, ServiceGraphOption? graphOption)
    {
        // Use cached dependencies if available
        if (_cachedDependencies != null)
        {
            return _cachedDependencies;
        }

        var dependencies = new Dictionary<Type, List<Type>>();

        foreach (ServiceDescriptor serviceDescriptor in services)
        {
            Type serviceType = serviceDescriptor.ServiceType;
            Type? implementationType = serviceDescriptor.ImplementationType;

            if (implementationType != null && (graphOption?.Namespaces == null 
                                               || IsCustomNamespace(implementationType, graphOption.Namespaces)))
            {
                try
                {
                    ConstructorInfo? ctor = implementationType.GetConstructors().FirstOrDefault();
                    if (ctor != null)
                    {
                        List<Type> parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).ToList();
                        dependencies[serviceType] = parameterTypes;
                        
                        if (graphOption?.EnableDetailedLogging == true)
                        {
                            _logger?.LogDebug("Service {ServiceType} depends on {DependencyCount} services", 
                                serviceType.FullName, parameterTypes.Count);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "Failed to resolve dependencies for service {ServiceType}", serviceType.FullName);
                }
            }
        }

        // Cache the dependencies
        _cachedDependencies = dependencies;
        return dependencies;
    }
    
    private bool IsCustomNamespace(Type type, string[] customNamespaces)
    {
        return customNamespaces.Contains("*") 
               || customNamespaces
                   .Any(ns => type.Namespace != null && type.Namespace.StartsWith(ns));
    }
}