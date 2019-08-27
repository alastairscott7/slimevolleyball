using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Entities
{
    class Ball:GameEntity
    {
        public float Diameter { get; set; }
        public const float maxBallVelocityY = 500;
        public const float AGAINSTWALLLEFT = 0f;
        public const float AGAINSTWALLRIGHT = 785f;

        public override void GameTick(float millisecondsElapsed)
        {
            //new position based on velocity
            Position = Position + Velocity * millisecondsElapsed / 1000f;

            //Reverses X velocity if ball hits left wall
            if (Position.X <= AGAINSTWALLLEFT)
            {
                Position -= new System.Numerics.Vector2(Position.X, 0);
                Velocity = new System.Numerics.Vector2(-(Velocity.X), Velocity.Y);
            }
            //Reverses X velocity if ball hits right wall
            else if (Position.X >= (AGAINSTWALLRIGHT-Diameter))
            {
                Position -= new System.Numerics.Vector2((Position.X - (AGAINSTWALLRIGHT-Diameter)), 0);
                Velocity = new System.Numerics.Vector2(-(Velocity.X), Velocity.Y);
            }
            //BUG: NEED TO FIX BOUNCE PHYSICS, currently goes higher with each bounce
            //velocity increases over time due to gravity
            Velocity += new System.Numerics.Vector2(0, 14);
            if(Velocity.Y <= -maxBallVelocityY)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, -maxBallVelocityY);
            }
            if(Velocity.Y >= maxBallVelocityY)
            {
                Velocity = new System.Numerics.Vector2(Velocity.X, maxBallVelocityY);
            }
        }

        public override void Draw(WriteableBitmap surface)
        {
            surface.FillEllipse((int)Position.X, (int)Position.Y, (int)(Position.X + Diameter), (int)(Position.Y + Diameter), Colors.White);
            base.Draw(surface);
        }

        public override bool Collision(GameEntity entity)
        {
            //ball collides with ground
            if(entity is Ground)
            {
                float y1G = entity.Position.Y; //y1G is y coord of ground
                float y2G = Position.Y + Diameter; //y2G is y coord of bottom of ball

                if (y2G >= y1G)
                    return true;
                else
                    return false;
            }
            //ball collides with Slime
            else
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

                if (Math.Sqrt((dX * dX) + (dY * dY)) <= radius)
                    return true;
                else
                    return false;

                //base.Collision(entity);
            }

        }
        
        public override void RoundObjectsCollide(GameEntity entity)
        {
            //update velocity when ball collides with slime
            float x1 = Position.X + (Diameter / 2);
            float y1 = Position.Y + (Diameter / 2);
            float x2 = entity.Position.X + (entity.GetDiameter() / 2);
            float y2 = entity.Position.Y + (entity.GetDiameter() / 2);

            float r1 = Diameter / 2;
            float r2 = entity.GetDiameter() / 2;
            float radius = r1 + r2;

            float dX = x2 - x1;
            float dY = y2 - y1;
            //new velocity is the unit vector normal to the collision surface multiplied by magnitude of old velocity
            Velocity = new System.Numerics.Vector2(-dX, -dY) / (float)(Math.Sqrt((dX * dX) + (dY * dY))) * (float)(Math.Sqrt((Velocity.X * Velocity.X) + (Velocity.Y * Velocity.Y)));
            //currently momentum is trasferred from slime to ball by dividing slime velocity by 4 and adding to ball
            Velocity += new System.Numerics.Vector2(entity.Velocity.X/4, entity.Velocity.Y/4);
        }

        public override float GetDiameter()
        {
            return Diameter;
        }

    }
}
