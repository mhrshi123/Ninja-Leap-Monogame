using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_GroupAssignment
{
    // Base class for all sprites in the game
    public class Sprite
    {
        public Texture2D Texture { get; }
        public Vector2 position;

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            this.position = position;
        }

        // Draw the sprite
        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, position, null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
