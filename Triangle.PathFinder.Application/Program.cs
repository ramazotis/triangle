using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Triangle.PathFinder.InputProvider;
using Triangle.PathFinder.Solver.Interfaces;
using Triangle.PathFinder.Solver.Services;

namespace Triangle.PathFinder.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultInputFilePath = "input.txt";
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IInputReader>(s => new InputReader(defaultInputFilePath))
            .AddSingleton<IPathFinderService, PathFinderService>()
            .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();


            logger.LogInformation("Starting application");
            logger.LogInformation("Enter input file name path or press <ENTER> to read default file <{0}>:", defaultInputFilePath);
            var filePath=Console.ReadLine();
            if (!filePath.Equals(String.Empty))
            {
                serviceProvider.GetService<IInputReader>()
                    .SetFilePath(filePath);
            }

            serviceProvider.GetService<IInputReader>().ReadInput();

            var pathFinderService = serviceProvider.GetService<IPathFinderService>();
            var answer=pathFinderService.FindPath();
            logger.LogInformation("Max sum: {0}", answer.MaxSum);
            logger.LogInformation("Path: {0}", answer.Path);

            logger.LogInformation("Press <ENTER> to close app...");
            Console.ReadLine();
        }
    }
}
