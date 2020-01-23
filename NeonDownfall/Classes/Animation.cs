using Microsoft.Xna.Framework.Graphics;

namespace NeonDownfall.Classes
{
    class Animation
    {
        double speed;
        double elapsed;

        int currentFrame = 0;

        private Texture2D[] frames;

        public Animation(Texture2D[] frames, double speed)
        {
            this.frames = frames;
            this.speed = speed;
        }

        public bool Update(double elapsedSeconds)
        {
            elapsed += elapsedSeconds;

            if (elapsed >= speed)
            {
                elapsed -= speed;

                currentFrame++;
                if (currentFrame >= frames.Length)
                {
                    currentFrame = 0;

                    return true;
                }
            }

            return false;
        }

        public Texture2D GetFrame()
        {
            return frames[currentFrame];
        }

        public void Reset()
        {
            currentFrame = 0;
            elapsed = 0;
        }
    }
}
