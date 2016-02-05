using System;
using ConsoleApplication.Univers;
using System.Text;
using System.Windows;
using ConsoleApplication.Elements;
using ConsoleApplication.Moves;

namespace ConsoleApplication.Renderer
{
    public class ColorConsoleRenderer : IUniversRenderer
    {
        private readonly string TIRET;
        private readonly string PIPE;
        private readonly string BLANK;
        private Point? _collision;

        public IUnivers Univers { get; private set; }

        public ColorConsoleRenderer(IUnivers univers)
        {
            Univers = univers;
            TIRET = "-";
            PIPE = "|";
            BLANK = " ";

            univers.OnCollision += this.HandleCollision;
        }

        private void HandleCollision(ElementsCollisionEvent evt)
        {
            _collision = evt.AtLocation;
        }

        public void Render()
        {
            Console.Clear();
            var bar = new StringBuilder().Insert(0, TIRET, Univers.Width + 2).ToString();
            Console.WriteLine(bar);
            for (int y = 0, h = Univers.Height; y < h; ++y)
            {
                Console.Write(PIPE);
                for (int x = 0, w = Univers.Width; x < w; ++x)
                {
                    var p = new Point(x, y);
                    if (_collision != null && _collision == p)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    var element = Univers[p];
                    if (element != null)
                    {
                        ConsoleColor color = element.GetType() == typeof(Molecule) ? ConsoleColor.Red : ConsoleColor.Blue;
                        Console.ForegroundColor = color;
                        Console.Write(element.Symbol.Substring(0, 1));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(BLANK);
                    }
                    if (_collision != null && _collision == p)
                    {
                        _collision = null;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.WriteLine(PIPE);
            }
            Console.WriteLine(bar);
            Console.WriteLine("Hit enter to stop");
        }
    }
}
