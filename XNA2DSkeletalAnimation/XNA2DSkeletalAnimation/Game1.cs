using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA2DSkeletalAnimation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Animation face;
        private Animation arm1;
        private Animation arm2;
        private Animation chest;
        private Animation foot1;
        private Animation foot2;
        private Texture2D origin;

        private int elapsed;
        private int count = 0;
        private float avg;

        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true;
            graphics.PreferMultiSampling = true;


            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            int time = 1600;
            float key1 = 0.25f, key2 = 0.5f, key3 = 0.75f, key4 = 1f;



            // TODO: Add your initialization logic here
            face = new Animation();
            face.a_Origin = new Vector2(100, 95);
            face.a_RotationOrigin = new Vector2(8, 16);
            face.a_Rotation = .3f;
            face.a_AnimationTime = time;
            face.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, .3f));
            face.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 3), 0f));
            face.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 5), -.3f));
            face.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 3), 0f));
            face.a_KeyFrames.Add(new KeyFrame((int)(time * key4), new Vector2(0, 0), .3f));

            chest = new Animation();
            chest.a_Origin = new Vector2(100, 110);
            chest.a_RotationOrigin = new Vector2(8, 12);
            chest.a_AnimationTime = time;
            chest.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, 0f));
            chest.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 4), 0f));
            chest.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 6), 0f));
            chest.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 4), 0f));
            chest.a_KeyFrames.Add(new KeyFrame((int)(time * key4), Vector2.Zero, 0f));


            arm1 = new Animation();
            arm1.a_Origin = new Vector2(116, 110);
            arm1.a_RotationOrigin = new Vector2(3, 0);
            arm1.a_AnimationTime = time;
            arm1.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, 0f));
            arm1.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 6), 0f));
            arm1.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 8), -3.14f));
            arm1.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 6), 0f));
            arm1.a_KeyFrames.Add(new KeyFrame((int)(time * key4), Vector2.Zero, 0f));

            arm2 = new Animation();
            arm2.a_Origin = new Vector2(94, 110);
            arm2.a_RotationOrigin = new Vector2(3, 0);
            arm2.a_AnimationTime = time;
            arm2.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, 0f));
            arm2.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 6), 0f));
            arm2.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 8), 3.14f));
            arm2.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 6), 0f));
            arm2.a_KeyFrames.Add(new KeyFrame((int)(time * key4), Vector2.Zero, 0f));

            foot1 = new Animation();
            foot1.a_Origin = new Vector2(109, 133);
            foot1.a_RotationOrigin = new Vector2(3, 0);
            foot1.a_AnimationTime = time;
            foot1.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, 0f));
            foot1.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 6), 0f));
            foot1.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 8), -1f));
            foot1.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 6), 0f));
            foot1.a_KeyFrames.Add(new KeyFrame((int)(time * key4), Vector2.Zero, 0f));

            foot2 = new Animation();
            foot2.a_Origin = new Vector2(101, 133);
            foot2.a_RotationOrigin = new Vector2(3, 0);
            foot2.a_AnimationTime = time;
            foot2.a_KeyFrames.Add(new KeyFrame(0, Vector2.Zero, 0f));
            foot2.a_KeyFrames.Add(new KeyFrame((int)(time * key1), new Vector2(0, 6), 0f));
            foot2.a_KeyFrames.Add(new KeyFrame((int)(time * key2), new Vector2(0, 8), 1f));
            foot2.a_KeyFrames.Add(new KeyFrame((int)(time * key3), new Vector2(0, 6), 0f));
            foot2.a_KeyFrames.Add(new KeyFrame((int)(time * key4), Vector2.Zero, 0f));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("General");

            face.a_Texture = Content.Load<Texture2D>("face");
            arm1.a_Texture = Content.Load<Texture2D>("arm");
            arm2.a_Texture = Content.Load<Texture2D>("arm");
            chest.a_Texture = Content.Load<Texture2D>("chest");
            foot1.a_Texture = Content.Load<Texture2D>("foot");
            foot2.a_Texture = Content.Load<Texture2D>("foot");

            origin = Content.Load<Texture2D>("origin");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                face.a_Origin += new Vector2(0, -10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                face.a_Origin += new Vector2(0, 10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                face.a_Origin += new Vector2(-10,0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                face.a_Origin += new Vector2(10,0);
            }



            // TODO: Add your update logic here
            face.updateTime(gameTime);
            chest.updateTime(gameTime);
            arm1.updateTime(gameTime);
            arm2.updateTime(gameTime);
            foot1.updateTime(gameTime);
            foot2.updateTime(gameTime);


            //calculate ms/s
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            count++;
            if (count > 100)
            {
                avg = (float)elapsed / (float)count;
                count = 0;
                elapsed = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //spriteBatch.Draw(origin, face.a_Origin, Color.CornflowerBlue);

            spriteBatch.Draw(chest.a_Texture, chest.getKeyPosition() + chest.a_RotationOrigin, null, Color.White, chest.getKeyRotation(), chest.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(face.a_Texture, face.getKeyPosition() + face.a_RotationOrigin, null, Color.White, face.getKeyRotation(), face.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(arm1.a_Texture, arm1.getKeyPosition() + arm1.a_RotationOrigin, null, Color.White, arm1.getKeyRotation(), arm1.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(arm2.a_Texture, arm2.getKeyPosition() + arm2.a_RotationOrigin, null, Color.White, arm2.getKeyRotation(), arm2.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(foot1.a_Texture, foot1.getKeyPosition() + foot1.a_RotationOrigin, null, Color.White, foot1.getKeyRotation(), foot1.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(foot2.a_Texture, foot2.getKeyPosition() + foot2.a_RotationOrigin, null, Color.White, foot2.getKeyRotation(), foot2.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);


            spriteBatch.DrawString(font, "fps: " + 1000 / avg, Vector2.Zero, Color.Red);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
