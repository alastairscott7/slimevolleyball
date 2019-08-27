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

        //Constructor
        public GameWorld()
        {
            GameEntities = new List<GameEntity>();
            GameTimer = new Stopwatch();
        }

        //adding game entities to list
        public void AddEntity(GameEntity entity)
        {
            GameEntities.Add(entity);
        }

        //start timer
        public void StartTimer()
        {
            GameTimer.Start();
        }

        //measures milliseconds since last tick
        public float ElapsedMillisecondsSinceLastTick
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }

        public void moveArtificialSlime()
        {
            if(GameEntities[3].Position.X - 10f < GameEntities[1].Position.X)
            {
                GameEntities[1].Velocity = new System.Numerics.Vector2(-200, GameEntities[1].Velocity.Y);
            }
            else if (GameEntities[3].Position.X - 70f > GameEntities[1].Position.X)
            {
                GameEntities[1].Velocity = new System.Numerics.Vector2(200, GameEntities[1].Velocity.Y);
            }
            else if ((GameEntities[3].Position.Y > GameEntities[1].Position.Y - 50) && GameEntities[1].Velocity.Y == 0f)
            {
                GameEntities[1].Velocity = new System.Numerics.Vector2(GameEntities[1].Velocity.X, -350);
            }
        }

        //What happens after each game tick
        public void GameTick()
        {
            //Performs GameTick method for each entity
            foreach (var entity in GameEntities)
            {
                //Gametick for individual entities, updates position and velocity
                entity.GameTick(ElapsedMillisecondsSinceLastTick);

                //**Collision algorithm, should this be moved into entity gametick method?
                if(!(entity == GameEntities[0]) && !(entity == GameEntities[1]) && (entity.Collision(GameEntities[0]) == true))
                {
                    //collison between ball and slime
                    entity.RoundObjectsCollide(GameEntities[0]);
                }
                else if (!(entity == GameEntities[0]) && !(entity == GameEntities[1]) && (entity.Collision(GameEntities[1]) == true))
                {
                    //collison between ball and artificial slime
                    entity.RoundObjectsCollide(GameEntities[1]);
                }
                else if ((entity == GameEntities[0] && (entity.Collision(GameEntities[2]) == true)) || (entity == GameEntities[1] && (entity.Collision(GameEntities[2]) == true)))
                {
                    //collision between ground and slime or artificial slime
                    entity.Velocity = new System.Numerics.Vector2(entity.Velocity.X, 0);
                }
                else if(!(entity == GameEntities[2]) && !(entity == GameEntities[0]) && !(entity == GameEntities[1]) && (entity.Collision(GameEntities[2]) == true))
                {
                    //collision between ground and ball
                    entity.Velocity = entity.Velocity * new System.Numerics.Vector2(1, -1);
                }
            }

     
            //update previoustotal time elapsed,
            //which is subtracted from total time elapsed
            //to find milliseconds between ticks
            previousGameTick = GameTimer.Elapsed;


            //OLD FOREACH
            /*
            foreach (var entity in GameEntities)
            {
                //Gametick for individual entities, updates position and velocity
                entity.GameTick(ElapsedMillisecondsSinceLastTick);

                //**Collision algorithm, should this be moved into entity gametick method?
                if (!(entity == GameEntities[0]) && (entity.Collision(GameEntities[0]) == true))
                {
                    //collison between ball and slime
                    entity.RoundObjectsCollide(GameEntities[0]);
                }
                else if (entity == GameEntities[0] && (entity.Collision(GameEntities[1]) == true))
                {
                    //collision between ground and slime
                    entity.Velocity = new System.Numerics.Vector2(entity.Velocity.X, 0);
                }
                else if (!(entity == GameEntities[1]) && !(entity == GameEntities[0]) && (entity.Collision(GameEntities[1]) == true))
                {
                    //collision between ground and ball
                    entity.Velocity = entity.Velocity * new System.Numerics.Vector2(1, -1);
                }
            }
            */
        }
    }
}
