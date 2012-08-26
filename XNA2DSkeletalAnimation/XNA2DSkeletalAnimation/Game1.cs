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
        private Texture2D mouse;

        private Bone sRoot;
        private Bone head;
        private Bone body;
		private Bone ulLeg;
		private Bone urLeg;
		private Bone llLeg;
		private Bone lrLeg;
        private Bone lShoulder;
        private Bone rShoulder;
        private Bone ulArm;
        private Bone urArm;
        private Bone lrArm;
        private Bone llArm;
        private Bone rHand;

        private List<Bone> boneList = new List<Bone>();



        private MouseState prevMouse, currMouse;
        private Vector2 target = Vector2.Zero;

        private int time = 0;

        private Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();

        private int elapsed;
        private int count = 0;
        private float avg;

        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.PreferredBackBufferHeight = 1050;
            //graphics.PreferredBackBufferWidth = 1680;
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
            head.RotationOrigin = new Vector2(8, 0);
            head.Direction = new Vector2(0, -1);
            head.TextureName = "face";
			head.TextureFlip = true;

            body = new Bone();
            body.Parent = sRoot;
            body.Position = calcPosition(body.Parent);//new Vector2(0, 0) + body.Parent.Position;
            body.Angle = MathHelper.ToRadians(0);
            body.Length = 24;
            body.RotationOrigin = new Vector2(8, 0);
            body.Direction = new Vector2(0, 1);
            body.TextureName = "chest";
			
			ulLeg = new Bone();
			ulLeg.Parent = body;
			ulLeg.Position = calcPosition(ulLeg.Parent);
			ulLeg.Angle = MathHelper.ToRadians(-60);
			ulLeg.Length = 16;
			ulLeg.RotationOrigin = new Vector2(3,0);
			ulLeg.Direction = new Vector2(-0.5f,0.5f);
			ulLeg.TextureName = "arm";
			
			urLeg = new Bone();
			urLeg.Parent = body;
			urLeg.Position = calcPosition(urLeg.Parent);
			urLeg.Angle = MathHelper.ToRadians(-60);
			urLeg.Length = 16;
			urLeg.RotationOrigin = new Vector2(3,0);
			urLeg.Direction = new Vector2(0.5f,0.5f);
			urLeg.TextureName = "arm";
			
			llLeg = new Bone();
			llLeg.Parent = ulLeg;
			llLeg.Position = calcPosition(llLeg.Parent);
			llLeg.Angle = MathHelper.ToRadians(-60);
			llLeg.Length = 8;
			llLeg.RotationOrigin = new Vector2(3,0);
			llLeg.Direction = new Vector2(0,1);
			llLeg.TextureName = "foot";
			
			lrLeg = new Bone();
			lrLeg.Parent = urLeg;
			lrLeg.Position = calcPosition(lrLeg.Parent);
			lrLeg.Angle = MathHelper.ToRadians(-60);
			lrLeg.Length = 8;
			lrLeg.RotationOrigin = new Vector2(3,0);
			lrLeg.Direction = new Vector2(0,1);
			lrLeg.TextureName = "foot";
			
            lShoulder = new Bone();
            lShoulder.Parent = sRoot;
            lShoulder.Position = calcPosition(lShoulder.Parent); //new Vector2(0, 0) + lShoulder.Parent.Position;
            lShoulder.Angle = MathHelper.ToRadians(90);
            lShoulder.Length = 8;
            lShoulder.RotationOrigin = new Vector2(2, 0);
            lShoulder.Direction = new Vector2(-1f, 0.25f);
            lShoulder.Direction.Normalize();
			lShoulder.IsNoVis = true;
            lShoulder.TextureName = "arm";

            rShoulder = new Bone();
            rShoulder.Parent = sRoot;
            rShoulder.Position = calcPosition(rShoulder.Parent); //new Vector2(0, 0) + rShoulder.Parent.Position;
            rShoulder.Angle = MathHelper.ToRadians(-90);
            rShoulder.Length = 8;
            rShoulder.RotationOrigin = new Vector2(2, 0);
            rShoulder.Direction = new Vector2(1f, 0.25f);
			rShoulder.IsNoVis = true;
            rShoulder.IsRigid = true;
            rShoulder.TextureName = "arm";
            

            ulArm = new Bone();
            ulArm.Parent = lShoulder;
            ulArm.Position = calcPosition(ulArm.Parent); //new Vector2(-16, 0) + lArm.Parent.Position;
            ulArm.Angle = MathHelper.ToRadians(90);
            ulArm.Length = 16;
            ulArm.RotationOrigin = new Vector2(3, 0);
            ulArm.Direction = new Vector2(-1, 0);
            ulArm.TextureName = "arm";

            urArm = new Bone();
            urArm.Parent = rShoulder;
            urArm.Position = calcPosition(urArm.Parent);
            urArm.Angle = MathHelper.ToRadians(-90);
            urArm.Length = 6;
            urArm.RotationOrigin = new Vector2(3, 0);
            urArm.Direction = new Vector2(1, 0);
            urArm.MinAngle = MathHelper.ToRadians(-150);
            urArm.MaxAngle = MathHelper.ToRadians(30);
            urArm.TextureName = "arm";

            lrArm = new Bone();
            lrArm.Parent = urArm;
            lrArm.Position = calcPosition(lrArm.Parent);
            lrArm.Angle = MathHelper.ToRadians(-90);
            lrArm.Length = 6;
            lrArm.RotationOrigin = new Vector2(3, 0);
            lrArm.Direction = new Vector2(1, 0);
            lrArm.MinAngle = MathHelper.ToRadians(-150);
            lrArm.MaxAngle = MathHelper.ToRadians(90);
            lrArm.TextureName = "arm";

            rHand = new Bone();
            rHand.Parent = lrArm;
            rHand.Position = calcPosition(rHand.Parent);
            rHand.Length = 6;
            rHand.RotationOrigin = new Vector2(3, 0);
            rHand.Direction = new Vector2(1, 0);
            rHand.MinAngle = MathHelper.ToRadians(-30);
            rHand.MaxAngle = MathHelper.ToRadians(30);
            rHand.TextureName = "hand";

            sRoot.Children.Add(head);
            sRoot.Children.Add(body);
			body.Children.Add(ulLeg);
			ulLeg.Children.Add(llLeg);
			body.Children.Add(urLeg);
			urLeg.Children.Add(lrLeg);
            sRoot.Children.Add(lShoulder);
            lShoulder.Children.Add(ulArm);
            sRoot.Children.Add(rShoulder);
            rShoulder.Children.Add(urArm);
            urArm.Children.Add(lrArm);
            lrArm.Children.Add(rHand);


            
            boneList = populateBones(100, 16, "line");

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
            
            line = Content.Load<Texture2D>("line");
            mouse = Content.Load<Texture2D>("origin");

            textures.Add("face", Content.Load<Texture2D>("face"));
            textures.Add("chest", Content.Load<Texture2D>("chest"));
            textures.Add("arm",  Content.Load<Texture2D>("arm"));
			textures.Add("foot", Content.Load<Texture2D>("foot"));
            textures.Add("line", Content.Load<Texture2D>("line"));
            textures.Add("hand", Content.Load<Texture2D>("hand"));
                

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
            prevMouse = currMouse;
            currMouse = Mouse.GetState();


            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                boneList[0].Position += new Vector2(0, -10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                boneList[0].Position += new Vector2(0, 10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                boneList[0].Position += new Vector2(-10, 0);
            }
			
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
                boneList[0].Position += new Vector2(10, 0);
			}

            if (Keyboard.GetState().IsKeyDown(Keys.OemOpenBrackets))
            {
				head.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(head.Direction, 0.1f));
            }
			
			if (Keyboard.GetState().IsKeyDown(Keys.OemCloseBrackets))
            {
				head.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(head.Direction, -0.1f));
            }
			
			if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
				urArm.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(urArm.Direction, 0.1f));
            }
			
			if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
				urArm.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(urArm.Direction, -0.1f));
            }

            if (prevMouse.X != currMouse.X || prevMouse.Y != currMouse.Y)
            {
                target = new Vector2(currMouse.X, currMouse.Y);// -new Vector2(760 / 2, 480 / 2);
            }


            animate(boneList[boneList.Count-1],boneList[boneList.Count-1], target, .7f);

            //animate(rHand, rHand, target, .1f);


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

            spriteBatch.Draw(mouse, target, Color.CornflowerBlue);

            drawBones(boneList[0]);
            //drawBones(sRoot);

            spriteBatch.DrawString(font, "fps: " +  avg, Vector2.Zero, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }



        private void drawBones(Bone root)
        {

            if (!root.IsRootMaster && !root.IsNoVis)

				drawBoneLine(textures[root.TextureName], root, Color.White);

            for (int i = 0; i < root.Children.Count; i++)
                drawBones(root.Children[i]);
        }

        private void drawBoneLine(Texture2D tex, Bone root, Color color)
        {
            Vector2 origin = root.RotationOrigin;//new Vector2(2f, 0f);
            Vector2 diff = root.Direction * root.Length;//root.Position - (root.Position + root.Direction * root.Length);

            Vector2 scale = new Vector2(1.0f, diff.Length() / tex.Height);

            float angle = (float)Math.Atan2(diff.Y, diff.X) - MathHelper.PiOver2;
			
			if(root.TextureFlip)
			{
            	spriteBatch.Draw(tex, root.Position, null, color, angle, origin, scale, SpriteEffects.FlipVertically, 1.0f);
			}else
			{
				spriteBatch.Draw(tex, root.Position, null, color, angle, origin, scale, SpriteEffects.None, 1.0f);
			}
        }


        
        private void animate(Bone effector, Bone bone, Vector2 target, float speed)
        {
            
            
            if (bone.Direction.X == 0 && bone.Direction.Y == 0 || bone.IsRigid)
                return;

            Vector2 tip = bone.Position + bone.Direction * bone.Length;

            if (Vector2.Distance(bone.Position + bone.Direction * bone.Length, target) < bone.Length * 0.10f)
                return;

            Vector2 toTarget = VectorHelper.normalize(target - tip);

            /*
            float curAngle = VectorHelper.getSignedAngle(VectorHelper.normalize(tip - bone.Position),VectorHelper.normalize(bone.Position - bone.Parent.Position));
            //float curAngle = VectorHelper.getAngle2(VectorHelper.normalize(bone.Position - bone.Parent.Position), VectorHelper.normalize(tip - bone.Position));
            //float curAngle = VectorHelper.getAngle2(bone.Parent.Position + bone.Parent.Direction * bone.Parent.Length, tip);

            if (bone.Parent.IsRootMaster)
                curAngle = 0f;

            curAngle = MathHelper.WrapAngle(curAngle);

            if(curAngle >= bone.MinAngle && curAngle <= bone.MaxAngle)
                bone.Direction = VectorHelper.normalize(bone.Direction + toTarget * speed);
            
            float angle;

            if (curAngle < bone.MinAngle)
            {
                angle = bone.MinAngle -curAngle;
                bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(bone.Direction, angle ));
            }
            
            if (curAngle > bone.MaxAngle)
            {
                angle = bone.MaxAngle - curAngle;
                bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(bone.Direction, angle));
            }*/


            
            float angle = Vector2.Dot(VectorHelper.getLeftNormal(bone.Direction), toTarget);
            float rotVal = -angle * speed;


            float curAngle = VectorHelper.getSignedAngle(VectorHelper.normalize(bone.Position - bone.Parent.Position),VectorHelper.normalize(tip - bone.Position));
            //float curAngle = VectorHelper.getAngle2(VectorHelper.normalize(bone.Position - bone.Parent.Position), VectorHelper.normalize(tip - bone.Position));

            if (bone.Parent.IsRootMaster)
                curAngle = 0f;

            curAngle = MathHelper.WrapAngle(curAngle);

            if(curAngle >= bone.MinAngle && curAngle <= bone.MaxAngle)
                bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(bone.Direction, rotVal));


            curAngle = VectorHelper.getSignedAngle(VectorHelper.normalize(bone.Position - bone.Parent.Position),VectorHelper.normalize(tip - bone.Position));

            if (bone.Parent.IsRootMaster)
                curAngle = 0f;

            float angleCorrect;

            if (curAngle < bone.MinAngle)
            {
                angleCorrect = bone.MinAngle - (curAngle);
                bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(tip-bone.Position, angleCorrect * speed));
                //bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(VectorHelper.normalize(bone.Position - bone.Parent.Position), -bone.MinAngle * speed));
            }
            else if (curAngle > bone.MaxAngle)
            {
                angleCorrect = bone.MaxAngle - (curAngle);
                bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(tip - bone.Position, angleCorrect * speed));
                //bone.Direction = VectorHelper.normalize(VectorHelper.rotateVectorRadians(VectorHelper.normalize(bone.Position - bone.Parent.Position), bone.MaxAngle * speed));
            }
           
            if(bone.Parent != null)
                animate(effector,bone.Parent, tip, speed);
        }

        private Vector2 calcPosition(Bone parent)
        {
            return parent.Position + parent.Direction * parent.Length;
        }


        private List<Bone> populateBones(int count, float boneLength, String TextureName)
        {
            Bone root = new Bone();
            root.IsRootMaster = true;
            root.IsNoVis = true;
            root.Position = new Vector2(760 / 2, 480 / 2);
            root.Length = 0;
            root.Angle = 0;
            root.Direction = new Vector2(0, 0);

            List<Bone> list = new List<Bone>();
            
            list.Add(root);

            for (int i = 1; i < count; i++)
            {
                Bone bone = new Bone();
                bone.Parent = list[i - 1];
                bone.Position = calcPosition(bone.Parent);
                bone.Length = boneLength;
                bone.RotationOrigin = new Vector2(2, 0);
                bone.Direction = new Vector2(1, 0);
                bone.MinAngle = MathHelper.ToRadians(-45);
                bone.MaxAngle = MathHelper.ToRadians(45);
                bone.TextureName = TextureName;

                list.Add(bone);
                list[i - 1].Children.Add(bone);
            }


            return list;
        }
    }
}
