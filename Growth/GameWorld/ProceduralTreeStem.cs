using System;
using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    internal class ProceduralTreeStem : ProceduralTreeBranch
    {
        public override Vector2 Origin => new Lazy<Vector2>(() => Vector2.Zero).Value;
        public override float Thickness => 6f;

        internal ProceduralTreeStem(ProceduralTree tree, Vector2 vector) : base(tree, null, vector)
        {
        }
    }
}