using System.Drawing;
using System.Numerics;
using Apollo.Core;
using ImGuiNET;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Apollo.Layer
{
    public class ImGUILayer : Core.Layer
    {
        private ImGuiController _controller;
        private GL gl;

        public override void OnAttach()
        {
            base.OnAttach();

            gl = GL.GetApi(Window.Current.silkWindow);

            _controller = new ImGuiController(GL.GetApi(Window.Current.silkWindow), Window.Current.silkWindow,
                Input.Context);
        }

        public override void OnDetach()
        {
            base.OnDetach();
        }

        public override void OnEvent(Event e)
        {
            base.OnEvent(e);
        }

        public override void OnUpdate(double delta)
        {
            base.OnUpdate(delta);

            _controller.Update((float)delta);
        }

        public override void OnRender(double delta)
        {
            base.OnRender(delta);

            gl.ClearColor(Color.FromArgb(255, 100, 149, 237));
            gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            const ImGuiWindowFlags flags = ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.AlwaysAutoResize |
                                     ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoFocusOnAppearing
                                     | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoMove;

            ImGui.SetNextWindowPos(new Vector2(10, 10));
            ImGui.SetNextWindowSize(new Vector2(150, 50));
            if (ImGui.Begin("", flags))
            {
                ImGui.Text("FPS: " + (int)(1.0 / delta));
                ImGui.Separator();
                ImGui.Text("Delta: 0" + delta.ToString("#.###"));
            }
            ImGui.End();

            _controller.Render();
        }
    }
}
