using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA2DSkeletalAnimation
{
    class Animation
    {
        private int a_ElapsedTime;

        public int ElapsedTime
        {
            get { return a_ElapsedTime; }
            set { a_ElapsedTime = value; }
        }

        private int a_AnimationTime;

        public int AnimationTime
        {
            get { return a_AnimationTime; }
            set { a_AnimationTime = value; }
        }

        private List<KeyFrame> a_KeyFrames = new List<KeyFrame>();

        public List<KeyFrame> KeyFrames
        {
            get { return a_KeyFrames; }
            set { a_KeyFrames = value; }
        }

        private Texture2D a_Texture;

        public Texture2D Texture
        {
            get { return a_Texture; }
            set { a_Texture = value; }
        }

        private Vector2 a_Origin;

        public Vector2 Origin
        {
            get { return a_Origin; }
            set { a_Origin = value; }
        }

        private Vector2 a_RotationOrigin;

        public Vector2 RotationOrigin
        {
            get { return a_RotationOrigin; }
            set { a_RotationOrigin = value; }
        }

        private float a_Rotation;

        public float Rotation
        {
            get { return a_Rotation; }
            set { a_Rotation = value; }
        }

        public void updateTime(GameTime gameTime)
        {
            a_ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (a_ElapsedTime >= a_AnimationTime)
            {
                a_ElapsedTime = 0;
            }
        }

        public Vector2 getKeyPosition()
        {
            for (int i = 0; i < a_KeyFrames.Count; i++)
            {
                if (i > 0)
                {
                    if (a_ElapsedTime <= a_KeyFrames[i].k_KeyTime && a_ElapsedTime > a_KeyFrames[i - 1].k_KeyTime)
                        return a_Origin + tweenKeyFramesPosition(a_KeyFrames[i - 1], a_KeyFrames[i], a_ElapsedTime);
                }
            }


            return a_Origin + Vector2.Zero;
        }


        private Vector2 tweenKeyFramesPosition(KeyFrame a, KeyFrame b, int time)
        {
            int timeBetweenFrames = b.k_KeyTime - a.k_KeyTime;
            int timeAfterA = time - a.k_KeyTime;
            float percentTween = (float)timeAfterA / (float)timeBetweenFrames;

            Vector2 aToB = b.k_KeyPosition - a.k_KeyPosition;

            return a.k_KeyPosition + (aToB * percentTween);
        }

        public float getKeyRotation()
        {
            for (int i = 0; i < a_KeyFrames.Count; i++)
            {
                if (i > 0)
                {
                    if (a_ElapsedTime <= a_KeyFrames[i].k_KeyTime && a_ElapsedTime > a_KeyFrames[i - 1].k_KeyTime)
                        return tweenKeyFramesRotation(a_KeyFrames[i - 1], a_KeyFrames[i], a_ElapsedTime);
                }
            }
            return a_Rotation;
        }


        private float tweenKeyFramesRotation(KeyFrame a, KeyFrame b, int time)
        {
            int timeBetweenFrames = b.k_KeyTime - a.k_KeyTime;
            int timeAfterA = time - a.k_KeyTime;
            float percentTween = (float)timeAfterA / (float)timeBetweenFrames;

            float aToB = b.k_KeyRotation - a.k_KeyRotation;

            return a.k_KeyRotation + aToB * percentTween;
        }
    }
}
