using ConsoleApplication.Annotations;
using ConsoleApplication.Univers;

namespace ConsoleApplication.Renderer
{
    public interface IUniversRenderer
    {

        IUnivers Univers { get; }

        void Render();

    }
}
