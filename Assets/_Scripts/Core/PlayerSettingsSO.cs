using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogFeedOrder.Core
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "PlayerSettingsSO", order = 0)]
    public class PlayerSettingsSO : ScriptableObject {
        [SerializeField] float _tongueMoveSpeed;
        [SerializeField] float _cellChangeSpeed;
        [SerializeField] AnimationCurve sizeArrangeCurve;
        public float TongueMoveSpeed=> _tongueMoveSpeed;
        public float CellChangeSpeed => _cellChangeSpeed;
        public AnimationCurve SizeArrangeCurve => sizeArrangeCurve;
    }
}
