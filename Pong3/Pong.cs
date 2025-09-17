using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

class Pong : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    InputHelper inputHelper;

    static GameWorld gameWorld;
    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }

    public static Random Random { get; private set; }
    public static Vector2 ScreenSize { get; private set; }

    [STAThread]
    static void Main()
    {
        Pong game = new Pong();
        game.Run();
    }

    public Pong()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        inputHelper = new InputHelper();
        Random = new Random();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        gameWorld = new GameWorld(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}