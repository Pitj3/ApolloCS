using System;
using Silk.NET.Windowing;

namespace Apollo.Core
{
    public class Window
    {
        #region Internal Data
        internal IWindow silkWindow;
        internal WindowOptions options;
        #endregion

        #region Public Data
        public static Window Current { get; private set; }

        public int Width => options.Size.X;
        public int Height => options.Size.Y;

        public string Title => options.Title;
        #endregion

        public Window(WindowOptions options)
        {
            Current = this;
            this.options = options;

            silkWindow = Silk.NET.Windowing.Window.Create(options);
        }
    }

    public class WindowResizeEvent : Event
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public WindowResizeEvent(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public class WindowMovedEvent : Event
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public WindowMovedEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
