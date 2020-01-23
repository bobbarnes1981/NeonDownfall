using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeonDownfall.Classes
{
    class Engine
    {
        // 28*20 tiles (8*8)
        int tilesX = 28;
        int tilesY = 20;

        int tileSize = 8;

        Player player;
        Texture2D pixel;

        public Engine(GraphicsDevice graphicsDevice)
        {
            player = new Player(graphicsDevice);

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.DarkGray });

            // TODO: states for title/game/gameover
            // TODO: show title screen
            // TODO: menus, also maybe nice in game menus
        }

        public void Update(double elapsedSeconds)
        {
            player.Update(elapsedSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int y = 0; y < tilesY * tileSize; y++)
            {
                for (int x = 0; x < tilesX * tileSize; x++)
                {
                    if (y % 8 == 0 || x % 8 == 0)
                    {
                        spriteBatch.Draw(pixel, new Vector2(x, y), Color.White);
                    }
                }
            }

            player.Draw(spriteBatch);
        }
    }
}
