using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    //Represents parallax layer for background scrolling
    public class ParallaxLayer
    {
        private readonly Texture2D _texture;
        private Vector2 _position = Vector2.Zero;
        private Vector2 _position2 = Vector2.Zero;
        private readonly float _depth;
        private readonly float _moveScale;
        private readonly float _defaultSpeed;
        private Vector2 _scale;

        // Method to update the position of the parallax layer based on movement
        public ParallaxLayer(Texture2D texture, float depth, float moveScale, float defaultSpeed = 0.0f)
        {
            _texture = texture;
            _depth = depth;
            _moveScale = moveScale;
            _defaultSpeed = defaultSpeed;
            _scale = new Vector2(1.8f, 1.8f); // Adjust scale as needed

        }

        // Method to update the position of the parallax layer based on movement
        public void Update(float movement)
        {
            _position.X += ((movement * _moveScale) + _defaultSpeed) * Globals.Time;
            _position.X %= _texture.Width;

            if (_position.X >= 0)
            {
                _position2.X = _position.X - _texture.Width;
            }
            else
            {
                _position2.X = _position.X + _texture.Width;
            }
        }

        // draw the parallax layer
        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, _depth);
            Globals.SpriteBatch.Draw(_texture, _position2, null, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, _depth);
        }
    }

}
