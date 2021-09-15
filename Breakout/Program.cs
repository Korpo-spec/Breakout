using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    class Program
    {
        public const int ScreenW = 500;
        public const int ScreenH = 700;
        static void Main(string[] args)
        {
            
            using (var window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "breakout"))
            {
                
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    Ball ball = new Ball();
                    Paddle paddle = new Paddle();
                    Tiles tile = new Tiles();
                    while (ball.Health > 0 && tile.positions.Count > 0)
                    {
                        float deltaTime = clock.Restart().AsSeconds();
                        window.DispatchEvents(); 
                        //TODO: Updates
                        ball.Update(deltaTime);
                        paddle.Update(deltaTime,ball);
                        tile.Update(deltaTime, ball);
                        
                        window.Clear(new Color(131, 197, 235));
                        // TODO: Drawing
                        ball.Draw(window);
                        paddle.Draw(window);
                        tile.Draw(window);
                        window.Display();
                    
                        
                    
                    }
                }
                
                
            }
        }
    }
}

    
