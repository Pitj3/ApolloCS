using System.Linq;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;

namespace Apollo.Core
{
    public static class Input
    {
        #region Internal Data
        internal static readonly IInputContext Context;
        #endregion

        #region Private Static Data
        private static readonly IKeyboard _primaryKeyboard;
        private static readonly IMouse _primaryMouse;

        private static readonly KeyState[] _keyStates = new KeyState[(int)Key.Menu + 1];
        private static readonly KeyState[] _lastKeyStates = new KeyState[(int)Key.Menu + 1];

        private static readonly KeyState[] _mouseButtonStates = new KeyState[(int)MouseButton.Button12 + 1];
        private static readonly KeyState[] _lastMouseButtonStates = new KeyState[(int)MouseButton.Button12 + 1];
        #endregion

        #region Enums
        public enum KeyState
        {
            None,
            Pressed,
            Down,
            Up
        }
        #endregion


        #region Public Static Data
        public static Vector2D<int> MousePosition { get; internal set; }
        #endregion

        static Input()
        {
            Context = Window.Current.silkWindow.CreateInput();
            _primaryKeyboard = Context.Keyboards.FirstOrDefault();
            _primaryMouse = Context.Mice.FirstOrDefault();

            if (_primaryKeyboard != null)
            {
                _primaryKeyboard.KeyDown += OnKeyDown;
                _primaryKeyboard.KeyUp += OnKeyUp;
            }
            else
            {
                // Error 
            }

            if (_primaryMouse != null)
            {
                _primaryMouse.MouseDown += OnMouseDown;
                _primaryMouse.MouseUp += OnMouseUp;
                _primaryMouse.MouseMove += OnMouseMove;
            }
            else
            {
                // Error
            }

            for (int i = 0; i < (int)Key.Menu + 1; i++)
            {
                _keyStates[i] = KeyState.None;
                _lastKeyStates[i] = KeyState.None;
            }

            for (int i = 0; i < (int)MouseButton.Button12 + 1; i++)
            {
                _mouseButtonStates[i] = KeyState.None;
                _lastMouseButtonStates[i] = KeyState.None;
            }
        }

        #region Private API
        private static void OnKeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            SetKeyPressed(key);
        }

        private static void OnKeyUp(IKeyboard keyboard, Key key, int arg3)
        {
            SetKeyReleased(key);
        }

        private static void OnMouseDown(IMouse keyboard, MouseButton button)
        {
            SetMouseButtonPressed(button);
        }

        private static void OnMouseUp(IMouse keyboard, MouseButton button)
        {
            SetMouseButtonReleased(button);
        }

        private static void OnMouseMove(IMouse mouse, Vector2 position)
        {
            MousePosition = new Vector2D<int>((int)position.X, (int)position.Y);
        }
        #endregion

        #region Internal API
        internal static void SetKeyPressed(Key key)
        {
            if (_keyStates[(int) key] == KeyState.Pressed || _keyStates[(int) key] == KeyState.Down)
                return;

            _keyStates[(int)key] = KeyState.Pressed;
        }

        internal static void SetKeyReleased(Key key)
        {
            _keyStates[(int)key] = KeyState.Up;
        }

        internal static void SetMouseButtonPressed(MouseButton button)
        {
            if (_mouseButtonStates[(int)button] == KeyState.Pressed || _mouseButtonStates[(int)button] == KeyState.Down)
                return;

            _mouseButtonStates[(int)button] = KeyState.Pressed;
        }

        internal static void SetMouseButtonReleased(MouseButton button)
        {
            _mouseButtonStates[(int)button] = KeyState.Up;
        }

        internal static void Update()
        {
            for (int i = 0; i < (int)Key.Menu + 1; i++)
            {
                if (_keyStates[i] == KeyState.Pressed && _lastKeyStates[i] == KeyState.Pressed)
                {
                    _keyStates[i] = KeyState.Down;
                }

                if(_keyStates[i] == KeyState.Up && _lastKeyStates[i] == KeyState.Up)
                {
                    _keyStates[i] = KeyState.None;
                }

                if (_mouseButtonStates[i] == KeyState.Pressed && _lastMouseButtonStates[i] == KeyState.Pressed)
                {
                    _mouseButtonStates[i] = KeyState.Down;
                }

                if (_mouseButtonStates[i] == KeyState.Up && _lastMouseButtonStates[i] == KeyState.Up)
                {
                    _mouseButtonStates[i] = KeyState.None;
                }

                _lastKeyStates[i] = _keyStates[i];
                _lastMouseButtonStates[i] = _mouseButtonStates[i];
            }
        }
        #endregion

        #region Public API
        public static bool IsKeyDown(Key key)
        {
            return _keyStates[(int) key] == KeyState.Pressed || _keyStates[(int) key] == KeyState.Down;
        }

        public static bool IsKeyPressed(Key key)
        {
            return _keyStates[(int)key] == KeyState.Pressed;
        }

        public static bool IsKeyUp(Key key)
        {
            return _keyStates[(int) key] == KeyState.Up;
        }
        #endregion
    }
}
