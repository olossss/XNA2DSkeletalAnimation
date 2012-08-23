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
            get { return b_Position; }
            set { b_Position = value; }
        }

        private int b_Length;

        public int Length
        {
            get { return b_Length; }
            set { b_Length = value; }
        }

        private bool b_IsRootMaster = false;

        public bool IsRootMaster
        {
            get { return b_IsRootMaster; }
            set { b_IsRootMaster = value; }
        }

        private String b_TextureName;

        public String TextureName
        {
            get { return b_TextureName; }
            set { b_TextureName = value; }
        }

        private List<BoneKeyFrame> b_KeyFrames;

        public List<BoneKeyFrame> KeyFrames
        {
            get { return b_KeyFrames; }
            set { b_KeyFrames = value; }
        }
    }
}
