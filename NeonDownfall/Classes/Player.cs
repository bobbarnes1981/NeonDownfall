using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NeonDownfall.Classes
{
    class Player
    {
        // use a collection so we can select the correct animation
        Texture2D texture;

        Vector2 position;

        public Player(GraphicsDevice graphicsDevice)
        {
            // 2*4 tiles (8*8)
            int width = 16;
            int height = 32;

            position = new Vector2(0, 0);

            // just use plain colour for now
            texture = new Texture2D(graphicsDevice, width, height);
            Color[] playerColor = new Color[width * height];
            for (int i = 0; i < playerColor.Length; i++)
            {
                playerColor[i] = Color.White;
            }

            texture.SetData(playerColor);
        }

        public void Update(double elapsedSeconds)
        {
            // TODO: allow key/pad mapping
            var state = Keyboard.GetState();

            // TODO: animation based movements of player
            //       on keypress should start step animation, if no keypress complete animation
            //       on still keypress should start/continue run animation, if no keypress stop animation

            int speedWalk = 100;

            Vector2 movement = new Vector2();
            if (state.IsKeyDown(Keys.Up))
            {
                movement.Y -= (float)(1 * (speedWalk * elapsedSeconds));
            }
            if (state.IsKeyDown(Keys.Down))
            {
                movement.Y += (float)(1 * (speedWalk * elapsedSeconds));
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movement.X -= (float)(1 * (speedWalk * elapsedSeconds));
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movement.X += (float)(1 * (speedWalk * elapsedSeconds));
            }

            position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
