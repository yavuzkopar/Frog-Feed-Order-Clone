using UnityEngine;
namespace FrogFeedOrder.NodeSystem
{
    public class NodeGrid : MonoBehaviour
    {
        public static NodeGrid Instance { get; private set; }
        [SerializeField] int _mapSizeX, _mapSizeZ;
        public Node[,] _cells;
        private void Awake()
        {
            Instance = this;
            _cells = new Node[_mapSizeX, _mapSizeZ];
            Node[] cells = FindObjectsOfType<Node>();

            foreach (var item in cells)
            {
                int x = Mathf.RoundToInt(item.transform.position.x);
                int y = Mathf.RoundToInt(item.transform.position.z);
                _cells[x, y] = item;
            }
        }
        public Node GetNode(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x);
            int z = Mathf.RoundToInt(position.z);
            return GetNodeByGrid(x, z);
        }
        private Node GetNodeByGrid(int x, int y)
        {
            if (x < 0 || x >= _mapSizeX || y < 0 || y >= _mapSizeZ)
                return null;
            else
            {
                return _cells[x, y];
            }
        }
    }
}