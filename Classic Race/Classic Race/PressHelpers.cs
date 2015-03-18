using Microsoft.Xna.Framework.Input;

namespace Classic_Race
{
    internal class PressHelpers
    {
        private bool _isPressed = false;
        private bool _pressCompleted = false;
        private int _pressX = 0;
        private int _pressY = 0;

        public bool InputHandler(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _isPressed = true;
                _pressX = mouseState.X;
                _pressY = mouseState.Y;
                _pressCompleted = false;
                
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _isPressed = false;
                _pressCompleted = true;
            }
        }

        public bool isPressCompleted()
        {
            return _pressCompleted;
        }

        public bool isPressed()
        {
            return _isPressed;
        }

        public int getPressX()
        {
            return _pressX;
        }

        public int getPressY()
        {
            return _pressY;
        }
    }
}
