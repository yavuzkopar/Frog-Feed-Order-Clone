using System.Collections.Generic;
using UnityEngine;
using FrogFeedOrder.NodeSystem;
using FrogFeedOrder.Core;
using System;
namespace FrogFeedOrder.Control
{
    public class TongueManager : MonoBehaviour
    {
        [SerializeField] private TongueMover _tongueMover;
        [SerializeField] private LineRenderer _lineRenderer;
        private ColorEnum _color;
        private List<Node> _nodes = new();
        private bool _goForward = true;

        #region Unity Methods
        private void Start()
        {
            SetProduct();
            SetTongueMover();
            SetLineRenderer();

        }
        private void LateUpdate()
        {
            if (_lineRenderer.positionCount > 0)
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueMover.transform.position);
        }
        #endregion
        #region Setups
        private void SetProduct()
        {
            Product product = GetComponent<Product>();
            _color = product.GetColor();
            product.OnClickEvent += StartMoving;
        }

        private void SetTongueMover()
        {
            if (_tongueMover == null) return;
            _tongueMover.OnReachedACell += TongueMover_OnReachedACell;
            _tongueMover.OnComplatedSuccesfully += TongueMover_OnComplated;
            _tongueMover.OnReachedPreviousPoint += TongueMover_OnReachedPrevious;
        }
        private void SetLineRenderer()
        {
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, _tongueMover.transform.position);
        }
        #endregion
        #region Tongue Events
        private void StartMoving()
        {
            if (!_tongueMover.CanMove) return;
            _tongueMover.gameObject.SetActive(true);
            _tongueMover.transform.forward = transform.forward;
            _tongueMover.GoTo(GetTargetPosition());
            _nodes.Clear();
            _nodes.Add(GetNode(_tongueMover.transform.position));
            _lineRenderer.positionCount = 2;
            _goForward = true;
            MoveCounter.Instance.UpdateMoveCount();
        }
        private void TongueMover_OnReachedACell()
        {
            GetNode(_tongueMover.transform.position).GetTopCell()?.GetCellProduct()?.HittedByTongue(_color, _tongueMover.transform);
            if (!_goForward) return;
            _nodes.Add(GetNode(_tongueMover.transform.position));
            if (IsCurrentCellSameColor())
            {
                if (IsOutOfBounds())
                    TurnBackInSuccess();
                else
                    KeepMoving();
            }
            else
            {
                TurnBackInFail();
            }
        }
        private void TongueMover_OnComplated()
        {
            MoveCounter.Instance.MoveComplated();
        }
        private void TongueMover_OnReachedPrevious()
        {
            _lineRenderer.positionCount--;
        }
        #endregion
        #region Tongue Behaviours
        private void TurnBackInFail()
        {
            _tongueMover.TurnBackInFail(_nodes);
            _goForward = false;
        }
        private void KeepMoving()
        {
            _tongueMover.GoTo(GetTargetPosition());
            _lineRenderer.positionCount++;
        }
        private void TurnBackInSuccess()
        {
            CollectableProduct target = GetNode(_tongueMover.transform.position).GetTopCell().GetCellProduct() as CollectableProduct;
            target.StartCollecting(_nodes);
            _tongueMover.TurnBackInSuccess(_nodes);
            _goForward = false;
        }
        #endregion
        #region Node Controls
        private bool IsOutOfBounds()
        {
            Vector3 target = _tongueMover.transform.position + _tongueMover.transform.forward;
            var testCell = GetNode(target);
            if (testCell == null)
            {
                // is out of bounds
                return true;
            }
            else
            {
                // is next node is empty
                return testCell.GetTopCell() == null;
            }
        }
        private bool IsCurrentCellSameColor()
        {
            var testCell = GetNode(_tongueMover.transform.position).GetTopCell();
            return testCell.GetColor() == _color;
        }
        #endregion
        #region Getters
        public void SetColor(ColorEnum color) => _color = color;
        public ColorEnum GetColor() => _color;
        private Node GetNode(Vector3 position)
        {
            return NodeGrid.Instance.GetNode(position);
        }
        private Vector3 GetTargetPosition()
        {
            Vector3 target = _tongueMover.transform.position + _tongueMover.transform.forward;
            var testCell = GetNode(target);
            if (testCell != null)
            {
                var product = testCell.GetTopCell();
                return product.transform.position + Vector3.up * 0.2f; ;
            }
            else
            {
                return Vector3.zero;
            }
        }
        #endregion
    }
}