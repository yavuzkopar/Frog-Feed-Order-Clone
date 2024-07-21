using System;
using System.Collections;
using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem
{
    public class Product : MonoBehaviour
    {
        private ColorEnum _color;
        public Action OnClickEvent;

        public virtual void OnClicked()
        {
            //hap tick
            OnClickEvent?.Invoke();
        }
        public void SetColor(ColorEnum color)
        {
            _color = color;
        }
        public ColorEnum GetColor() => _color;
        public virtual void HittedByTongue(ColorEnum tongueColor, Transform mover) { }

        protected void StartSizeRoutine(PlayerSettingsSO playerSettings)
        {
            StartCoroutine(SizeRoutine(playerSettings));
        }
        private IEnumerator SizeRoutine(PlayerSettingsSO playerSettings)
        {
            float time = 0;
            float evaluateSpeed = 5f;
            while (time <= 1)
            {
                time += Time.deltaTime * evaluateSpeed;
                transform.localScale = Vector3.one * playerSettings.SizeArrangeCurve.Evaluate(time);
                yield return null;
            }
        }
    }
}