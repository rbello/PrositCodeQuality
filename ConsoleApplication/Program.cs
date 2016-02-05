using ConsoleApplication.Elements;
using ConsoleApplication.Engine;
using ConsoleApplication.Moves;
using ConsoleApplication.Renderer;
using ConsoleApplication.Univers;
using System;
using System.Windows;

namespace ConsoleApplication
{
    class Program
    {
        public static void Main(string[] args)
        {

            // Fabriquer et peupler l'univers
            IUnivers u = InitUnivers(40, 20);

            // Créer un renderer
            IUniversRenderer r = new ColorConsoleRenderer(u);

            // Créer un moteur
            IUniversEngine e = new SimpleThreadEngine() {
                Renderer = r,
                TickDelay = 400
            };

            // On gère les collisions (pas dans le prosit)
            u.OnCollision += Program.HandleCollision;

            // Lancer le moteur
            e.Init();
            e.Start();

            // On attend pour éteindre
            Console.ReadLine();
            e.Stop();

        }

        private static IUnivers InitUnivers(int width, int height)
        {
            var u = new Univers2D(width, height);

            IElementFactory f = ChemicalElementFactory.GetInstance();

            u += f.Create("Hydrogen", (int)(width * .3), (int)(height * .9), u);
            u[0].Displacement = new Vector(1, -1);
            u += f.Create("Carbone",   (int)(width * .8), (int)(height * .2), u);
            u[1].Displacement = new Vector(1, 2);
            u += f.Create("Carbone", (int)(width * .7), (int)(height * .3), u);
            u[2].Displacement = new Vector(1, -1);
            u += f.Create("Helium",   (int)(width * .3), (int)(height * .4), u);
            u[3].Displacement = new Vector(2, 2);
            u += f.Create("Lithium",  (int)(width * .5), (int)(height * .4), u);
            u[4].Displacement = new Vector(-2, -1);
            u += f.Create("Oxygen", (int)(width * .3), (int)(height * .6), u);
            u[5].Displacement = new Vector(-1, -1);
            u += f.Create("Helium", (int)(width * .1), (int)(height * .7), u);
            u[6].Displacement = new Vector(2, 2);
            u += f.Create("Helium", (int)(width * .2), (int)(height * .8), u);
            u[7].Displacement = new Vector(2, 3);
            u += f.Create("Helium", (int)(width * .3), (int)(height * .5), u);
            u[8].Displacement = new Vector(1, 2);

            u += f.Create(new Atom[] {
                f.Create("Hydrogen"),
                f.Create("Hydrogen"),
                f.Create("Oxygen")
            });
            u[9].MoveStrategy = new UnBoundedMovesStrategy(u);
            u[9].Displacement = new Vector(3, -2);

            return u;
        }

        private static void HandleCollision(ElementsCollisionEvent evt)
        {
            // Implémentation sommaire, qui ne tient pas compte de la masse
            // Ce mécanisme devrait plutôt être exporté dans une classe à part
            Vector moveA = evt.FromElement.Displacement;
            Vector moveB = evt.ToElement.Displacement;
            // On intervertit simplement les vecteurs de déplacements
            evt.FromElement.Displacement = moveB;
            evt.ToElement.Displacement = moveA;
        }
    }
}
