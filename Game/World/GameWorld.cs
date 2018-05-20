using Game.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.World
{
public class GameWorld
    {
        public List<GameEntity> GameEntities { get; }
        public Stopwatch GameTimer { get; }
        private TimeSpan previousGameTick;

        public GameWorld()
        {
            GameEntities = new List<GameEntity>();
            GameTimer = new Stopwatch();
        }

        public void AddEntity(GameEntity entity)
        {
            GameEntities.Add(entity);
        }

        public void StartTimer()
        {
            GameTimer.Start();
        }

        public float ElapsedMillisecondsSinceLastTick
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }


        public void GameTick()
        {
            foreach (var entity in GameEntities)
            {
                entity.GameTick(ElapsedMillisecondsSinceLastTick);

                if(!(entity == GameEntities[0]) && (entity.Collision(GameEntities[0]) == true))
                {
                    entity.Velocity = new System.Numerics.Vector2(0, 0);
                }
                else if(!(entity == GameEntities[1]) && !(entity == GameEntities[0]) && (entity.Collision(GameEntities[1]) == true))
                {
                    entity.Velocity = entity.Velocity * new System.Numerics.Vector2(1, -1);
                }
                /*else if(!(entity == GameEntities[2]) && (entity.Collision(GameEntities[2]) == true))
                {
                    entity.Velocity = new System.Numerics.Vector2(0, 0);
                }*/
            }
                
             

            previousGameTick = GameTimer.Elapsed;
        }
    }
}
