using FrogFeedOrder.Core;
using UnityEngine;
namespace FrogFeedOrder.NodeSystem
{
    [CreateAssetMenu(fileName = "ProductDataSO", menuName = "ProductDataSO", order = 0)]
    public class ProductDataSO : ScriptableObject
    {
        public ColorEnum colorEnum;
        public ProductData[] _products;
        public Product GetCellProduct(ProductEnum productsEnum)
        {
            foreach (var item in _products)
            {
                if (item._product == productsEnum)
                {
                    Product tempProduct = Instantiate(item._productPrefab);
                    return tempProduct;
                }
            }
            return null;
        }
    }
    [System.Serializable]
    public class ProductData
    {
        public ProductEnum _product;
        public Product _productPrefab;
    }
    public enum ProductEnum
    {
        Grape,
        Frog,
        Arrow
    }
}