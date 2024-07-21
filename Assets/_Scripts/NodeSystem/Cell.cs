using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private ProductDataSO _productDataSO;
        [SerializeField] private ProductEnum _products;
        private Product _cellProduct;
        private ColorEnum _color;
        private void Start()
        {
            _color = _productDataSO.colorEnum;
        }
        public void InstantiateCellProduct()
        {
            _cellProduct = _productDataSO.GetCellProduct(_products);
            _cellProduct.transform.position = transform.position + Vector3.up * 0.2f;
            _cellProduct.transform.rotation = transform.rotation;
            _cellProduct.SetColor(_productDataSO.colorEnum);
        }
        public Product GetCellProduct() => _cellProduct;
        public ColorEnum GetColor() => _color;
        public ProductEnum GetProductsEnum() => _products;
    }
}