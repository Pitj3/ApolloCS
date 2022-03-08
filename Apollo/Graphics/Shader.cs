using Apollo.Core;
using Silk.NET.OpenGL;

namespace Apollo.Graphics
{
    public enum ShaderModuleType
    {
        Vertex,
        Geometry,
        Hull,
        Domain,
        Fragment,
        Compute
    }

    public class ShaderModule
    {
        public ShaderType Type { get; private set; }
        
        internal uint ID { get; private set; }

        public ShaderModule(ShaderType type, string source)
        {
            Type = type;

            ID = Graphics.GL.CreateShader(type);
            Graphics.GL.ShaderSource(ID, source);
            Graphics.GL.CompileShader(ID);

            string infoLog = Graphics.GL.GetShaderInfoLog(ID);
            if (infoLog != null)
            {
                Log.Error(infoLog);
            }
        }
    }

    public class ShaderProgram
    {
        internal uint ID { get; private set; }

        public ShaderProgram(params ShaderModule[] modules)
        {
            ID = Graphics.GL.CreateProgram();

            foreach (ShaderModule module in modules)
            {
                Graphics.GL.AttachShader(ID, module.ID);
            }

            Graphics.GL.LinkProgram(ID);

            string infoLog = Graphics.GL.GetProgramInfoLog(ID);
            if (infoLog != null)
            {
                Log.Error(infoLog);
            }

            foreach (ShaderModule module in modules)
            {
                Graphics.GL.DeleteShader(module.ID);
            }
        }

        public void Bind()
        {
            Graphics.GL.UseProgram(ID);
        }
    }
}
