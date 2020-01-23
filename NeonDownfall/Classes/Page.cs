using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeonDownfall.Classes
{
    class Page
    {
        int tilesX = 28;
        int tilesY = 20;

        int tileSize = 8;

        Texture2D pixel;
        Texture2D hitbox;

        Rectangle[] objects;

        public Page(GraphicsDevice graphicsDevice, int tilesX, int tilesY, int tileSize, Color color, Rectangle[] objects)
        {
            this.tilesX = tilesX;
            this.tilesY = tilesY;
            this.tileSize = tileSize;

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { color });

            hitbox = new Texture2D(graphicsDevice, 1, 1);
            hitbox.SetData(new Color[] { Color.Red });

            this.objects = objects;
        }

        public bool Collision(Rectangle hitbox)
        {
            foreach (Rectangle obj in objects)
            {
                if (hitbox.Intersects(obj))
                {
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {

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

            foreach (Rectangle obj in objects)
            {
                for (int y = 0; y < obj.Height; y++)
                {
                    for (int x = 0; x < obj.Width; x++)
                    {
                        spriteBatch.Draw(hitbox, new Vector2(x + obj.Left, y + obj.Top), Color.White);
                    }
                }
            }
        }
    }
}
