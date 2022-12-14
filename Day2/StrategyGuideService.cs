using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day2
{
    public class StrategyGuideService
    {
        private const string _rock = "rock";
        private const string _paper = "paper";
        private const string _scissors = "scissors";
        private const string _p1 = "p1";
        private const string _p2 = "p2";
        private const string _draw = "draw";
        private const string _win = "win";
        private const string _lose = "lose";
        private bool _usePartTwoLogic = false;

        public void Process()
        {
            var fileService = new Common.FileService();
            var input = fileService.LoadEmbededResourceAsString("Day2.Input.txt");
            var inputs = input.Split(Environment.NewLine);
            var playerOneInputs = new List<string>();
            var playerTwoInputs = new List<string>();
            foreach (var item in inputs)
            {
                var t = item.Split(" ");
                playerOneInputs.Add(t[0].ToLower());
                playerTwoInputs.Add(t[1].ToLower());
            }
            
            var score = PlayRockPaperScissors(playerOneInputs, playerTwoInputs);
            Console.WriteLine("Part One: ");
            Console.WriteLine($"Score: {score}");

            // X = Lose, Y = Draw, Z = Win
            _usePartTwoLogic = true;
            score = PlayRockPaperScissors(playerOneInputs, playerTwoInputs);
            Console.WriteLine("Part Two: ");
            Console.WriteLine($"Score: {score}");
        }

        private object PlayRockPaperScissors(List<string> playerOneInputs, List<string> playerTwoInputs)
        {
            var points = 0;
            for (int i = 0; i < playerOneInputs.Count; i++)
            {
                points += EvaluateRound(playerOneInputs[i], playerTwoInputs[i]);
            }

            return points;
        }

        private int EvaluateRound(string p1, string p2)
        {
            var point = 0;
            var result = EvaluateShapes(p1, p2, out string newp2);

            if (_usePartTwoLogic)
                p2 = newp2;

            if (result.Equals(_p1))
            {
                // Loss, track shape points only
                point += GetShapePoints(p2);
                return point;
            }
            else if (result.Equals(_p2))
            {
                // Win
                point += 6;
                point += GetShapePoints(p2);
                return point;
            }
            else if (result.Equals(_draw))
            {
                point += 3;
                point += GetShapePoints(p2);
                return point;
            }

            throw new Exception("Shouldn't happen");
        }

        private string EvaluateShapes(string p1, string p2, out string newp2)
        {
            p1 = ConverLetterToShape(p1);
            if (!_usePartTwoLogic)
                p2 = ConverLetterToShape(p2);
            else
                p2 = ConvertLetterToObjective(p2);

            if (p2.Equals(_win))
            {
                if (p1.Equals(_rock))
                    p2 = _paper;
                else if (p1.Equals(_scissors))
                    p2 = _rock;
                else if (p1.Equals(_paper))
                    p2 = _scissors;
            }
            else if (p2.Equals(_draw))
            {
                p2 = p1;
            }
            else if (p2.Equals(_lose))
            {
                if (p1.Equals(_rock))
                    p2 = _scissors;
                else if (p1.Equals(_scissors))
                    p2 = _paper;
                else if (p1.Equals(_paper))
                    p2 = _rock;
            }

            newp2 = ConvertShapeToLetter(p2);

            // Rock
            if (p1.Equals(_rock) && p2.Equals(_scissors))
                return _p1;
            if (p1.Equals(_rock) && p2.Equals(_paper))
                return _p2;
            if (p1.Equals(_rock) && p2.Equals(_rock))
                return _draw;
            
            // Paper
            if (p1.Equals(_paper) && p2.Equals(_scissors))
                return _p2;
            if (p1.Equals(_paper) && p2.Equals(_rock))
                return _p1;
            if (p1.Equals(_paper) && p2.Equals(_paper))
                return _draw;
            
            // Scissors
            if (p1.Equals(_scissors) && p2.Equals(_scissors))
                return _draw;
            if (p1.Equals(_scissors) && p2.Equals(_paper))
                return _p1;
            if (p1.Equals(_scissors) && p2.Equals(_rock))
                return _p2;

            throw new Exception("Shape not found");
        }

        private string ConvertShapeToLetter(string shape)
        {
            switch (shape.ToLower())
            {
                case _rock:
                    return "x";
                case _paper:
                    return "y";
                case _scissors:
                    return "z";
                default:
                    throw new Exception("Unknown shape");
            }
        }

        private string ConvertLetterToObjective(string letter)
        {
            switch (letter.ToLower())
            {
                case "x":
                    return _lose;
                case "y":
                    return _draw;
                case "z":
                    return _win;
                default:
                    throw new Exception("Unknown Letter");
            }
        }

        private string ConverLetterToShape(string letter)
        {
            switch (letter.ToLower())
            {
                case "a":
                case "x":
                    return _rock;
                case "b":
                case "y":
                    return _paper;
                case "c":
                case "z":
                    return _scissors;
                default:
                    throw new Exception("Unknown Letter");
            }
        }

        public int GetShapePoints(string letter)
        {
            var shape = ConverLetterToShape(letter);

            switch (shape.ToLower())
            {
                case _rock:
                    return 1;
                case _paper:
                    return 2;
                case _scissors:
                    return 3;
                default:
                    throw new Exception("Unknown Shape");
            }
        }
    }
}
