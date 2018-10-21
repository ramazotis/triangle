using System;
using System.Collections.Generic;
using System.Text;

namespace Triangle.PathFinder.Solver.Model
{
    public class Answer
    {
        public int MaxSum { get; set; }
        public IEnumerable<int> Path { get; set; }
    }
}
