using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogFeedOrder.UI
{
    public class EndGameUIOpening : MonoBehaviour
    {
        [SerializeField] AnimationCurve _animationCurve;
        [SerializeField] Transform uiParentObject;
        private void OnEnable() {
            StartCoroutine(OpeningSequence());
        }
        private IEnumerator OpeningSequence()
        {
            float time = 0;
            while (time<=1)
            {
                time+=Time.deltaTime;
                uiParentObject.localScale = Vector3.one * _animationCurve.Evaluate(time);
                yield return null;
            }
        }
    }
}
