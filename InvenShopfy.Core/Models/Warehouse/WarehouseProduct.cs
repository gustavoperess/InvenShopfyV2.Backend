namespace InvenShopfy.Core.Models.Warehouse;

public class WarehouseProduct
{
    public long ProductId { get; set; }
    public Product.Product Product { get; set; } = null!;
    public long WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public int Quantity { get; set; }
    
    public WarehouseProduct? AddProductIdAndAmountToWarehouse(
        Dictionary<long, int> productIdPlusQuantity, List<WarehouseProduct> warehouseProducts, long warehouseId)
    {
        
        foreach (var item in productIdPlusQuantity)
        {
            var existingProduct = warehouseProducts.FirstOrDefault(x => x.ProductId == item.Key && x.WarehouseId == warehouseId);
        
            if (existingProduct != null)
            {
                existingProduct.Quantity += item.Value;
            }
            else
            {
                var newWarehouseProduct = new WarehouseProduct
                {
                    WarehouseId = warehouseId,
                    ProductId = item.Key,
                    Quantity = item.Value
                };
                
                warehouseProducts.Add(newWarehouseProduct);
                return newWarehouseProduct;
            }
        }
        return null;
    }

}