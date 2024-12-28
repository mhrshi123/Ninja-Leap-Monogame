using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    // Represents the player character
    public class Player : Sprite
    {
        // Constants for movement and physics
        private const float SPEED = 750f;
        private const float GRAVITY = 5000f;
        private const float JUMP = 1500f;
        private const int OFFSET = 10;
        private Vector2 _velocity;

        // Whether the player is on the ground
        private bool _onGround;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }


        // Method to calculate the bounds of the Penguin
        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET, (int)pos.Y, Texture.Width - (2 * OFFSET), Texture.Height);
        }


        // Property to get the current bounds of the Penguin
        public Rectangle Bounds => CalculateBounds(position);


        // update the velocity based on user input and gravity
        private void UpdateVelocity()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A)) _velocity.X = -SPEED;
            else if (keyboardState.IsKeyDown(Keys.D)) _velocity.X = SPEED;
            else _velocity.X = 0;

            _velocity.Y += GRAVITY * Globals.Time;

            if (keyboardState.IsKeyDown(Keys.Space) && _onGround)
            {
                _velocity.Y = -JUMP;
            }
        }


        // update the position based on velocity and handle collisions
        private void UpdatePosition()
        {
            _onGround = false;
            var newPos = position + (_velocity * Globals.Time);
            Rectangle newRect = CalculateBounds(newPos);

            foreach (var collider in Map.GetNearestColliders(newRect))
            {
                if (newPos.X != position.X)
                {
                    newRect = CalculateBounds(new(newPos.X, position.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (newPos.X > position.X) newPos.X = collider.Left - Texture.Width + OFFSET;
                        else newPos.X = collider.Right - OFFSET;
                        continue;
                    }
                }

                newRect = CalculateBounds(new(position.X, newPos.Y));
                if (newRect.Intersects(collider))
                {
                    if (_velocity.Y > 0)
                    {
                        newPos.Y = collider.Top - Texture.Height;
                        _onGround = true;
                        _velocity.Y = 0;
                    }
                    else
                    {
                        newPos.Y = collider.Bottom;
                        _velocity.Y = 0;
                    }
                }
            }

            position = newPos;
        }

        // Method to update the Penguin's state
        public void Update()
        {
            UpdateVelocity();
            UpdatePosition();
        }
    }
}
