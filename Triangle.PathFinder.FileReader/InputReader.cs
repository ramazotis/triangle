using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Triangle.PathFinder.InputProvider
{
    public class InputReader : IInputReader
    {
        protected string _filePath;
        protected string[] _inputLines;


        public InputReader(string filePath) {
            _filePath = filePath;
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        public string[] ReadInput()
        {
            _inputLines = File.ReadAllLines(_filePath);
            return _inputLines;
        }

        public virtual int[,] PrepareInputMatrix()
        {
            int linesCount = _inputLines.Count();
            int elementsCountInLine = _inputLines[linesCount-1].Split(" ").Count();
            List<int[]> arraysList = new List<int[]>();
            foreach (var line in _inputLines)
            {
                var matrixLine=line.Split(" ").Select(Int32.Parse).ToArray();
                Array.Resize(ref matrixLine, elementsCountInLine);
                arraysList.Add(matrixLine);
            }
            var matrix = CreateMatrix(arraysList);
            return matrix;
        }

        private T[,] CreateMatrix<T>(IList<T[]> arrays)
        {
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }
    }
}
