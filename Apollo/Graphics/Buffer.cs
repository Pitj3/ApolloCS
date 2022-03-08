using System.Collections.Generic;

namespace Apollo.Graphics
{
    public class Buffer
    {
        protected uint id;
        public uint Size { get; protected set; }

        public Buffer()
        {
            id = Graphics.GL.CreateBuffer();
        }
    }

    public class VertexBuffer : Buffer
    {
        public VertexBuffer(uint size)
        {
            Size = size;

            unsafe
            {
                Graphics.GL.NamedBufferStorage(id, size, null, 0U);
            }
        }

        public VertexBuffer(float[] data)
        {
            SetData(data);
        }

        public VertexBuffer(List<float> data)
        {
            SetData(data.ToArray());
        }

        public void SetData(List<float> data)
        {
            SetData(data.ToArray());
        }

        public void SetData(float[] data)
        {
            Size = (uint) data.Length * 4U;

            unsafe
            {
                fixed (void* d = &data[0])
                {
                    Graphics.GL.NamedBufferStorage(id, (uint)data.Length * 4U, d, 0U);
                }
            }
        }
    }

    public class IndexBuffer : Buffer
    {
        public IndexBuffer(uint count)
        {
            Size = count * 4U;

            unsafe
            {
                Graphics.GL.NamedBufferStorage(id, count * 4U, null, 0U);
            }
        }

        public IndexBuffer(uint[] data)
        {
            SetData(data);
        }

        public IndexBuffer(List<uint> data)
        {
            SetData(data.ToArray());
        }

        public void SetData(List<uint> data)
        {
            SetData(data.ToArray());
        }

        public void SetData(uint[] data)
        {
            Size = (uint) data.Length * 4U;

            unsafe
            {
                fixed (void* d = &data[0])
                {
                    Graphics.GL.NamedBufferStorage(id, (uint)data.Length * 4U, d, 0U);
                }
            }
        }
    }
}
