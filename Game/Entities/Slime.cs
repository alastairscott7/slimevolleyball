using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Entities
{
    class Slime : GameEntity
    {
        public float Diameter { get; set; }

        //needs to change
        public override void GameTick(float millisecondsElapsed)
        {
            Position = Position + Velocity * millisecondsElapsed / 1000f;
        }

        public override void Draw(WriteableBitmap surface)
        {
            surface.FillEllipse((int)Position.X, (int)Position.Y, (int)(Position.X + Diameter), (int)(Position.Y + Diameter), Colors.LightGreen);
            base.Draw(surface);
        }

        public override bool Collision(GameEntity entity)
        {
            float x1 = Position.X + (Diameter / 2);
            float y1 = Position.Y + (Diameter / 2);
            float x2 = entity.Position.X + (entity.GetDiameter() / 2);
            float y2 = entity.Position.Y + (entity.GetDiameter() / 2);

            float r1 = Diameter / 2;
            float r2 = entity.GetDiameter() / 2;
            float radius = r1 + r2;

            float dX = x2 - x1;
            float dY = y2 - y1;

            if (Math.Sqrt((dX * dX) + (dY * dY)) <= Math.Sqrt(radius * radius))
                return true;
            else
                return false;

            //base.Collision(entity);
        }

        public override float GetDiameter()
        {
            return Diameter;
        }
    }
}