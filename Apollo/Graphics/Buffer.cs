using System;
using System.Collections.Generic;
using System.Text;
using Apollo.Core;
using Silk.NET.OpenGL;

namespace Apollo.Graphics
{
    public class Buffer
    {
        protected uint id;

        public Buffer()
        {
            GL gl = GL.GetApi(Window.Current.silkWindow);
            id = gl.GenBuffer();
        }
    }

    public class VertexBuffer
    {
        public VertexBuffer(uint size)
        {

        }

        public VertexBuffer(object data, uint size)
        {

        }

        public void SetData(object data, uint size)
        {

        }
    }
}
