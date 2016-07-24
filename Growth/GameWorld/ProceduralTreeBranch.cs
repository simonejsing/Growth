using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    public class ProceduralTreeBranch
    {
        private List<ProceduralTreeBranch> branchList = new List<ProceduralTreeBranch>();

        public IEnumerable<ProceduralTreeBranch> Branches => branchList;

        public ProceduralTreeBranch Parent { get; set; }
        public Vector2 Vector { get; private set; }
        public virtual float Thickness => 1f;

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