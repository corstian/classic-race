using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Classic_Race
{
    class GameVisuals
    {
        public void Draw(string page)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (_page == "menu")
            {
                _spriteBatch.DrawString(_font, "Touch the screen", new Vector2(10, 200), Color.Gray);
                _spriteBatch.DrawString(_font, "to start the game.", new Vector2(10, 240), Color.Gray);
            }
            else
            {
                _spriteBatch.Draw(_car, _carPosition, Color.White);
                _spriteBatch.Draw(_sideLine, _leftLine, Color.White);
                _spriteBatch.Draw(_sideLine, _rightLine, Color.White);
                foreach (Vector2 vector in _traffic)
                {
                    _spriteBatch.Draw(_car, vector, Color.White);
                }
                _spriteBatch.DrawString(_font, "Speed " + _speed, new Vector2(70, 10), Color.Black);
                if (_collison == true)
                {
                    _spriteBatch.DrawString(_font, "collision", new Vector2(70, 200), Color.Red);
                    _collison = false;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
