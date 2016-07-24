using System;
using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    public class ProceduralTreeBranch
    {
        private List<ProceduralTreeBranch> branchList = new List<ProceduralTreeBranch>();

        public IEnumerable<ProceduralTreeBranch> Branches => branchList;

        public ProceduralTreeBranch Parent { get; set; }
        public virtual Vector2 Origin => new Lazy<Vector2>(() => Parent.Origin + Parent.Vector).Value;
        public Vector2 Vector { get; private set; }
        public virtual float Thickness => new Lazy<float>(() => Math.Max(1.0f, Parent.Thickness - 1.0f)).Value;

        internal ProceduralTreeBranch(ProceduralTreeBranch parent, Vector2 vector)
        {
            Parent = parent;
            Vector = vector;
        }

        public void AddBranch(ProceduralTreeBranch newBranch)
        {
            branchList.Add(newBranch);
        }
    }
}