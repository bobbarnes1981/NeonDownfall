using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NeonDownfall.Enums;
using System;
using System.Collections.Generic;

namespace NeonDownfall.Classes
{
    class Player
    {
        // 2*4 tiles (8*8)
        int width = 16;
        int height = 32;

        Vector2 position;

        Dictionary<PlayerDirection, Dictionary<PlayerState, Animation>> animations;

        PlayerState state;
        PlayerDirection direction;

        public Player(GraphicsDevice graphicsDevice, Vector2 position)
        {
            state = PlayerState.Stand;
            direction = PlayerDirection.Right;

            this.position = position;
            createPlaceholderTextures(graphicsDevice);
        }

        private void createPlaceholderTextures(GraphicsDevice graphicsDevice)
        {
            // just use plain colour for now

            animations = new Dictionary<PlayerDirection, Dictionary<PlayerState, Animation>>
            {
                { PlayerDirection.Left, new Dictionary<PlayerState, Animation>() },
                { PlayerDirection.Right, new Dictionary<PlayerState, Animation>() },
            };

            //standing
            animations[PlayerDirection.Right].Add(
                PlayerState.Stand,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 2, 0, (byte)(0xFF / 2), 0, 15),
                    new Vector2[]
                    {
                        new Vector2(),
                        new Vector2()
                    },
                    0.5
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Stand,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 2, 0, (byte)(0xFF / 2), 0, 0),
                    new Vector2[]
                    {
                        new Vector2(),
                        new Vector2()
                    },
                    0.5
                )
            );

            //fall
            animations[PlayerDirection.Right].Add(
                PlayerState.Fall,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 2, (byte)(0xFF / 2), 0, (byte)(0xFF / 2), 15),
                    new Vector2[]
                    {
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                        new Vector2(0, 4),
                    },
                    1 / 32.0
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Fall,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 2, (byte)(0xFF / 2), 0, (byte)(0xFF / 2), 0),
                    new Vector2[]
                    {
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                        new Vector2(0, 2),
                    },
                    1 / 32.0
                )
            );

            //turn
            animations[PlayerDirection.Right].Add(
                PlayerState.Turn,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Yellow, 4, 0, 0, 0, 15),
                    new Vector2[]
                    {
                        new Vector2(),
                        new Vector2(),
                        new Vector2(),
                        new Vector2()
                    },
                    1 / 2.0
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Turn,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Yellow, 4, 0, 0, 0, 0),
                    new Vector2[]
                    {
                        new Vector2(),
                        new Vector2(),
                        new Vector2(),
                        new Vector2()
                    },
                    1 / 2.0
                )
            );

            //walk
            animations[PlayerDirection.Right].Add(
                PlayerState.Walk,
                new Animation(
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
                    0.25 / 16f
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Walk,
                new Animation(
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
                    0.25 / 16f
                )
            );

            //run
            animations[PlayerDirection.Right].Add(
                PlayerState.Run,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 16, 0, 0, (byte)(0xFF / width), 15),
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
                    0.25 / 16f
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Run,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.Black, 16, 0, 0, (byte)(0xFF / width), 0),
                    new Vector2[]
                {
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                    new Vector2(-2, 0),
                },
                    0.25 / 16f
                )
            );

            //jump
            animations[PlayerDirection.Right].Add(
                PlayerState.Jump,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.CornflowerBlue, 16, 0, 0, (byte)(0xFF / width), 15),
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
                    0.25 / 16f
                )
            );
            animations[PlayerDirection.Left].Add(
                PlayerState.Jump,
                new Animation(
                    generateTextureArray(graphicsDevice, width, height, Color.CornflowerBlue, 16, 0, 0, (byte)(0xFF / width), 0),
                    new Vector2[]
                    {
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                        new Vector2(-2, 0),
                    },
                    0.25 / 16f
                )
            );
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

        private void checkStand(KeyboardState kb)
        {
            switch (direction)
            {
                case PlayerDirection.Left:

                    if (kb.IsKeyDown(Keys.Left))
                    {
                        if (kb.IsKeyDown(Keys.LeftShift))
                        {
                            state = PlayerState.Run;
                            animations[direction][state].Reset();
                        }
                        else
                        {
                            state = PlayerState.Walk;
                            animations[direction][state].Reset();
                        }
                    }
                    else if (kb.IsKeyDown(Keys.Right))
                    {
                        state = PlayerState.Turn;
                        direction = PlayerDirection.Right;
                        animations[direction][state].Reset();
                    }
                    else if (kb.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Up;
                        animations[direction][state].Reset();
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        state = PlayerState.Jump;
                        animations[direction][state].Reset();
                    }

                    break;

                case PlayerDirection.Right:

                    if (kb.IsKeyDown(Keys.Right))
                    {
                        if (kb.IsKeyDown(Keys.LeftShift))
                        {
                            state = PlayerState.Run;
                            animations[direction][state].Reset();
                        }
                        else
                        {
                            state = PlayerState.Walk;
                            animations[direction][state].Reset();
                        }
                    }
                    else if (kb.IsKeyDown(Keys.Left))
                    {
                        state = PlayerState.Turn;
                        direction = PlayerDirection.Left;
                        animations[direction][state].Reset();
                    }
                    else if (kb.IsKeyDown(Keys.Up))
                    {
                        state = PlayerState.Up;
                        animations[direction][state].Reset();
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        state = PlayerState.Jump;
                        animations[direction][state].Reset();
                    }

                    break;

                default:

                    throw new Exception(string.Format("Unhandled direction {0}", direction));
            }
        }

        public void checkWalk(KeyboardState kb)
        {
            if (animations[direction][state].End)
            {
                state = PlayerState.Stand;
                animations[direction][state].Reset();
            }
            else
            {
            }
        }

        public void checkRun(KeyboardState kb)
        {
            if (animations[direction][state].End)
            {
                state = PlayerState.Stand;
            }
            else
            {
            }
        }

        public void checkTurn(KeyboardState kb)
        {
            if (animations[direction][state].End)
            {
                state = PlayerState.Stand;
            }
            else
            {
            }
        }

        public void checkFall(KeyboardState kb)
        {
            // nothing until detected collision with ground
        }

        public void checkJump(KeyboardState kb)
        {
            if (animations[direction][state].End)
            {
                state = PlayerState.Stand;
            }
            else
            {
            }
        }

        public void Update(double elapsedSeconds, Page page)
        {
            // TODO: allow key/pad mapping
            var kb = Keyboard.GetState();

            Vector2 movement = new Vector2();
            if (animations[direction][state].Update(elapsedSeconds))
            {
                movement = animations[direction][state].GetMovement();
            }

            Vector2 newPosition = position + movement;
            if (page.Collision(new Rectangle(newPosition.ToPoint(), new Point(width, height))))
            {
                movement = new Vector2();
            }

            if (state != PlayerState.Jump)
            {
                Vector2 gravityCheck = position + new Vector2(0, 1);
                if (page.Collision(new Rectangle(gravityCheck.ToPoint(), new Point(width, height))))
                {
                    if (state == PlayerState.Fall)
                    {
                        state = PlayerState.Stand;
                    }
                }
                else
                {
                    state = PlayerState.Fall;
                }
            }

            switch (state)
            {
                case PlayerState.Stand:
                    checkStand(kb);
                    break;

                case PlayerState.Walk:
                    checkWalk(kb);
                    break;

                case PlayerState.Run:
                    checkRun(kb);
                    break;

                case PlayerState.Turn:
                    checkTurn(kb);
                    break;

                case PlayerState.Fall:
                    checkFall(kb);
                    break;

                case PlayerState.Jump:
                    checkJump(kb);
                    break;

                default:
                    throw new Exception(string.Format("Unhandled state {0}", state));
            }

            position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animations[direction][state].GetFrame(), position, Color.White);
        }
    }
}
