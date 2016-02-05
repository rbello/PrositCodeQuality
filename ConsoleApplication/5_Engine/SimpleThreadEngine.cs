using System;
using ConsoleApplication.Renderer;
using System.Threading;
using ConsoleApplication.Annotations;

namespace ConsoleApplication.Engine
{
    public class SimpleThreadEngine : IUniversEngine
    {
        private Thread _thread;

        private bool _run = false;

        private bool _updated = true;

        public IUniversRenderer Renderer { get; set; }

        public int TickDelay { get; set; } = 500;

        public void Init()
        {
            // Non utilisé dans cette implémentation, mais pourrait servir à
            // initialiser une vue d'IHM par exemple.
        }

        public void Start()
        {
            if (_thread != null && _run)
            {
                throw new Exception("Engine is allready started");
            }
            _run = true;
            _thread = new Thread(this.Run);
            _thread.Start();
        }

        internal void Run()
        {
            while (_run)
            {
                Renderer.Univers.Update();
                Renderer.Render();
                Thread.Sleep(TickDelay);
            }
        }

        public void Stop()
        {
            _run = false;
        }

    }
}
