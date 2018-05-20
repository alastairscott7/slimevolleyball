using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Media.Imaging;

namespace Game.Entities
{
    public abstract class GameEntity
    {
        //Represents the location of this game entity
        public Vector2 Position { get; set; }

        //Represents the velocity of this game entity, is in units of pixels per second.
        public Vector2 Velocity { get; set; }

        //This method will contain our game logic.
        //milliseconds elapsed indicates how much game time has passed since this function was last executed.
        public virtual void GameTick(float millisecondsElapsed)
        {

        }

        //Entities should draw themselves in this function.
        public virtual void Draw(WriteableBitmap surface)
        {
        }

        public virtual bool Collision(GameEntity entity)
        {
            return false;
        }

        public virtual float GetDiameter()
        {
            return 0f;
        }
    }
}
