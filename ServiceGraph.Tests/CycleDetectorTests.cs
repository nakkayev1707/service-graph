using QuickGraph;
using QuickGraph.Graphviz;
using ServiceGraph.Core;
using Xunit;

namespace ServiceGraph.Tests;

public class CycleDetectorTests
{
    [Fact]
    public void TryFindCircularDependentServices_WithNoCycle_ReturnsNull()
    {
        // Arrange
        var graph = new AdjacencyGraph<Type, Edge<Type>>();
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(string), typeof(int)));
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(int), typeof(double)));
        
        var graphviz = new GraphvizAlgorithm<Type, Edge<Type>>(graph);
        var cycleDetector = new CycleDetector(graphviz);

        // Act
        var result = cycleDetector.TryFindCircularDependentServices();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void TryFindCircularDependentServices_WithCycle_ReturnsCycleNodes()
    {
        // Arrange
        var graph = new AdjacencyGraph<Type, Edge<Type>>();
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(string), typeof(int)));
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(int), typeof(string))); // This creates a cycle
        
        var graphviz = new GraphvizAlgorithm<Type, Edge<Type>>(graph);
        var cycleDetector = new CycleDetector(graphviz);

        // Act
        var result = cycleDetector.TryFindCircularDependentServices();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(typeof(string), result!.Item1);
        Assert.Equal(typeof(int), result.Item2);
    }

    [Fact]
    public void TryFindCircularDependentServices_WithComplexCycle_ReturnsCycleNodes()
    {
        // Arrange
        var graph = new AdjacencyGraph<Type, Edge<Type>>();
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(string), typeof(int)));
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(int), typeof(double)));
        graph.AddVerticesAndEdge(new Edge<Type>(typeof(double), typeof(string))); // This creates a cycle
        
        var graphviz = new GraphvizAlgorithm<Type, Edge<Type>>(graph);
        var cycleDetector = new CycleDetector(graphviz);

        // Act
        var result = cycleDetector.TryFindCircularDependentServices();

        // Assert
        Assert.NotNull(result);
    }
} 