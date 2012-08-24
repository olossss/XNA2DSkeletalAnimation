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

        private Texture2D line;

        private Bone sRoot;
        private Bone head;
        private Bone body;
        private Bone lShoulder;
        private Bone rShoulder;
        private Bone lArm;
        private Bone rArm;

        private int time = 0;

        private Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();

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

            sRoot = new Bone();
            sRoot.IsRootMaster = true;
            sRoot.IsNoVis = true;
            sRoot.Position = new Vector2(760/2, 480/2);
            sRoot.Length = 0;
            sRoot.Angle = 0;
            sRoot.Direction = new Vector2(0, 0);

            head = new Bone();
            head.Parent = sRoot;
            head.Position = calcPosition(head.Parent);
            head.Angle = MathHelper.ToRadians(180);
            head.Length = 16;
            head.RotationOrigin = new Vector2(3, 0);
            head.Direction = new Vector2(0, -1);
            head.TextureName = "arm";

            body = new Bone();
            body.Parent = sRoot;
            body.Position = calcPosition(body.Parent);//new Vector2(0, 0) + body.Parent.Position;
            body.Angle = MathHelper.ToRadians(0);
            body.Length = 16;
            body.RotationOrigin = new Vector2(3, 0);
            body.Direction = new Vector2(0, 1);
            body.TextureName = "arm";

            lShoulder = new Bone();
            lShoulder.Parent = sRoot;
            lShoulder.Position = calcPosition(lShoulder.Parent); //new Vector2(0, 0) + lShoulder.Parent.Position;
            lShoulder.Angle = MathHelper.ToRadians(90);
            lShoulder.Length = 16;
            lShoulder.RotationOrigin = new Vector2(3, 0);
            lShoulder.Direction = new Vector2(-1f, 0.25f);
            lShoulder.Direction.Normalize();
            lShoulder.TextureName = "arm";

            rShoulder = new Bone();
            rShoulder.Parent = sRoot;
            rShoulder.Position = calcPosition(rShoulder.Parent); //new Vector2(0, 0) + rShoulder.Parent.Position;
            rShoulder.Angle = MathHelper.ToRadians(-90);
            rShoulder.Length = 16;
            rShoulder.RotationOrigin = new Vector2(3, 0);
            rShoulder.Direction = new Vector2(1f, 0.25f);
            rShoulder.TextureName = "arm";

            lArm = new Bone();
            lArm.Parent = lShoulder;
            lArm.Position = calcPosition(lArm.Parent); //new Vector2(-16, 0) + lArm.Parent.Position;
            lArm.Angle = MathHelper.ToRadians(90);
            lArm.Length = 16;
            lArm.RotationOrigin = new Vector2(3, 0);
            lArm.Direction = new Vector2(-1, 0);
            lArm.TextureName = "arm";

            rArm = new Bone();
            rArm.Parent = rShoulder;
            rArm.Position = calcPosition(rArm.Parent); //new Vector2(16, 0) + rArm.Parent.Position;
            rArm.Angle = MathHelper.ToRadians(-90);
            rArm.Length = 16;
            rArm.RotationOrigin = new Vector2(3, 0);
            rArm.Direction = new Vector2(1, 0);
            rArm.TextureName = "arm";
            rArm.KeyFrames.Add(new BoneKeyFrame(0, 0, 16));
            rArm.KeyFrames.Add(new BoneKeyFrame(1, MathHelper.ToRadians(30), 16));
            rArm.KeyFrames.Add(new BoneKeyFrame(2, MathHelper.ToRadians(-60), 16));
            rArm.KeyFrames.Add(new BoneKeyFrame(3, 0, 16));

            sRoot.Children.Add(head);
            sRoot.Children.Add(body);
            sRoot.Children.Add(lShoulder);
            lShoulder.Children.Add(lArm);
            sRoot.Children.Add(rShoulder);
            rShoulder.Children.Add(rArm);

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

            line = Content.Load<Texture2D>("line");

            textures.Add("face", face.a_Texture);
            textures.Add("chest", chest.a_Texture);
            textures.Add("arm", arm1.a_Texture);

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
                sRoot.Position += new Vector2(0, -1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                sRoot.Position += new Vector2(0, 1);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                sRoot.Position += new Vector2(-1, 0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //sRoot.Position += new Vector2(1, 0);
                rShoulder.Direction += new Vector2(0, 0.01f);
                rShoulder.Direction.Normalize();
            }




            // TODO: Add your update logic here
            //face.updateTime(gameTime);
            //chest.updateTime(gameTime);
            //arm1.updateTime(gameTime);
            //arm2.updateTime(gameTime);
            //foot1.updateTime(gameTime);
            //foot2.updateTime(gameTime);

            animateBones(sRoot, time, 60);
            time++;

            if (time > 60 * 4)
                time = 0;


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

            /*
            spriteBatch.Draw(chest.a_Texture, chest.getKeyPosition() + chest.a_RotationOrigin, null, Color.White, chest.getKeyRotation(), chest.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(face.a_Texture, face.getKeyPosition() + face.a_RotationOrigin, null, Color.White, face.getKeyRotation(), face.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(arm1.a_Texture, arm1.getKeyPosition() + arm1.a_RotationOrigin, null, Color.White, arm1.getKeyRotation(), arm1.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(arm2.a_Texture, arm2.getKeyPosition() + arm2.a_RotationOrigin, null, Color.White, arm2.getKeyRotation(), arm2.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(foot1.a_Texture, foot1.getKeyPosition() + foot1.a_RotationOrigin, null, Color.White, foot1.getKeyRotation(), foot1.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(foot2.a_Texture, foot2.getKeyPosition() + foot2.a_RotationOrigin, null, Color.White, foot2.getKeyRotation(), foot2.a_RotationOrigin, Vector2.One, SpriteEffects.None, 0f);
            */


            //drawLine(line, new Vector2(100, 100), new Vector2(500, 300), Color.White);

            drawBones(sRoot);

            spriteBatch.DrawString(font, "fps: " + 1000 / avg, Vector2.Zero, Color.Red);


            spriteBatch.End();

            base.Draw(gameTime);
        }



        private void drawBones(Bone root)
        {

            if (!root.IsRootMaster && !root.IsNoVis)
                //drawLine(line, root.Position, root.Position + root.Direction * root.Length, Color.White);
                drawBoneLine(line, root, Color.White);
                //spriteBatch.Draw(textures[root.TextureName], root.Position + root.RotationOrigin, null, Color.White, root.Angle, root.RotationOrigin, Vector2.One, SpriteEffects.None, 0f);


            for (int i = 0; i < root.Children.Count; i++)
                drawBones(root.Children[i]);
        }


        private void drawLine(Texture2D tex, Vector2 a, Vector2 b, Color color)
        {
            Vector2 origin = new Vector2(0.5f, 0f);
            Vector2 diff = b - a;

            Vector2 scale = new Vector2(1.0f, diff.Length()/tex.Height);

            float angle = (float)Math.Atan2(diff.Y, diff.X) - MathHelper.PiOver2;

            spriteBatch.Draw(tex, a, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
        }

        private void drawBoneLine(Texture2D tex, Bone root, Color color)
        {
            Vector2 origin = new Vector2(2f, 0f);
            Vector2 diff = root.Direction * root.Length;//root.Position - (root.Position + root.Direction * root.Length);

            Vector2 scale = new Vector2(1.0f, diff.Length() / tex.Height);

            float angle = (float)Math.Atan2(diff.Y, diff.X) - MathHelper.PiOver2;

            spriteBatch.Draw(tex, root.Position, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
        }


        private void animateBones(Bone root, int time, int rate)
        {
            float ang, len, tim;

            for (int i = 0; i < root.KeyFrames.Count; i++)
            {
                if (root.KeyFrames[i].time * rate == time)
                {
                    if (i != root.KeyFrames.Count - 1)
                    {
                        tim = root.KeyFrames[i + 1].time*rate - root.KeyFrames[i].time*rate;
                        ang = root.KeyFrames[i + 1].angle - root.KeyFrames[i].angle;
                        len = root.KeyFrames[i + 1].length - root.KeyFrames[i].length;

                        root.AngleOff = ang / tim;
                        root.LengthOff = len / tim;
                    }
                    else
                    {
                        root.AngleOff = 0;
                        root.LengthOff = 0;
                    }
                }
            }

            root.Angle += root.AngleOff;
            root.Length += root.LengthOff;

            for (int i = 0; i < root.Children.Count; i++)
                animateBones(root.Children[i], time, rate);
        }

        private Vector2 calcPosition(Bone parent)
        {
            return parent.Position + parent.Direction * parent.Length;
        }
    }
}
