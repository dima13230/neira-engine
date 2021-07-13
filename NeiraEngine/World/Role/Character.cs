using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;


namespace NeiraEngine.World.Role
{
    class Character : ControllableWorldObject
    {


        public Character(string id, SpatialData spatial_data, float movement_speed_walk, float movement_speed_run, Scene scene = null)
            : base (id, spatial_data, scene, movement_speed_walk, movement_speed_run)
        { }


    }
}
