using Apollo.Core;
using Silk.NET.OpenGL;

namespace Apollo.Graphics
{
    public sealed class Graphics
    {
        private static GL _gl;
        public static GL GL
        {
            get { return _gl ??= GL.GetApi(Window.Current.silkWindow); }
        }
    }
}
