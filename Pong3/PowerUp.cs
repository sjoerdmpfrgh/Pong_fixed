using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
class PowerUp
{
    Texture2D powerUp;
    Vector2 powPos, or;
    Random Random = new Random();
    Paddle paddletoDealWith;
    bool properlySpawned = false;
    Color abilityColor = new Color(0, 0, 0);

    public PowerUp(ContentManager Content)
    {
        powerUp = Content.Load<Texture2D>("abilitySquareBig");
        or = new Vector2(powerUp.Width, powerUp.Height) / 2;
    }
    public void Update(GameTime gameTime)
    {
        if(Pong.GameWorld.roundsLeftOfAbility <= 0)
        {
            Pong.GameWorld.roundsLeftOfAbility = 3;
            Pong.GameWorld.roundsTillNextAbility = Random.Next(3, 6);
            Pong.GameWorld.abilityInUse = false;
        }


        if (!Pong.GameWorld.abilityInUse && Pong.GameWorld.roundsTillNextAbility <= 0 && !properlySpawned)
        {
            Pong.GameWorld.spawnSide = Random.Next(0, 2); 
            if(Pong.GameWorld.spawnSide == 0)
            {
                powPos.X = 0;
                paddletoDealWith = Pong.GameWorld.PaddleL;
                Pong.GameWorld.playerAffected = "one";
            } 
            else
            {
                powPos.X = (int)Pong.ScreenSize.X - powerUp.Width;
                paddletoDealWith = Pong.GameWorld.PaddleR;
                Pong.GameWorld.playerAffected = "two";
            }
            
            Pong.GameWorld.abilityGenerated = Random.Next(0, 2); //0 = speed, 1 = strength
            if(Pong.GameWorld.abilityGenerated == 0)
                abilityColor = new Color(0, 0, 255);
            else
                abilityColor = new Color(255, 0, 0);
            

            while (!properlySpawned)
            {
                powPos.Y = Random.Next(0 + powerUp.Height, (int)Pong.ScreenSize.Y);
                if (BoundingBox.Intersects(paddletoDealWith.BoundingBox))
                {
                    powPos.Y = Random.Next(0 + powerUp.Height, (int)Pong.ScreenSize.Y);
                }
                else
                {
                    properlySpawned = true;
                    Debug.WriteLine("Ability Spawned at: " + powPos);
                }
            }
        }

        if (properlySpawned) 
        {
            if (BoundingBox.Intersects(paddletoDealWith.BoundingBox))
            {
                Pong.GameWorld.abilityInUse = true;
                properlySpawned = false;
            }
        }
       
    }
    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = powerUp.Bounds;
            spriteBounds.Offset(powPos - or);
            return spriteBounds;
        }
    }

    public void Reset()
    {
        properlySpawned = false;
        Pong.GameWorld.abilityInUse = false;
        Pong.GameWorld.roundsTillNextAbility = Random.Next(3, 6);
        Pong.GameWorld.roundsLeftOfAbility = 3;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!Pong.GameWorld.abilityInUse && Pong.GameWorld.roundsTillNextAbility == 0)
        {
            spriteBatch.Draw(powerUp, powPos, abilityColor);
        }

    }
}

