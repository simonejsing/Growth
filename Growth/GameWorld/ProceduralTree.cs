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
        private int Seed;

        public ProceduralTreeBranch Stem { get; private set; }

        public ProceduralTree(int seed)
        {
            Seed = seed;
            Stem = new ProceduralTreeStem(new Vector2(0, 100));
        }

        public void AddBranch(ProceduralTreeBranch parentBranch, ProceduralTreeBranch newBranch)
        {
            parentBranch.AddBranch(newBranch);
        }
    }
}
