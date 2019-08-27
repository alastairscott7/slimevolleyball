using Game.Entities;
using Game.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //width and height variables used to measure the window size
        int width, height;
        //writable bitmap has all game entities drawn to it
        WriteableBitmap writableBitmap;
        //class which contains laws of the world
        GameWorld world;
    
        //main method which starts the window
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //ViewPort loading event
        private void ViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            //create writable bitmap which is the same size as the window
            width = (int)this.ViewPortContainer.ActualWidth;
            height = (int)this.ViewPortContainer.ActualHeight;
            writableBitmap = BitmapFactory.New(width, height);
            ViewPort.Source = writableBitmap;

            //create world and start ticks
            CreateWorld();
            world.StartTimer();

            //Not sure how this works but it starts the game running at 60fps
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        //called 60 times per second, contains drawing method
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //this function updates previous game ticket 
            world.GameTick();
            world.moveArtificialSlime();

            //clear old frame
            writableBitmap.Clear();

            writableBitmap.FillRectangle(0, 0, 800, 600, Colors.SkyBlue);

            //draw all entities in their new location
            foreach (GameEntity entity in world.GameEntities)
            {
                entity.Draw(writableBitmap);
            }
        }

        //KeyDown event to move player's slime left and right
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if only move in direction if one arrow key is pressed, stop if both are pressed
            if (e.Key == Key.Right && !Keyboard.IsKeyDown(Key.Left))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(200, world.GameEntities[0].Velocity.Y);
            }
            else if(e.Key == Key.Right && Keyboard.IsKeyDown(Key.Left))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, world.GameEntities[0].Velocity.Y);
            }
            else if (e.Key == Key.Left && !Keyboard.IsKeyDown(Key.Right))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(-200, world.GameEntities[0].Velocity.Y);
            }
            else if (e.Key == Key.Left && Keyboard.IsKeyDown(Key.Right))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, world.GameEntities[0].Velocity.Y);
            }
            else if (e.Key == Key.Up && world.GameEntities[0].Velocity.Y == 0) //bug: double jump @ max jump ht.
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(world.GameEntities[0].Velocity.X, -350);
            }
        }

        //KeyUp event to stop moving when player lets go of arrow key
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, world.GameEntities[0].Velocity.Y);
            }
            else if (e.Key == Key.Left)
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, world.GameEntities[0].Velocity.Y);
            }

        }

        //This method is called when viewport is loaded, and creates world and game entities
        private void CreateWorld()
        {
            //create new game world
            world = new GameWorld();

            //create entities
            var ground = new Ground()
            {
                Position = new System.Numerics.Vector2(0, 450),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            var ball = new Ball()
            {
                Diameter = 20,
                Position = new System.Numerics.Vector2(190, 300),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            var ballMoveable = new Slime()
            {
                Diameter = 80,
                Position = new System.Numerics.Vector2(160, 410),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            var artificialSlime = new Slime()
            {
                Diameter = 80,
                Position = new System.Numerics.Vector2(560, 410),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            //add game entities to entity list "GameEntities"
            world.AddEntity(ballMoveable);
            world.AddEntity(artificialSlime);
            world.AddEntity(ground);
            world.AddEntity(ball); 

        }
    }
}
