using FrogFeedOrder.NodeSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace FrogFeedOrder.Control
{
    public class MoveCounter : MonoBehaviour
    {
        public static MoveCounter Instance { get; private set; }
        public UnityEvent OutOfMovesEvent;
        public UnityEvent OnLevelComplated;
        [SerializeField] TextMeshProUGUI _MoveCounterText;
        [SerializeField] int _remainingMoves;
        [SerializeField] int _neededMoves;
        private void Awake()
        {
            Instance = this;

        }
        private void Start()
        {
            var cells = FindObjectsOfType<Cell>();
            foreach (var item in cells)
            {
                if (item.GetProductsEnum() == ProductEnum.Frog)
                    _neededMoves++;
            }
            _remainingMoves = _neededMoves + 4;
            _MoveCounterText.text = $"{_remainingMoves} MOVES";
        }
        public void UpdateMoveCount()
        {
            _remainingMoves--;
            if (_remainingMoves <= 0)
            {
                OutOfMovesEvent?.Invoke();
                return;
            }
            _MoveCounterText.text = $"{_remainingMoves} MOVES";
        }
        public void MoveComplated()
        {
            _neededMoves--;
            if (_neededMoves <= 0)
            {
                OnLevelComplated?.Invoke();
            }
        }
    }
}