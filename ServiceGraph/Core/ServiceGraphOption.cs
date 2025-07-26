namespace ServiceGraph.Core;

public class ServiceGraphOption
{
    /// <summary>
    /// Namespaces to include while resolving dependencies
    /// If not null all other namespaces will be excluded from scan
    /// </summary>
    public string[]? Namespaces { get; set; }
    
    /// <summary>
    /// Whether to show circular dependency warnings in the UI
    /// </summary>
    public bool ShowCircularDependencyWarnings { get; set; } = true;
    
    /// <summary>
    /// Custom title for the service graph UI
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Whether to enable detailed logging
    /// </summary>
    public bool EnableDetailedLogging { get; set; } = false;
    
    /// <summary>
    /// Custom route prefix for the service graph UI
    /// </summary>
    public string RoutePrefix { get; set; } = "service-graph";
}
