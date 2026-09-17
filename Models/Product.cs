namespace ShopShowcase.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/img/placeholder.png";
        public string Tag { get; set; } = "Разное";

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }   // заполнено, если есть скидка
        public int Stock { get; set; }           // 0 = нет в наличии
        public bool IsHit { get; set; }

        public bool HasDiscount => OldPrice.HasValue && OldPrice.Value > Price;

        public int DiscountPercent =>
            HasDiscount
                ? (int)Math.Round(100 - (Price / OldPrice!.Value * 100))
                : 0;
    }
}
