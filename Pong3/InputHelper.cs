using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class InputHelper
{
    KeyboardState currentKeyboardState, previousKeyboardState;

    public void Update()
    {
        previousKeyboardState = currentKeyboardState;
        currentKeyboardState = Keyboard.GetState();
    }

    public bool KeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && !previousKeyboardState.IsKeyDown(k);
    }

}
