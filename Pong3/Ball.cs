using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Reflection;

class Ball
{
    Texture2D ball;
    Vector2 pos, or, vel;
    float speed, speedIncrease;
    Color ballColor = new Color(255, 255, 255);
    int redComponent = 0;
    public static bool ballPassedMiddle = true;

    public Ball(ContentManager Content)
    {
        ball = Content.Load<Texture2D>("ball");
        or = new Vector2(ball.Width, ball.Height) / 2;
        speed = 300.0f;
        speedIncrease = 1.05f;
        Reset();
    }

    public void Update(GameTime gameTime)
    {
        if(!ballPassedMiddle)
        {
            if(Math.Abs(pos.X - Pong.ScreenSize.X / 2) <= 10)
            {
                Debug.WriteLine("Ball passed middle on: " + pos.Y);
                ballPassedMiddle = true;
            }
        }
        
        ballColor = new Color(255, 255 - redComponent * (255 / 20), 255 - redComponent * (255 / 20)); 
        
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        pos += vel * dt;

        if (pos.X < 0 - or.X)
        {
            Pong.GameWorld.score2++;
            Reset();
        }
        else if(pos.X > Pong.ScreenSize.X + or.X)
        {
            Pong.GameWorld.score1++;
            Reset();
        }
        
        if (pos.Y < 0 + or.X || pos.Y > Pong.ScreenSize.Y - or.X)
        {
            vel.Y = -vel.Y;
        }

        if (BoundingBox.Intersects(Pong.GameWorld.Paddle1.BoundingBox))
        {
            double angle = (pos.Y - Pong.GameWorld.Paddle1.pos.Y) / (Pong.GameWorld.Paddle1.paddle.Height) * (2.0f / 3.0f) * Math.PI;
            vel.X = (float)Math.Cos(angle) * speed;
            vel.Y = (float)Math.Sin(angle) * speed;

            vel *= speedIncrease;

            if (redComponent < 20) redComponent++;
            Debug.WriteLine("hello " + pos.Y);
        }
        if (BoundingBox.Intersects(Pong.GameWorld.Paddle2.BoundingBox))
        {
            double angle = (pos.Y - Pong.GameWorld.Paddle2.pos.Y) / (Pong.GameWorld.Paddle2.paddle.Height) * (2.0f / 3.0f * Math.PI);
            vel.X = (float)Math.Cos(angle) * -speed;
            vel.Y = (float)Math.Sin(angle) * speed;

            vel *= speedIncrease;

            if (redComponent < 20) redComponent++;
            Debug.WriteLine("hello " + pos.Y);
        }

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ball, pos, null, ballColor, 0f, or, 1.0f, SpriteEffects.None, 0);
    }

    public void Reset()
    {
        redComponent = 0;
        speed = 300.0f;

        pos = new Vector2(Pong.ScreenSize.X / 2.0f, Pong.ScreenSize.Y / 2.0f);
        double angle = Pong.Random.NextDouble() * 0.5f * Math.PI;
        if(Pong.Random.Next(0, 2) == 0)
        {
            angle = angle - 0.25f * Math.PI;
        }
        else
        {
            angle = angle + 0.75f * Math.PI;
        }
        vel.X = (float)Math.Cos(angle) * speed;
        vel.Y = (float)Math.Sin(angle) * speed;
    }

    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = ball.Bounds;
            spriteBounds.Offset(pos - or);
            return spriteBounds;
        }
    }

}
