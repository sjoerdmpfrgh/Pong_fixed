using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection;

class GameWorld
{
    Ball ball;
    Paddle paddle1, paddle2;
    public PowerUp powerUp;
    int playPhase = 0; //0 = opening screen, 1 = playing, 2 = game over
    //public static Random r2 = new Random();
    public int roundsTillNextAbility = 6;
    public int score1 = 0;
    public int score2 = 0;
    public int roundsLeftOfAbility = 3;
    public int spawnSide;
    public bool resetPowerUps = false;
    public int abilityGenerated; //0 = speed, 1 = strength
    public string playerAffected;
    public bool abilitySpawned = false;
    public bool abilityInUse = false;
    string displayText = "Press Space to Start"; 
    
    SpriteFont font1, font2, font3;
    Vector2 font1Pos, font2Pos, font3Pos, font4Pos;

    Texture2D instructions1, instructions2;
    Vector2 instructions1Pos = Vector2.Zero;
    Vector2 instructions2Pos = Vector2.Zero;
    
    //public int Score1 { set {  score1 = value; } }
    //public int Score2 { set { score1 = value; } }
    public Paddle PaddleL {  get { return paddle1; } }
    public Paddle PaddleR { get { return paddle2; } }

    public GameWorld(ContentManager Content)
    {
        ball = new Ball(Content);
        paddle1 = new Paddle(Content, "one");
        paddle2 = new Paddle(Content, "two"); 
        powerUp = new PowerUp(Content);
       

        font1 = Content.Load<SpriteFont>("MyMenuFont");
        font2 = Content.Load<SpriteFont>("MyMenuFont2");
        font3 = Content.Load<SpriteFont>("MyMenuFont2Smaller");
        instructions1 = Content.Load<Texture2D>("keysUpDown");
        instructions2 = Content.Load<Texture2D>("ArrowsUpDown");
        font1Pos = new Vector2(Pong.ScreenSize.X / 2 - 50 - font1.MeasureString(score1.ToString()).X / 2, 20);
        font2Pos = new Vector2(Pong.ScreenSize.X / 2 + 50 - font1.MeasureString(score1.ToString()).X / 2, 20);
        font3Pos = new Vector2(Pong.ScreenSize.X / 2 - font2.MeasureString(displayText).X / 2, Pong.ScreenSize.Y - 80);
        font4Pos = new Vector2(Pong.ScreenSize.X / 2 - font3.MeasureString("Get 3 Goals Before Your Opponent Does").X / 2, Pong.ScreenSize.Y - 110);
        instructions1Pos = new Vector2(50, Pong.ScreenSize.Y / 2 - (instructions1.Height / 2));
        instructions2Pos = new Vector2(Pong.ScreenSize.X - instructions2.Width - 50, Pong.ScreenSize.Y / 2 - (instructions2.Height / 2)); 
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (playPhase == 0)
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                playPhase = 1;
            }
        }
        else if (playPhase == 1)
        {
            paddle1.HandleInput(inputHelper);
            paddle2.HandleInput(inputHelper); 
            
            if (inputHelper.KeyPressed(Keys.R))
            {
                ball.Reset();
                powerUp.Reset();
            }
        }
        else
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                score1 = 0;
                score2 = 0;
                playPhase = 0;
                ball.Reset();
                powerUp.Reset();
                paddle1.Reset();
                paddle2.Reset();
            }
        }
    }

    public void Update(GameTime gameTime)
    {

        if (playPhase == 1)
        {
            /*
            if (resetPowerUps)
            {
                powerUp.Reset();
                resetPowerUps = false;
            }
            */
            ball.Update(gameTime);
            paddle1.Update(gameTime);
            paddle2.Update(gameTime);
            powerUp.Update(gameTime);
        }

        if (score1 == 3 || score2 == 3)
        {
            playPhase = 2;
            if (score1 > score2) displayText = "Player 1 has won!";
            else displayText = "Player 2 has won!";
            font3Pos = new Vector2(Pong.ScreenSize.X / 2 - font2.MeasureString(displayText).X / 2, Pong.ScreenSize.X / 2);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        
        if (playPhase != 1)
        {
            if (playPhase == 0)
            {
                spriteBatch.DrawString(font3, "get 3 goals before your opponent does", font4Pos, Color.White);
                spriteBatch.Draw(instructions1, instructions1Pos, Color.White);
                spriteBatch.Draw(instructions2, instructions2Pos, Color.White);
            }


            spriteBatch.DrawString(font2, displayText, font3Pos, Color.White);
        }

        ball.Draw(gameTime, spriteBatch);
        paddle1.Draw(gameTime, spriteBatch);
        paddle2.Draw(gameTime, spriteBatch);
        powerUp.Draw(gameTime, spriteBatch);

        spriteBatch.DrawString(font1, score1.ToString(), font1Pos, Color.White);
        spriteBatch.DrawString(font1, score2.ToString(), font2Pos, Color.White);

        spriteBatch.End();
    }

}
