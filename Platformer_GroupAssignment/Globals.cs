using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    // Class to hold global variables and methods
    public class Globals
    {
        public static float Time { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Point WindowSize { get; set; }


        //updates the global time
        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
