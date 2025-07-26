# ServiceGraph

ServiceGraph is a C# library designed to visualize and manage the dependencies between services in a microservices architecture. This tool helps developers and system administrators understand and maintain the complex web of interdependencies in modern software systems.

## Features

- **Dependency Visualization**: Displays a graphical representation of service dependencies.
- **Circular Dependency Detection**: Identify and highlight circular dependencies to prevent potential issues in your microservices architecture.
- **Interactive Graph**: Nodes and edges are clickable, providing detailed information about each service and its dependencies.
- **Scalability**: Efficiently handles large numbers of services and dependencies.
- **Customizable Views**: Filter and organize the graph to focus on specific aspects of the system.
- **Performance Optimized**: Includes caching and performance improvements for large dependency graphs.
- **Detailed Logging**: Optional detailed logging for debugging and monitoring.
- **Customizable UI**: Configurable title, route prefix, and circular dependency warnings.

## Installation

To set up and run ServiceGraph locally, follow these steps:

1. **Clone the repository**:
    ```bash
    git clone https://github.com/nakkayev1707/ServiceGraph.git
    cd ServiceGraph
    ```

2. **Build the project**:
    ```bash
    dotnet build
    ```

3. **Run tests**:
    ```bash
    dotnet test
    ```

## Usage

### Basic Usage

```csharp
// Add your services to the DI container
builder.Services.AddScoped<IYourService, YourService>();
builder.Services.AddScoped<IAnotherService, AnotherService>();

// Enable ServiceGraph UI in development
if (app.Environment.IsDevelopment())
{
    app.UseServiceGraphUI(builder.Services, new ServiceGraphOption
    {
        Namespaces = new[] { "*" } // Include all namespaces
    });
}
```

### Advanced Configuration

```csharp
app.UseServiceGraphUI(builder.Services, new ServiceGraphOption
{
    // Filter by specific namespaces
    Namespaces = new[] { "Your.Custom.Namespaces", "Another.Namespace" },
    
    // Customize the UI
    Title = "My Service Dependencies",
    RoutePrefix = "my-service-graph",
    
    // Control features
    ShowCircularDependencyWarnings = true,
    EnableDetailedLogging = true
});
```

### Namespace Filtering

```csharp
// Include only specific namespaces
app.UseServiceGraphUI(builder.Services, new ServiceGraphOption
{
    Namespaces = new[] { "Your.Custom.Namespaces" }
});

// Include all namespaces (default)
app.UseServiceGraphUI(builder.Services, new ServiceGraphOption
{
    Namespaces = new[] { "*" }
});

// Include multiple namespaces
app.UseServiceGraphUI(builder.Services, new ServiceGraphOption
{
    Namespaces = new[] { "Your.Services", "Your.Infrastructure", "Your.Domain" }
});
```

## UI Features

The ServiceGraph UI provides:

- **Interactive Graph Visualization**: Click on nodes to see detailed information
- **Circular Dependency Detection**: Automatic detection and highlighting of circular dependencies
- **Responsive Design**: Works on desktop and mobile devices
- **Custom Styling**: Modern, clean interface with customizable appearance

### Accessing the UI

Once configured, access the ServiceGraph UI at:
- Default: `https://your-app/service-graph/`
- Custom route: `https://your-app/your-custom-route/`

## UI Preview

<img alt="ServiceGraph UI" height="300" src="img.png"/>

## Performance Considerations

- **Caching**: Dependency resolution is cached for better performance
- **Lazy Loading**: Graph is built only when accessed
- **Memory Efficient**: Optimized for large dependency graphs
- **Async Operations**: Non-blocking graph generation

## Contributing

Contributions are welcome! If you have ideas for improvements or find any issues, please clone the repository and submit a pull request. Follow these steps to contribute:

1. Clone the repository
2. Create a new branch (`git checkout -b feature/your-feature`) from main branch
3. Make your changes
4. Run tests (`dotnet test`)
5. Commit your changes (`git commit -m 'Add some feature'`)
6. Push to the branch (`git push origin feature/your-feature`)
7. Open a pull request

### Development Setup

1. **Install .NET 8.0 SDK**
2. **Clone and build**:
   ```bash
   git clone https://github.com/nakkayev1707/ServiceGraph.git
   cd ServiceGraph
   dotnet restore
   dotnet build
   ```
3. **Run tests**:
   ```bash
   dotnet test
   ```
4. **Run the example**:
   ```bash
   cd ServiceGraphUsage
   dotnet run
   ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any issues or inquiries, please contact the project maintainer at ismailnakkayev@gmail.com.

## Topics

C#, Microservices, Dependency Management, Visualization, Interactive Graph, .NET 8, Dependency Injection
