using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Breakout
{
    public class Tiles
    {
        public Sprite sprite;
        public Vector2f size;
        public List<Vector2f> positions;
        

        public Tiles()
        {
           
            
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/tileblue.png");
            Vector2f tileTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Position = new Vector2f(Program.ScreenW * 0.5f, Program.ScreenH - 250);
            sprite.Origin = 0.5f * tileTextureSize;
            sprite.Scale = new Vector2f((tileTextureSize.Y / tileTextureSize.X)*0.7f ,(tileTextureSize.Y / tileTextureSize.X) *0.7f);
            size= new Vector2f(sprite.GetGlobalBounds().Width,sprite.GetGlobalBounds().Height);
            
            positions = new List<Vector2f>();
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    var pos = new Vector2f(Program.ScreenW * 0.5f + i * 96.0f, Program.ScreenH * 0.3f + j * 48.0f);
                    positions.Add(pos);
                }
            }
        }

        
        public void Update(float deltaTime, Ball ball)
        {
            
            for (int i = 0; i < positions.Count; i++)
            {
                var pos = positions[i];
                if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, pos, size, out Vector2f hit))
                {
                    ball.sprite.Position += hit;
                    ball.Reflect(hit.Normalized());
                    positions.RemoveAt(i);
                    ball.hitCount++;
                    ball.Score+= 100 * ball.hitCount;
                    i = 0; 
                }
                
            }
        }

        public void Draw(RenderTarget target)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                sprite.Position = positions[i];
                target.Draw(sprite);
            }
        }
    }
}