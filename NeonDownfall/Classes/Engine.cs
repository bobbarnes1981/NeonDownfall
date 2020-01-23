using Microsoft.Xna.Framework.Graphics;

namespace NeonDownfall.Classes
{
    class Engine
    {
        // 28*20 tiles (8*8)

        Player player;

        public Engine(GraphicsDevice graphicsDevice)
        {
            player = new Player(graphicsDevice);

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
            player.Draw(spriteBatch);
        }
    }
}
