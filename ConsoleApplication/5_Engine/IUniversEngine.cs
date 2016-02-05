using ConsoleApplication.Annotations;
using ConsoleApplication.Renderer;

namespace ConsoleApplication.Engine
{
    public interface IUniversEngine
    {
        IUniversRenderer Renderer { get; set; }

        void Start();
        void Stop();
        void Init();
    }
}
