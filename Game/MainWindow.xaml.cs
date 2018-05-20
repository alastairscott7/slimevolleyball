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
        int width, height;
        WriteableBitmap writableBitmap;
        GameWorld world;

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortContainer.ActualWidth;
            height = (int)this.ViewPortContainer.ActualHeight;
            writableBitmap = BitmapFactory.New(width, height);

            ViewPort.Source = writableBitmap;
            CreateWorld();
            world.StartTimer();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        //called 60 times per second, contains drawing method
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            world.GameTick();

            writableBitmap.Clear();

            foreach (GameEntity entity in world.GameEntities)
            {
                entity.Draw(writableBitmap);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && !Keyboard.IsKeyDown(Key.Left))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(100, 0);
            }
            else if(e.Key == Key.Right && Keyboard.IsKeyDown(Key.Left))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, 0);
            }
            else if (e.Key == Key.Left && !Keyboard.IsKeyDown(Key.Right))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(-100, 0);
            }
            else if (e.Key == Key.Left && Keyboard.IsKeyDown(Key.Right))
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, 0);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, 0);
            }
            if (e.Key == Key.Left)
            {
                world.GameEntities[0].Velocity = new System.Numerics.Vector2(0, 0);
            }

        }

        private void CreateWorld()
        {
            world = new GameWorld();
            var ground = new Ground()
            {
                Position = new System.Numerics.Vector2(0, 450),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            var ball = new Ball()
            {
                Diameter = 20,
                Position = new System.Numerics.Vector2(100, 100),
                Velocity = new System.Numerics.Vector2(60, 60)
            };

            var ball2 = new Ball()
            {
                Diameter = 20,
                Position = new System.Numerics.Vector2(100, 300),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            var ballMoveable = new Slime()
            {
                Diameter = 100,
                Position = new System.Numerics.Vector2(100, 400),
                Velocity = new System.Numerics.Vector2(0, 0)
            };

            
            world.AddEntity(ballMoveable);
            world.AddEntity(ground);
            world.AddEntity(ball);
            world.AddEntity(ball2);

        }
    }
}
