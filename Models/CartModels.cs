namespace ShopShowcase.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;

        public decimal LineTotal => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new();
        public bool JustAdded { get; set; }

        public decimal TotalAmount => Items.Sum(i => i.LineTotal);
        public bool IsEmpty => Items.Count == 0;
    }

    // Used inside the "Подтверждение заказа" modal
    public class OrderViewModel
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    public class CatalogViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string? ActiveTag { get; set; }
    }
}
