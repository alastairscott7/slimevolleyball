using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Entities
{
    class Ground:GameEntity
    {
        //position will always stay the same as ground has no velocity
        public override void GameTick(float millisecondsElapsed)
        {
            //Position = Position + Velocity * millisecondsElapsed / 1000f; //f means float
        }
        
        //draw ground as rectangle
        public override void Draw(WriteableBitmap surface)
        {
            surface.FillRectangle((int)Position.X, (int)Position.Y, 800, 600, Colors.Tan);
            surface.DrawLine((int)Position.X, (int)Position.Y, 800, (int)Position.Y, Colors.Black);
            base.Draw(surface);
        }
       
        public override bool Collision(GameEntity entity)
        {
            return false;
        }

        //empty as ground is flat
        public override void RoundObjectsCollide(GameEntity entity)
        {

        }

        //returns 0 as ground has no diameter
        public override float GetDiameter()
        {
            return 0f;
        }

    }
}
