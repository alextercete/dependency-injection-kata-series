using DependencyInjection.Console.Entities;
using DependencyInjection.Console.SquarePainters;

namespace DependencyInjection.Console
{
    internal class PatternGenerator
    {
        private readonly ISquarePainter _squarePainter;

        public PatternGenerator(ISquarePainter squarePainter)
        {
            _squarePainter = squarePainter;
        }

        public Pattern Generate(int width, int height)
        {
            var pattern = new Pattern(width, height);
            var squares = pattern.Squares;

            for (var i = 0; i < width; ++i)
            {
                for (var j = 0; j < height; ++j)
                {
                    squares[j, i] = _squarePainter.PaintSquare(width, height, i, j);
                }
            }

            return pattern;
        }
    }
}