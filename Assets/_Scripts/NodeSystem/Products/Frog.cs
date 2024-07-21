using System.Collections;
using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem.Products
{
    public class Frog : Product
    {
        [SerializeField] private PlayerSettingsSO _playerSettings;
        public override void OnClicked()
        {
            base.OnClicked();
            StartSizeRoutine(_playerSettings);
        }
    }
}