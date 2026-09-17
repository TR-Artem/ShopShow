using System.Text.Json;

namespace ShopShowcase.Models
{
    public static class SessionExtensions
    {
        private const string CartKey = "Cart";

        public static List<CartItem> GetCart(this ISession session)
        {
            var json = session.GetString(CartKey);
            return string.IsNullOrEmpty(json)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        public static void SaveCart(this ISession session, List<CartItem> cart)
        {
            session.SetString(CartKey, JsonSerializer.Serialize(cart));
        }
    }
}
