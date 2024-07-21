using System.Collections;
using System.Collections.Generic;
using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem.Products
{
    public class Grape : CollectableProduct
    {
        [SerializeField] private PlayerSettingsSO _playerSettings;
        private int _moveIndex;
        private MeshRenderer _meshRenderer;
        private List<Node> _route = new();
        private void OnEnable()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
        public override void OnClicked()
        {
            base.OnClicked();
            StartSizeRoutine(_playerSettings);
        }
        public override void HittedByTongue(ColorEnum tongueColor, Transform mover)
        {
            if (tongueColor == GetColor())
            {
                //Do things
                StartSizeRoutine(_playerSettings);
            }
            else
            {
                StartCoroutine(ColorRoutine());
                // do wrongThings
            }
        }
        public override void StartCollecting(List<Node> route)
        {
            var currentNode = NodeGrid.Instance.GetNode(transform.position);
            _moveIndex = route.IndexOf(currentNode);
            _route = route;
            StartCoroutine(MoveToNextPoint(route));
            _isMoving = true;
        }
        private IEnumerator ColorRoutine()
        {
            Color startColor = _meshRenderer.material.color;
            _meshRenderer.material.color = Color.red;
            float time = 0;
            while (time <= 1)
            {
                time += Time.deltaTime;
                _meshRenderer.material.color = Color.Lerp(Color.red, startColor, time);
                yield return null;
            }
        }

        private IEnumerator MoveToNextPoint(List<Node> route)
        {
            _moveIndex -= 1;
            if (_moveIndex < 0)
            {
                yield break;
            }
            var target = route[_moveIndex].GetTopCell().transform.position + Vector3.up * 0.2f;
            float travelDistance = 0;
            Vector3 startPosition = transform.position;
            while (travelDistance <= 1)
            {
                travelDistance += Time.deltaTime * _playerSettings.TongueMoveSpeed;
                if (_moveIndex == 0)
                {
                    transform.localScale = (1 - travelDistance) * Vector3.one;
                }
                transform.position = Vector3.Lerp(startPosition, target, travelDistance);
                yield return null;

            }
            if (_moveIndex == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(MoveToNextPoint(route));
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CollectableProduct grape))
            {
                if (_isMoving && !grape.IsMoving())
                {
                    grape.StartCollecting(_route);
                }
            }
        }
    }
}