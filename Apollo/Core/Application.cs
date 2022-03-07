using System.Drawing;
using Apollo.Layer;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Apollo.Core
{
    public class Application
    {
        #region Public Data
        public Window Window { get; private set; }
        public static Application Current { get; private set; }
        #endregion

        #region Private Data
        private readonly LayerStack _layers = new LayerStack();
        #endregion

        public Application(Vector2D<int> size, string title)
        {
            Current = this;

            WindowOptions options = WindowOptions.Default; 
            options.Size = size;
            options.Title = title;

            Window = new Window(options);

            Window.silkWindow.Load += OnLoad;
            Window.silkWindow.Update += OnUpdate;
            Window.silkWindow.Render += OnRender;
            Window.silkWindow.Closing += OnClose;

            Window.silkWindow.Resize += SilkWindowOnResize;
            Window.silkWindow.Move += SilkWindowOnMove;
        }

        #region Private API
        private void SilkWindowOnResize(Vector2D<int> size)
        {
            OnInternalEvent(new WindowResizeEvent(size.X, size.Y));
        }

        private void SilkWindowOnMove(Vector2D<int> location)
        {
            OnInternalEvent(new WindowMovedEvent(location.X, location.Y));
        }

        private void OnInternalEvent(Event e)
        {
            EventDispatcher dispatcher = new EventDispatcher(e);
            dispatcher.Dispatch<WindowResizeEvent>(OnWindowResized);
            dispatcher.Dispatch<WindowMovedEvent>(OnWindowMoved);
        }
        #endregion

        #region Internal Events
        internal bool OnWindowResized(WindowResizeEvent e)
        {
            GL gl = GL.GetApi(Window.silkWindow);
            gl.Viewport(new Size(e.Width, e.Height));

            return false;
        }

        internal bool OnWindowMoved(WindowMovedEvent e)
        {
            return false;
        }
        #endregion

        #region Public API
        public void Start()
        {
            Window.silkWindow.Run();
        }

        public void Close()
        {
            OnEvent(new ApplicationClosedEvent());

            Window.silkWindow.Close();
        }
        #endregion

        #region Public Virtual API
        public virtual void OnLoad()
        {
            _layers.PushOverlay(new ImGUILayer());
        }

        public virtual void OnUpdate(double delta)
        {
            foreach (Layer layer in _layers.layers)
            {
                layer.OnUpdate(delta);
            }
        }

        public virtual void OnRender(double delta)
        {
            foreach (Layer layer in _layers.layers)
            {
                layer.OnRender(delta);
            }
        }

        public virtual void OnClose()
        {

        }

        public virtual void OnEvent(Event e)
        {
            foreach (Layer layer in _layers.layers)
            {
                layer.OnEvent(e);
            }
        }
        #endregion
    }

    public class ApplicationClosedEvent : Event { }
}
