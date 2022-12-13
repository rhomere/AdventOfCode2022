using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day1
{
    public class Elf
    {
        public int Number { get; set; }
        public List<int> Calories { get; set; } = new List<int>();
        public int TotalCalories => Calories.Sum();
    }
}
