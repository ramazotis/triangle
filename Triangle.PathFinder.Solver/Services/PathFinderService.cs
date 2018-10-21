using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Triangle.PathFinder.InputProvider;
using Triangle.PathFinder.Solver.Interfaces;
using Triangle.PathFinder.Solver.Model;

namespace Triangle.PathFinder.Solver.Services
{
    public class PathFinderService : IPathFinderService
    {
        protected IInputReader _inputReader;

        public PathFinderService(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public Answer FindPath()
        {
            var inputMatrix = _inputReader.PrepareInputMatrix();
            return Solve(inputMatrix);
        }

        private Answer Solve(int[,] triangle)
        {
            int rowMaxIndex = triangle.GetLength(0);
            int collMaxIndex = triangle.GetLength(1);
            int[,] tri = new int[rowMaxIndex, collMaxIndex];
            Array.Copy(triangle, tri, triangle.Length);
            Dictionary<string, string> pointersDictionary = new Dictionary<string, string>();
            for (int i = rowMaxIndex-2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    var takeLeft = false;
                    var takeRight = false;
                    if ((IsOdd(triangle[i, j]) && IsEven(triangle[i + 1, j]))
                         || (IsEven(triangle[i, j]) && IsOdd(triangle[i + 1, j])))
                        takeLeft = true;
                    if ((IsOdd(triangle[i, j]) && IsEven(triangle[i + 1, j + 1]))
                         || (IsEven(triangle[i, j]) && IsOdd(triangle[i + 1, j + 1])))
                        takeRight = true;
                    if (takeLeft && takeRight)
                    {
                        if (tri[i + 1, j]> tri[i + 1, j + 1])
                        {
                            tri[i, j] += tri[i + 1, j];
                            pointersDictionary.Add(i + ":" + j, (i + 1) + ":" + (j));
                        }
                        else
                        {
                            tri[i, j] += tri[i + 1, j+1];
                            pointersDictionary.Add(i + ":" + j, (i + 1) + ":" + (j+1));
                        }
                    }else if (takeLeft)
                    {
                        tri[i, j] += tri[i + 1, j];
                        pointersDictionary.Add(i + ":" + j, (i + 1) + ":" + (j));
                    }
                    else if (takeRight)
                    {
                        tri[i, j] += tri[i + 1, j + 1];
                        pointersDictionary.Add(i + ":" + j, (i + 1) + ":" + (j+1));
                    }        
                }
            }
            var fullPath = PathBuilder(pointersDictionary, triangle);
            return new Answer { MaxSum = tri[0, 0], Path = fullPath };
        }

        private List<int> PathBuilder(Dictionary<string, string> pointersDictionary, int[,] triangle)
        {
            List<int> result = new List<int>();
            var nextAddress = "0:0";
            while (nextAddress != null)
            {
                var indexes = nextAddress.Split(":").Select(Int32.Parse).ToArray();
                result.Add(triangle[indexes[0], indexes[1]]);
                nextAddress = pointersDictionary.GetValueOrDefault(nextAddress);
            }
            return result;
        }


        private bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        private bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }
}
