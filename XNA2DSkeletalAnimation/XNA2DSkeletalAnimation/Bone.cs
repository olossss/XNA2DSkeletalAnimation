using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA2DSkeletalAnimation
{
    class Bone
    {
        private Bone b_Parent;

        public Bone Parent
        {
            get { return b_Parent; }
            set { b_Parent = value; }
        }

        private List<Bone> b_Children = new List<Bone>();

        public List<Bone> Children
        {
            get { return b_Children; }
            set { b_Children = value; }
        }

        private float b_Angle;

        public float Angle
        {
            get { return b_Angle; }
            set { b_Angle = value; }
        }

        private float b_MinAngle;

        public float MinAngle
        {
            get { return b_MinAngle; }
            set { b_MinAngle = value; }
        }

        private float b_MaxAngle;

        public float MaxAngle
        {
            get { return b_MaxAngle; }
            set { b_MaxAngle = value; }
        }

        private Vector2 b_Position;

        public Vector2 Position
        {
            get {
                if (this.Parent != null)
                    return this.Parent.Position + this.Parent.Direction * this.Parent.Length;
                else
                    return b_Position;
            }
            set { b_Position = value; }
        }

        private Vector2 b_RotationOrigin;

        public Vector2 RotationOrigin
        {
            get { return b_RotationOrigin; }
            set { b_RotationOrigin = value; }
        }

        private Vector2 b_Direction;

        public Vector2 Direction
        {
            get { return b_Direction; }
            set { b_Direction = value; }
        }

        private float b_Length;

        public float Length
        {
            get { return b_Length; }
            set { b_Length = value; }
        }

        private float b_AngleOff;

        public float AngleOff
        {
            get { return b_AngleOff; }
            set { b_AngleOff = value; }
        }

        private float b_LengthOff;

        public float LengthOff
        {
            get { return b_LengthOff; }
            set { b_LengthOff = value; }
        }

        private bool b_IsRootMaster = false;

        public bool IsRootMaster
        {
            get { return b_IsRootMaster; }
            set { b_IsRootMaster = value; }
        }

        private bool b_IsNoVis = false;

        public bool IsNoVis
        {
            get { return b_IsNoVis; }
            set { b_IsNoVis = value; }
        }

        private String b_TextureName;

        public String TextureName
        {
            get { return b_TextureName; }
            set { b_TextureName = value; }
        }

        private List<BoneKeyFrame> b_KeyFrames = new List<BoneKeyFrame>();

        public List<BoneKeyFrame> KeyFrames
        {
            get { return b_KeyFrames; }
            set { b_KeyFrames = value; }
        }
    }
}
