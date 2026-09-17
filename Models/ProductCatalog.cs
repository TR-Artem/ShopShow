namespace ShopShowcase.Models
{
    // Простое статическое "хранилище" товаров — для практической работы
    // достаточно in-memory списка, без базы данных.
    public static class ProductCatalog
    {
        public static readonly List<Product> All = new()
        {
            new Product { Id = 1, Name = "Беспроводные наушники", Description = "Bluetooth 5.3, шумоподавление", Price = 3990, OldPrice = 5990, Stock = 12, IsHit = true,  Tag = "Электроника", ImageUrl = "https://placehold.co/400x300?text=Headphones" },
            new Product { Id = 2, Name = "Смарт-часы",            Description = "AMOLED экран, до 10 дней автономности", Price = 7490, Stock = 0,  IsHit = false, Tag = "Электроника", ImageUrl = "https://placehold.co/400x300?text=Smartwatch" },
            new Product { Id = 3, Name = "Механическая клавиатура", Description = "Hot-swap, RGB подсветка", Price = 5290, Stock = 7, IsHit = true, Tag = "Электроника", ImageUrl = "https://placehold.co/400x300?text=Keyboard" },
            new Product { Id = 4, Name = "Куртка демисезонная",   Description = "Мембрана 10000мм, водоотталкивающая", Price = 6990, OldPrice = 10990, Stock = 5, IsHit = false, Tag = "Одежда", ImageUrl = "https://placehold.co/400x300?text=Jacket" },
            new Product { Id = 5, Name = "Кроссовки беговые",     Description = "Лёгкая амортизирующая подошва", Price = 4590, Stock = 15, IsHit = true, Tag = "Одежда", ImageUrl = "https://placehold.co/400x300?text=Sneakers" },
            new Product { Id = 6, Name = "Худи оверсайз",         Description = "100% хлопок, унисекс", Price = 2490, Stock = 0, IsHit = false, Tag = "Одежда", ImageUrl = "https://placehold.co/400x300?text=Hoodie" },
            new Product { Id = 7, Name = "Учебник по C#",         Description = "Полное руководство для начинающих", Price = 1290, Stock = 20, IsHit = false, Tag = "Книги", ImageUrl = "https://placehold.co/400x300?text=Book" },
            new Product { Id = 8, Name = "Фантастический роман",  Description = "Бестселлер, твёрдый переплёт", Price = 890, OldPrice = 1290, Stock = 30, IsHit = true, Tag = "Книги", ImageUrl = "https://placehold.co/400x300?text=Novel" },
            new Product { Id = 9, Name = "Ежедневник",            Description = "А5, недатированный", Price = 590, Stock = 40, IsHit = false, Tag = "Книги", ImageUrl = "https://placehold.co/400x300?text=Planner" },
        };

        public static List<string> Tags => All.Select(p => p.Tag).Distinct().OrderBy(t => t).ToList();
    }
}
