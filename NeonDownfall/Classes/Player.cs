using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            animations = new Animation[10];
            speeds = new int[10];

            //standing
            animations[(int)PlayerState.StandRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, 2, 0, (byte)(0xFF / 2), 0, 15),
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2()
                },
                1 / 2.0
            );
            speeds[(int)PlayerState.StandRight] = 0;
            animations[(int)PlayerState.StandLeft] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, 2, 0, (byte)(0xFF / 2), 0, 0),
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2()
                },
                1 / 2.0
            );
            speeds[(int)PlayerState.StandLeft] = 0;

            //turn
            animations[(int)PlayerState.TurnRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, 4, (byte)(0xFF / 4), (byte)(0xFF / 4), 0, 15),
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2()
                },
                1 / 2.0
            );
            speeds[(int)PlayerState.TurnRight] = 0;
            animations[(int)PlayerState.TurnLeft] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, 4, (byte)(0xFF / 4), (byte)(0xFF / 4), 0, 0),
                new Vector2[]
                {
                    new Vector2(),
                    new Vector2(),
                    new Vector2(),
                    new Vector2()
                },
                1 / 2.0
            );
            speeds[(int)PlayerState.TurnLeft] = 0;

            //walk
            animations[(int)PlayerState.WalkRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, (byte)(0xFF / width), 0, 0, 15),
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
            animations[(int)PlayerState.WalkLeft] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, (byte)(0xFF / width), 0, 0, 0),
                new Vector2[]
                {
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                    new Vector2(-1, 0),
                },
                0.25 / ((float)width)
            );
            speeds[(int)PlayerState.WalkLeft] = 64;

            //right run
            animations[(int)PlayerState.RunRight] = new Animation(
                generateTextureArray(graphicsDevice, width, height, Color.Black, width, 0, 0, (byte)(0xFF / width), 15),
                new Vector2[]
                {
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                    new Vector2(2, 0),
                },
                0.25 / ((float)width)
            );
            speeds[(int)PlayerState.RunRight] = 128;
        }

        private Texture2D[] generateTextureArray(GraphicsDevice graphicsDevice, int width, int height, Color color, int number, byte rStep, byte gStep, byte bStep, int line)
        {
            Texture2D[] textures = new Texture2D[number];

            for (int j = 0; j < number; j++)
            {
                Texture2D texture = new Texture2D(graphicsDevice, width, height);
                Color[] colors = new Color[width * height];

                for (int i = 0; i < colors.Length; i++)
                {
                    Color currentColor = new Color(
                        color.R + (byte)(rStep * j),
                        color.G + (byte)(gStep * j),
                        color.B + (byte)(bStep * j),
                        255
                    );

                    colors[i] = currentColor;
                }
                for (int k = line; k < colors.Length; k+=16)
                {
                    colors[k] = Color.Yellow;
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
                    else if (state.IsKeyDown(Keys.Left))
                    {
                        currentState = PlayerState.TurnLeft;
                        animations[(int)currentState].Reset();
                    }

                    break;

                    // TODO: combine with code for StandRight
                case PlayerState.StandLeft:

                    if (state.IsKeyDown(Keys.Left))
                    {
                        if (state.IsKeyDown(Keys.LeftShift))
                        {
                            currentState = PlayerState.RunLeft;
                            animations[(int)currentState].Reset();
                        }
                        else
                        {
                            currentState = PlayerState.WalkLeft;
                            animations[(int)currentState].Reset();
                        }
                    }
                    else if (state.IsKeyDown(Keys.Right))
                    {
                        currentState = PlayerState.TurnRight;
                        animations[(int)currentState].Reset();
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

                case PlayerState.WalkLeft:

                    if (animations[(int)currentState].End)
                    {
                        currentState = PlayerState.StandLeft;
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

                case PlayerState.TurnLeft:

                    if (animations[(int)currentState].End)
                    {
                        currentState = PlayerState.StandLeft;
                    }
                    else
                    {
                    }

                    break;

                    // combine with above?
                case PlayerState.TurnRight:

                    if (animations[(int)currentState].End)
                    {
                        currentState = PlayerState.StandRight;
                    }
                    else
                    {
                    }

                    break;

                default:

                    throw new Exception(string.Format("Unhandled state {0}", currentState));
            }

            position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animations[(int)currentState].GetFrame(), position, Color.White);
        }
    }
}
