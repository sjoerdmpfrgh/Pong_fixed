using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection;

class Paddle
{
    Texture2D paddle;
    Vector2 pos, or;
    float speed, yDir;
    string player;
    Color paddleColor;

    public Paddle(ContentManager Content, string player)
    {
        paddle = Content.Load<Texture2D>("paddle");
        this.player = player; 
        or = new Vector2(paddle.Width, paddle.Height) / 2;
        speed = 300.0f;
        Reset();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if( ( (player == "one" && inputHelper.KeyDown(Keys.W))
              ||
              (player == "two" && inputHelper.KeyDown(Keys.Up)) )
              && pos.Y > or.Y
            )
        {
            
            if(Pong.GameWorld.abilityInUse && Pong.GameWorld.abilityGenerated == 0 && Pong.GameWorld.playerAffected == player)
            {
                yDir = -speed * 1.5f;
            }
            else 
                yDir = -speed;
            
        }
        else if ( ( (player == "one" && inputHelper.KeyDown(Keys.S))
                    ||
                    (player == "two" && inputHelper.KeyDown(Keys.Down)) )
                    && pos.Y < Pong.ScreenSize.Y - or.Y
                )
        {
            if(Pong.GameWorld.abilityInUse && Pong.GameWorld.abilityGenerated == 0 && Pong.GameWorld.playerAffected == player)
            {
                yDir = speed * 1.5f;
            }
            else
                yDir = speed;
        }
        else
        {
            yDir = 0.0f;
        }
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        pos.Y += yDir * dt;
        if (Pong.GameWorld.abilityInUse && Pong.GameWorld.playerAffected == player)
        {
            if(Pong.GameWorld.abilityGenerated == 0)
                paddleColor = new Color (0, 0, 255);
            else
                paddleColor = new Color (255, 0, 0);
        }
        else
        {
            paddleColor = new Color(255, 255, 255);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(paddle, pos, null, paddleColor, 0f, or, 1.0f, SpriteEffects.None, 0);
    }

    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = paddle.Bounds;
            spriteBounds.Offset(pos - or);
            return spriteBounds;
        }
    }

    public void Reset()
    {
        if (player == "one")
        {
            pos = new Vector2(paddle.Width / 2.0f, Pong.ScreenSize.Y / 2.0f);
        }
        else
        {
            pos = new Vector2(Pong.ScreenSize.X - paddle.Width / 2.0f, Pong.ScreenSize.Y / 2.0f);
        }
    }

}