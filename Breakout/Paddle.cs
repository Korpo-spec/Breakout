using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Breakout
{
    public class Paddle
    {
        public Sprite sprite;
        public Vector2f size;
        

        public Paddle()
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/paddle.png");
            Vector2f paddleTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Position = new Vector2f(Program.ScreenW * 0.5f, Program.ScreenH - 150);
            sprite.Origin = 0.5f * paddleTextureSize;
            sprite.Scale = new Vector2f(paddleTextureSize.Y / paddleTextureSize.X ,paddleTextureSize.Y / paddleTextureSize.X);
            size= new Vector2f(sprite.GetGlobalBounds().Width,sprite.GetGlobalBounds().Height);
        }

        public void Update(float deltaTime, Ball ball)
        {
            
            if (ball.BallOnPaddle)
            {
                sprite.Position = new Vector2f(Program.ScreenW * 0.5f, Program.ScreenH - 150);
                Vector2f temp = sprite.Position;
                temp.Y -= 20;
                ball.sprite.Position = temp;
                
                
            }
            var newPos = sprite.Position;
            
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                {
                    newPos.X += deltaTime * 300.0f;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                {
                    newPos.X -= deltaTime * 300.0f;
                } // TODO: Moveback if outside
            
            
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    ball.BallOnPaddle = false;
                }
            
            if (newPos.X < 0 + (int)size.X /2)
            {
                newPos.X = 0 + (int)size.X/2;
            }
            else if (newPos.X > Program.ScreenW - (int) size.X / 2)
            {
                newPos.X = 0 + Program.ScreenW - (int) size.X / 2;
            }
            sprite.Position = newPos;
            
           
            if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, this.sprite.Position, size,
                out Vector2f hit))
            {
                ball.sprite.Position += hit;
                ball.hitCount = 0;
                ball.Reflect(hit.Normalized());
            }
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}