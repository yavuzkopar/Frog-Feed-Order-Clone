using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem.Products
{
    public class Arrow : Product
    {
        public override void HittedByTongue(ColorEnum tongueColor, Transform mover)
        {
            transform.parent = NodeGrid.Instance.GetNode(transform.position).GetTopCell().transform;
            if (tongueColor == GetColor())
            {
                mover.forward = transform.forward;
            }
        }
    }
}
