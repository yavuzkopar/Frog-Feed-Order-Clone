using FrogFeedOrder.NodeSystem;
using UnityEngine;
namespace FrogFeedOrder.Control
{
    public class ClickManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
                {
                    if (hitInfo.collider.TryGetComponent(out Product product))
                    {
                        product.OnClicked();
                    }
                }
            }
        }
    }
}