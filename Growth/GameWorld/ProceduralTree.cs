using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorMath;

namespace GameWorld
{
    public class ProceduralTree
    {
        public Random Random { get; }

        public ProceduralTreeBranch Stem { get; private set; }

        public ProceduralTree(Random random)
        {
            Random = random;
            Stem = new ProceduralTreeStem(this, new Vector2(0, 100));
        }

        public void AddBranch(ProceduralTreeBranch parentBranch, ProceduralTreeBranch newBranch)
        {
            parentBranch.AddBranch(newBranch);
        }
    }
}
