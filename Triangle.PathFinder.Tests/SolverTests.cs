using Moq;
using System;
using System.Collections.Generic;
using Triangle.PathFinder.InputProvider;
using Triangle.PathFinder.Solver.Services;
using Xunit;

namespace Triangle.PathFinder.Tests
{
    public class SolverTests
    {
        [Fact]
        public void FindPath_GivenTriange_ReturnedMaxSum()
        {
            //arrange
            var inputReader = new Mock<IInputReader>();
            var inputMatrix = new int[4, 4] { {1,0,0,0 },{8,9,0,0 },{ 1, 5, 9, 0 }, { 4, 5, 2, 3 } };
            inputReader.Setup(m => m.PrepareInputMatrix()).Returns(inputMatrix);
            var pathFinderService = new PathFinderService(inputReader.Object);

            //act
            var answer=pathFinderService.FindPath();

            //assert
            Assert.Equal(16, answer.MaxSum);
        }

        [Fact]
        public void FindPath_GivenTriangle_ReturnedCorrectPath()
        {
            //arrange
            var inputReader = new Mock<IInputReader>();
            var inputMatrix = new int[4, 4] { { 1, 0, 0, 0 }, { 8, 9, 0, 0 }, { 1, 5, 9, 0 }, { 4, 5, 2, 3 } };
            inputReader.Setup(m => m.PrepareInputMatrix()).Returns(inputMatrix);
            var pathFinderService = new PathFinderService(inputReader.Object);
            var resultPath = new List<int> { 1, 8, 5, 2 };

            //act
            var answer = pathFinderService.FindPath();

            //assert
            Assert.Equal(resultPath, answer.Path);
        }
    }
}
