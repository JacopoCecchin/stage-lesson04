#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Resources;
using WaveEngine.Framework.Services;
#endregion

namespace Lezione4
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);

            this.AddSceneBehavior(new MySceneBehavior(), SceneBehavior.Order.PreUpdate);

            var camera2d = new FixedCamera2D("defaultCamera2D");
            camera2d.BackgroundColor = Color.CadetBlue;            
            this.EntityManager.Add(camera2d);

            //var sprite1 = new Entity("tree")
            //     .AddComponent(new Transform2D())
            //     .AddComponent(new Sprite(WaveContent.Assets.tree2_png))
            //     .AddComponent(new SpriteRenderer());
            //this.EntityManager.Add(sprite1);
            //sprite1.FindComponent<Transform2D>().LocalScale = new Vector2(.25f);

            var sprite2 = new Entity("ball")
                 .AddComponent(new Transform2D())
                 .AddComponent(new Sprite(WaveContent.Assets.ball_png))
                 .AddComponent(new SpriteRenderer());
            this.EntityManager.Add(sprite2);
            sprite2.FindComponent<Transform2D>().LocalScale = new Vector2(.25f);

            var sprite3 = new Entity("field")
                 .AddComponent(new Transform2D())
                 .AddComponent(new Sprite(WaveContent.Assets.field_png))
                 .AddComponent(new SpriteRenderer());
            this.EntityManager.Add(sprite3);
            sprite3.FindComponent<Transform2D>().LocalScale = new Vector2(.89f);
        }
    }

    public class MySceneBehavior : SceneBehavior
    {
        private Vector2 _ballDirection = Vector2.One;

        protected override void Update(System.TimeSpan gameTime)
        {
            float screenWidth = this.Scene.VirtualScreenManager.ScreenWidth;
            float screenHeigth = this.Scene.VirtualScreenManager.ScreenHeight;
            System.Random random = new System.Random();
            float speed = random.Next(150,301);

            var field = this.Scene.EntityManager.Find<Entity>("field");
            var f2d = field.FindComponent<Transform2D>();
            var positionfield = f2d.LocalPosition;

            var ball = this.Scene.EntityManager.Find<Entity>("ball");
            var t2d = ball.FindComponent<Transform2D>();
            var position = t2d.LocalPosition;

            position += _ballDirection * speed * (float)gameTime.TotalSeconds;
            Vector2 size = new Vector2(120,120)* .25f;
            Vector2 BottomRight = position + size;
            if (position.Y <= 0 || BottomRight.Y >= screenHeigth)
                _ballDirection.Y *= -1;

            if (position.X <= 0 || BottomRight.X >= screenWidth)
                _ballDirection.X *= -1;

            t2d.LocalPosition = position;
        }

        protected override void ResolveDependencies()
        {            

        }
    }

}