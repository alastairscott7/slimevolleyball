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

        public override void GameTick(float millisecondsElapsed)
        {
            Position = Position + Velocity * millisecondsElapsed / 1000f; //f means float
        }

        public override void Draw(WriteableBitmap surface)
        {
            surface.FillRectangle((int)Position.X, (int)Position.Y, 800, 600, Colors.Brown);
            base.Draw(surface);
        }

        public override bool Collision(GameEntity entity)
        {
            return false;
        }

        public override float GetDiameter()
        {
            return 0f;
        }
    }
}
