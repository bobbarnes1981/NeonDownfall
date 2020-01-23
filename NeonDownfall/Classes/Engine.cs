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
        Page page;

        public Engine(GraphicsDevice graphicsDevice)
        {
            player = new Player(graphicsDevice, new Vector2(32, 0));

            // TODO: page designer (restrict to 16 pixels)
            Rectangle[] objects = new Rectangle[]
            {
                new Rectangle(new Point(12, 0), new Point(4, 32)),
                new Rectangle(new Point(96, 0), new Point(4, 32)),
                new Rectangle(new Point(16, 32), new Point(48, 4)),
                new Rectangle(new Point(80, 32), new Point(16, 4)),
                new Rectangle(new Point(32, 96), new Point(64, 4)),
            };
            page = new Page(graphicsDevice, tilesX, tilesY, tileSize, Color.DarkGray, objects);

            //page = new Page(graphicsDevice, tilesX, tilesY, tileSize, Color.DarkGray, new Rectangle[] { new Rectangle(new Point(0, 96), new Point(190, 4)) });

            // TODO: states for title/game/gameover
            // TODO: show title screen
            // TODO: menus, also maybe nice in game menus
        }

        public void Update(double elapsedSeconds)
        {
            player.Update(elapsedSeconds, page);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            page.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }
    }
}
