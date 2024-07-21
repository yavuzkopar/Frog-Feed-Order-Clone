using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FrogFeedOrder.Core;
namespace FrogFeedOrder.NodeSystem
{
    public class Node : MonoBehaviour
    {
        private List<Cell> _cells = new();
        [SerializeField] PlayerSettingsSO _playerSettings;
        private void Start()
        {
            Ray ray = new Ray(transform.position, Vector3.up);
            var testCells = Physics.RaycastAll(ray, 10f);
            for (int i = 0; i < testCells.Length; i++)
            {
                if (testCells[i].collider.TryGetComponent(out Cell testCell))
                {
                    _cells.Add(testCell);
                }
            }
            _cells = _cells.OrderBy(x => x.transform.position.y).ToList();
            GetTopCell()?.InstantiateCellProduct();
        }
        public void RemoveCell(Cell cell)
        {
            StartCoroutine(ChangeCell(cell));
        }
        public Cell GetTopCell()
        {
            if (_cells.Count == 0) return null;
            return _cells[_cells.Count - 1];
        }
        private IEnumerator ChangeCell(Cell cell)
        {
            yield return RemoveOldCell(cell);
            yield return GenerateNewCell();
        }
        private IEnumerator RemoveOldCell(Cell cell)
        {
            _cells.Remove(cell);
            float cellSize = 1f;
            float cellChangeSpeed = _playerSettings.CellChangeSpeed;
            while (cellSize >= 0)
            {
                cellSize -= Time.deltaTime * cellChangeSpeed;
                cell.transform.localScale = Vector3.one * cellSize;
                yield return null;
            }
            cell.gameObject.SetActive(false);
        }
        private IEnumerator GenerateNewCell()
        {
            float cellSize = 1f;
            float cellChangeSpeed = _playerSettings.CellChangeSpeed;
            GetTopCell()?.InstantiateCellProduct();
            var newProduct = GetTopCell()?.GetCellProduct();
            if (newProduct == null)
            {
                yield break;
            }
            while (cellSize <= 1)
            {
                cellSize += Time.deltaTime * cellChangeSpeed;
                newProduct.transform.localScale = Vector3.one * cellSize;
                yield return null;
            }
            newProduct.transform.localScale = Vector3.one;
        }
    }
}