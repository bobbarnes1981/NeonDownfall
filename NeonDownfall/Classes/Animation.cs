using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeonDownfall.Classes
{
    class Animation
    {
        double speed;
        double elapsed;

        int currentFrame = 0;

        private Texture2D[] frames;
        private Vector2[] movements;

        public Animation(Texture2D[] frames, Vector2[] movements, double speed)
        {
            this.frames = frames;
            this.movements = movements;
            this.speed = speed;
        }

        public bool End
        {
            get
            {
                return currentFrame == 0;
            }
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
                }

                return true;
            }

            return false;
        }

        public Texture2D GetFrame()
        {
            return frames[currentFrame];
        }

        public Vector2 GetMovement()
        {
            return movements[currentFrame];
        }

        public void Reset()
        {
            currentFrame = 0;
            elapsed = 0;
        }
    }
}
