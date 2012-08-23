using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA2DSkeletalAnimation
{
    struct BoneKeyFrame
    {
        public int time;

        public float angle, length;

        public BoneKeyFrame(int tim, float ang, float len)
        {
            time = tim;
            angle = ang;
            length = len;
        }
    }
}
