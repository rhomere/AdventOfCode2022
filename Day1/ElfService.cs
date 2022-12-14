using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day1
{
    public class ElfService
    {
        public void Process()
        {
            var fileService = new Common.FileService();
            var input = fileService.LoadEmbededResourceAsString("Day1.Input.txt");

            var elvesList = GetListOfElves(input);

            var elf = GetElfWithMostCalories(elvesList);

            Console.WriteLine($"Elf #{elf.Number} has most calories at {elf.TotalCalories}");

            var topThreeElves = GetTopThreeElvesWithMostCalories(elvesList);

            Console.WriteLine($"Top Three Elves with most calories are");
            foreach (var topElf in topThreeElves)
            {
                Console.WriteLine($"Elf #{topElf.Number} at {topElf.TotalCalories}");
            }
            Console.WriteLine($"Altogether they have {topThreeElves.Select(x => x.TotalCalories).ToList().Sum()}");
        }

        private List<Elf> GetTopThreeElvesWithMostCalories(List<Elf> elvesList)
        {
            return elvesList.OrderBy(x => x.TotalCalories).TakeLast(3).ToList();
        }

        private Elf GetElfWithMostCalories(List<Elf> elvesList)
        {
            return elvesList.OrderBy(x => x.TotalCalories).Last();
        }

        private List<Elf> GetListOfElves(string input)
        {
            var counter = 1;
            var inputs = input.Split(Environment.NewLine);

            var elf = new Elf();
            var elves = new List<Elf>();

            foreach (var item in inputs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    elf.Calories.Add(Int32.Parse(item));
                }
                else
                {
                    elf.Number = counter;
                    elves.Add(elf);
                    elf = new Elf();
                }

                counter++;
            }
                
            return elves;
        }
    }
}
