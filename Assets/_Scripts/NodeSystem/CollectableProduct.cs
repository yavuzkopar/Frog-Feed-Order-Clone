using System.Collections.Generic;
namespace FrogFeedOrder.NodeSystem
{
    public abstract class CollectableProduct : Product
    {
        public abstract void StartCollecting(List<Node> route);
        protected bool _isMoving = false;
        public bool IsMoving() => _isMoving;
    }
}