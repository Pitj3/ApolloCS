using Apollo.Core;
using Silk.NET.Input;
using Silk.NET.Maths;

namespace Game
{
    internal class Program
    {
        internal class Game : Application
        {
            public Game(Vector2D<int> size, string title) : base(size, title)
            {

            }

            public override void OnUpdate(double delta)
            {
                base.OnUpdate(delta);

                if (Input.IsKeyDown(Key.Escape))
                {
                    Close();
                }
            }

            public override void OnRender(double delta)
            {
                base.OnRender(delta);
            }
        }

        private static void Main(string[] args)
        {
            Game game = new Game(new Vector2D<int>(1280, 720), "Apollo Engine");
            game.Start();
        }
    }
}
