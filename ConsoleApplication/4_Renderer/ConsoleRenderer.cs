using System;
using ConsoleApplication.Univers;
using System.Text;
using System.Windows;

namespace ConsoleApplication.Renderer
{
    public class ConsoleRenderer : IUniversRenderer
    {
        private readonly string TIRET;
        private readonly string PIPE;
        private readonly string BLANK;

        public IUnivers Univers { get; private set; }

        public ConsoleRenderer(IUnivers univers)
        {
            Univers = univers;
            TIRET = "-";
            PIPE = "|";
            BLANK = " ";
        }

        public void Render()
        {
            Console.Clear();
            var bar = new StringBuilder().Insert(0, TIRET, Univers.Width + 2).ToString();
            Console.WriteLine(bar);
            for (int y = 0, h = Univers.Height; y < h; ++y)
            {
                var line = new StringBuilder(PIPE);
                for (int x = 0, w = Univers.Width; x < w; ++x)
                {
                    var element = Univers[new Point(x, y)];
                    if (element != null)
                    {
                        line.Append(element.Symbol.Substring(0, 1));
                    }
                    else
                    {
                        line.Append(BLANK);
                    }
                }
                line.Append(PIPE);
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine(bar);
            Console.WriteLine("Hit enter to stop");
        }
    }
}
