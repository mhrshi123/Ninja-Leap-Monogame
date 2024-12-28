using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Platformer_GroupAssignment
{
    // Class to manage the background layers
    public class BackgroundManager
    {
        private readonly List<ParallaxLayer> _layers;

        public BackgroundManager()
        {
            _layers = new();
        }

        // Method to add a layer to the background
        public void AddLayer(ParallaxLayer layer)
        {
            _layers.Add(layer);
        }


        // Method to update the background layers based on movement
        public void Update(float movement)
        {
            foreach (var layer in _layers)
            {
                layer.Update(movement);
            }
        }

        // Method to draw the background layers
        public void Draw()
        {
            foreach (var layer in _layers)
            {
                layer.Draw();
            }
        }
    }
}
