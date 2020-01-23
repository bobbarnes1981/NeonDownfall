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
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2()
                },
                1 / 2.0
            );
            speeds[(int)PlayerState.StandRight] = 0;

            //right walk
            animations[(int)PlayerState.WalkRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, (byte)(0xFF / width), 0, 0),
                new Vector2[]
                {
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 0),
                },
                0.25 / ((float)width)
            );
            speeds[(int)PlayerState.WalkRight] = 64;
            
            //right run
            animations[(int)PlayerState.RunRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, 0, 0, (byte)(0xFF / width)),
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2()
                },
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

            Vector2 movement = new Vector2();
            if (animations[(int)currentState].Update(elapsedSeconds))
            {
                movement = animations[(int)currentState].GetMovement();
            }

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
                    }

                    break;

                case PlayerState.WalkRight:
                    if (animations[(int)currentState].End)
                    {
                        currentState = PlayerState.StandRight;
                    }
                    else
                    {
                    }

                    break;

                case PlayerState.RunRight:
                    if (animations[(int)currentState].End)
                    {
                        currentState = PlayerState.StandRight;
                    }
                    else
                    {
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
