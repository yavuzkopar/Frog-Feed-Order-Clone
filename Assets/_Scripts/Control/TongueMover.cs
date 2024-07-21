using System;
using System.Collections;
using System.Collections.Generic;
using FrogFeedOrder.Core;
using FrogFeedOrder.NodeSystem;
using UnityEngine;
namespace FrogFeedOrder.Control
{
    public class TongueMover : MonoBehaviour
    {
        [SerializeField] private PlayerSettingsSO playerSettings;
        public Action OnReachedACell;
        public Action OnComplatedSuccesfully;
        public Action OnReachedPreviousPoint;
        private List<Node> _cells = new List<Node>();
        private int cellIndex;
        private bool _canMove = true;
        public bool CanMove => _canMove;
        #region  Go Forward
        public void GoTo(Vector3 position)
        {
            _cells = new();
            StartCoroutine(GoToPositionRoutine(position));
            _canMove = false;
        }
        private IEnumerator GoToPositionRoutine(Vector3 position)
        {
            float travelDistance = 0;
            Vector3 startPosition = transform.position;
            while (travelDistance <= 1)
            {
                travelDistance += Time.deltaTime * playerSettings.TongueMoveSpeed; ;
                transform.position = Vector3.Lerp(startPosition, position, travelDistance);
                yield return null;
            }
            OnReachedACell?.Invoke();

        }
        #endregion
        #region Go Back In Success
        public void TurnBackInSuccess(List<Node> cells)
        {
            _cells = cells;
            cellIndex = cells.Count - 1;
            StartCoroutine(TurnBackInSuccessRoutine(cells));
        }
        private IEnumerator TurnBackInSuccessRoutine(List<Node> cells)
        {
            Node previousNode = GetNode(transform.position);
            cellIndex--;
            if (cellIndex < 0)
            {
                RemoveLastCell();
                yield break;
            }

            var target = _cells[cellIndex].GetTopCell().transform.position;
            float travelDistance = 0;
            Vector3 startPosition = transform.position;
            while (travelDistance <= 1)
            {
                if (cellIndex == 0)
                {
                    transform.localScale = (1 - travelDistance) * Vector3.one;
                }
                travelDistance += Time.deltaTime * playerSettings.TongueMoveSpeed;
                transform.position = Vector3.Lerp(startPosition, target, travelDistance);
                RemovePreviousCell(ref previousNode);
                yield return null;
            }
            OnReachedPreviousPoint?.Invoke();
            StartCoroutine(TurnBackInSuccessRoutine(cells));
        }
        private void RemoveLastCell()
        {
            var lastNode = GetNode(transform.position);
            var lastCell = lastNode.GetTopCell();
            OnComplatedSuccesfully?.Invoke();
            lastCell.GetCellProduct().gameObject.SetActive(false);
            lastNode.RemoveCell(lastCell);
        }
        private void RemovePreviousCell(ref Node previousNode)
        {
            Node currentNode = GetNode(transform.position);
            if (currentNode != previousNode)
            {
                var tempCell = previousNode.GetTopCell();
                previousNode.RemoveCell(tempCell);
                previousNode = currentNode;
            }
        }
        #endregion
        #region  Go Back In Fail
        public void TurnBackInFail(List<Node> cells)
        {
            _cells = cells;
            cellIndex = cells.Count - 1;
            StartCoroutine(TurnBackInFailRoutine(cells));
        }

        private IEnumerator TurnBackInFailRoutine(List<Node> cells)
        {
            cellIndex--;
            if (cellIndex < 0)
            {
                yield break;
            }
            yield return GoBackRoutine(cells);

            StartCoroutine(TurnBackInFailRoutine(cells));
        }


        private IEnumerator GoBackRoutine(List<Node> cells)
        {
            var target = cells[cellIndex].GetTopCell().transform.position;
            float travelDistance = 0;
            Vector3 startPosition = transform.position;
            while (travelDistance <= 1)
            {
                if (cellIndex == 0)
                {
                    transform.localScale = (1 - travelDistance) * Vector3.one;

                }
                travelDistance += Time.deltaTime * playerSettings.TongueMoveSpeed;
                transform.position = Vector3.Lerp(startPosition, target, travelDistance);
                yield return null;
            }
            if (cellIndex == 0)
            {
                _canMove = true;
            }
            OnReachedPreviousPoint?.Invoke();
        }
        #endregion
        private Node GetNode(Vector3 position)
        {
            return NodeGrid.Instance.GetNode(position);
        }

    }
}