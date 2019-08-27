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
        const float ONGROUND = 450f;
        const float LEFTWALL = 0f;
        const float LEFTNET = 390f;
        const float RIGHTNET = 410f;
        const float RIGHTWALL = 784f;

        public override void GameTick(float millisecondsElapsed)
        {
            Position = Position + Velocity * millisecondsElapsed / 1000f;

            //Left Slime Limits
            if(Position.X < LEFTWALL)
            {
                Position -= new System.Numerics.Vector2(Position.X, 0);
            }
            else if(Position.X >= (LEFTNET-Diameter) && Position.X < (RIGHTNET-Diameter))
            {
                Position -= new System.Numerics.Vector2(Position.X - (LEFTNET - Diameter), 0);
            }

            if(Position.X > RIGHTWALL - Diameter)
            {
                Position -= new System.Numerics.Vector2(Position.X - (RIGHTWALL - Diameter), 0);
            }
            else if(Position.X <= RIGHTNET && Position.X >= LEFTNET)
            {
                Position -= new System.Numerics.Vector2(Position.X - RIGHTNET, 0);
            }

            if (Position.Y <= ONGROUND)
            {
                Velocity += new System.Numerics.Vector2(0, 14);
            }
        }

        public override void Draw(WriteableBitmap surface)
        {
            surface.FillEllipse((int)Position.X, (int)Position.Y, (int)(Position.X + Diameter), (int)(Position.Y + Diameter), Colors.LightGreen);
            surface.DrawEllipse((int)Position.X, (int)Position.Y, (int)(Position.X + Diameter), (int)(Position.Y + Diameter), Colors.Black);
            if (Position.X < (RIGHTNET - Diameter))
            {
                surface.FillEllipse((int)(Position.X + (Diameter * 0.6f)), (int)(Position.Y + (Diameter * 0.1f)), (int)((Position.X + (Diameter * 0.6f)) + 17), (int)((Position.Y + (Diameter * 0.1f) + 17)), Colors.White);
                surface.FillEllipse((int)(Position.X + (Diameter * 0.71f)), (int)(Position.Y + (Diameter * 0.11)), (int)((Position.X + (Diameter * 0.71f)) + 7), (int)((Position.Y + (Diameter * 0.11f) + 7)), Colors.Black);
            }
            else if (Position.X >= LEFTNET)
            {
                surface.FillEllipse((int)(Position.X + (Diameter * 0.2f)), (int)(Position.Y + (Diameter * 0.1f)), (int)((Position.X + (Diameter * 0.2f)) + 17), (int)((Position.Y + (Diameter * 0.1f) + 17)), Colors.White);
                surface.FillEllipse((int)(Position.X + (Diameter * 0.22f)), (int)(Position.Y + (Diameter * 0.11)), (int)((Position.X + (Diameter * 0.22f)) + 7), (int)((Position.Y + (Diameter * 0.11f) + 7)), Colors.Black);
            }
            surface.FillRectangle((int)Position.X, (int)Position.Y + (int)(Diameter / 2), (int)Position.X + (int)(Diameter + 1f), (int)Position.Y + (int)(Diameter + 1f), Colors.SkyBlue);
            base.Draw(surface);
        }

        public override bool Collision(GameEntity entity)
        {
            if (entity is Ground)
            {
                float y1G = entity.Position.Y; //y1G is y coord of ground
                float y2G = Position.Y + (Diameter/2); //y2G is y coord of bottom of ball

                if (y2G > y1G)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            /*
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
            */
            //base.Collision(entity);
        }

        public override void RoundObjectsCollide(GameEntity entity)
        {

        }

        //return slime diameter
        public override float GetDiameter()
        {
            return Diameter;
        }
    }
}