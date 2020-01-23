using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NeonDownfall.Classes
{
    class Player
    {
        Vector2 position;

        Animation[] animations;
        int[] speeds;

        PlayerState currentState;

        public Player(GraphicsDevice graphicsDevice)
        {
            currentState = PlayerState.StandRight;

            // 2*4 tiles (8*8)
            int width = 16;
            int height = 32;

            position = new Vector2(16, 0);

            // just use plain colour for now

            animations = new Animation[4];
            speeds = new int[4];

            //standing
            animations[(int)PlayerState.StandRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, 2, 0, (byte)(0xFF / 2), 0),
                1 / 2.0
            );
            speeds[(int)PlayerState.StandRight] = 0;

            //right walk
            animations[(int)PlayerState.WalkRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, (byte)(0xFF / width), 0, 0),
                0.25 / ((float)width)
            );
            speeds[(int)PlayerState.WalkRight] = 64;
            
            //right run
            animations[(int)PlayerState.RunRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, 0, 0, (byte)(0xFF / width)),
                0.25 / ((float)width)
            );
            speeds[(int)PlayerState.RunRight] = 128;
        }

        private Texture2D[] generateTextureArray(GraphicsDevice graphicsDevice, int width, int height, Color color, int number, byte rStep, byte gStep, byte bStep)
        {
            Texture2D[] textures = new Texture2D[number];

            for (int j = 0; j < number; j++)
            {
                Texture2D texture = new Texture2D(graphicsDevice, width, height);
                Color[] colors = new Color[width * height];

                Color currentColor = new Color(
                    color.R + (byte)(rStep * j),
                    color.G + (byte)(gStep * j),
                    color.B + (byte)(bStep * j),
                    0xFF
                );
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = currentColor;
                }
                texture.SetData(colors);

                textures[j] = texture;
            }

            return textures;
        }

        public void Update(double elapsedSeconds)
        {
            // TODO: allow key/pad mapping
            var state = Keyboard.GetState();

            // TODO: pixel perfect movement

            bool finished = animations[(int)currentState].Update(elapsedSeconds);

            Vector2 movement = new Vector2();

            switch (currentState)
            {
                case PlayerState.StandRight:

                    if (state.IsKeyDown(Keys.Right))
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            currentState = PlayerState.RunRight;
                            animations[(int)currentState].Reset();
                        }
                        else
                        {
                            currentState = PlayerState.WalkRight;
                            animations[(int)currentState].Reset();
                        }

                        movement.X += (float)(speeds[(int)currentState] * elapsedSeconds); // todo: should be correct distance
                    }

                    break;

                case PlayerState.WalkRight:
                    if (finished)
                    {
                        currentState = PlayerState.StandRight;
                    }
                    else
                    {
                        movement.X += (float)(speeds[(int)currentState] * elapsedSeconds); // todo: should be correct distance
                    }

                    break;

                case PlayerState.RunRight:
                    if (finished)
                    {
                        currentState = PlayerState.StandRight;
                    }
                    else
                    {
                        movement.X += (float)(speeds[(int)currentState] * elapsedSeconds); // todo: should be correct distance
                    }

                    break;
            }

            position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animations[(int)currentState].GetFrame(), position, Color.White);
        }
    }
}
