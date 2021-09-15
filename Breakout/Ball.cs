using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace Breakout
{
    public class Ball
    {
        public Sprite sprite;
        public const float Diameter = 20.0f;
        public const float Radius = Diameter * 0.5f;
        public int Health = 3;
        public int Score = 0;
        public Text gui;
        public Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        public int hitCount = 0;
        

        public Ball()
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/ball.png");
            sprite.Position = new Vector2f(250, 400);
            Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);
            gui = new Text();
            gui.CharacterSize = 24;
            gui.Font = new Font("assets/future.ttf");
        }

        public bool BallOnPaddle;
        public void Update(float deltaTime)
        {
            if (!BallOnPaddle)
            {
                var newPos = sprite.Position;
                newPos += direction * deltaTime * 300.0f;
                sprite.Position = newPos;
                if (newPos.X > Program.ScreenW - Radius)
                {
                    newPos.X = Program.ScreenW - Radius;
                    Reflect(new Vector2f(-1,0));
                }
                else if (newPos.Y > Program.ScreenH - Radius)
                {
                    newPos.Y = Program.ScreenH - Radius;
                    Health--;
                    hitCount = 0;
                    sprite.Position = new Vector2f(250, 400);
                    if (new Random().Next() % 2 == 0)
                    {
                        direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
                    }
                    else
                    {
                        direction = new Vector2f(-1, 1) / MathF.Sqrt(2.0f);

                    }

                    BallOnPaddle = true;

                }
                else if (newPos.X < 0 + Radius)
                {
                    newPos.X = 0 + Radius;
                    Reflect(new Vector2f(1,0));
                }
                else if (newPos.Y < 0 + Radius)
                {
                    newPos.Y = 0 + Radius;
                    Reflect(new Vector2f(0,1));
                }

            }
            
            
        }

        public void Reflect(Vector2f normal)
        {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);

            gui.DisplayedString = $"Health: {Health}";
            gui.Position = new Vector2f(12, 8);
            target.Draw(gui);
            gui.DisplayedString = $"Score: {Score}";
            gui.Position = new Vector2f(Program.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
            target.Draw(gui);
        }
    }
}