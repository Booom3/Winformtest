using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subpixelprogramming
{
    class MovingPixel : Pixel, IUpdate
    {
        public Position velocity;
        public MovingPixel(Position Pos, Position Velocity) : base(Pos)
        {
            velocity = Velocity;
        }

        public void Update()
        {
            pos.x += velocity.x;
            pos.y += velocity.y;
            pos.z += velocity.z;
        }
    }
}
