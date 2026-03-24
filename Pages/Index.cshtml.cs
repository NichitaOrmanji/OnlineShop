using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop;
namespace OnlineShopProject_TMPPP.Pages 
{
    public class IndexModel : PageModel
    {
        // Вспомогательный метод для отображения цены с учетом Адаптера
        public string FormatPrice(double usdPrice)
        {
            var settings = ShopSettings.GetInstance();
            if (settings.Currency == "EUR") return new EuroAdapter().GetConvertedPrice(usdPrice);
            if (settings.Currency == "MDL") return new LeuAdapter().GetConvertedPrice(usdPrice);
            return $"${usdPrice}";
        }

        public void OnGet()
        {
            // Просто обновляем данные при загрузке
        }

        // 1. Смена валюты (Adapter)
        public IActionResult OnPostChangeCurrency(string currency)
        {
            ShopSettings.GetInstance().Currency = currency;
            ShopSettings.GetInstance().AddLog($"Валюта изменена на {currency}");
            return RedirectToPage();
        }

        // 2. Добавление в корзину
        public IActionResult OnPostAddToCart(string item, double price)
        {
            var settings = ShopSettings.GetInstance();
            string formattedPrice = FormatPrice(price);
            settings.Cart.Add($"{item} ({formattedPrice})");
            settings.AddLog($"В корзину добавлен: {item}");
            return RedirectToPage();
        }

        // 3. Оформление заказа (Factory Method)
        public IActionResult OnPostCheckout(string deliveryType)
        {
            var settings = ShopSettings.GetInstance();
            if (settings.Cart.Count == 0) return RedirectToPage();

            // Используем Factory Method для выбора доставки
            DeliveryService logistics = deliveryType == "air" ? new AirLogistics() : new GroundLogistics();
            var delivery = logistics.CreateDelivery();

            // Создаем объект заказа и сохраняем его (для Prototype)
            var order = new SimpleOrder(string.Join(", ", settings.Cart), 0) { CustomerName = "Никита" };
            settings.CompletedOrders.Add(order);

            settings.AddLog($"Заказ оформлен! {delivery.GetDeliveryInfo()}");
            settings.Cart.Clear(); // Очищаем корзину
            
            return RedirectToPage();
        }

        // 4. Повтор последнего заказа (Prototype)
        public IActionResult OnPostRepeatLast()
        {
            var settings = ShopSettings.GetInstance();
            if (settings.CompletedOrders.Count > 0)
            {
                var lastOrder = settings.CompletedOrders[settings.CompletedOrders.Count - 1];
                var clonedOrder = (SimpleOrder)lastOrder.Clone();
                settings.AddLog($"[Prototype] Скопирован заказ: {clonedOrder.ProductName}");
            }
            return RedirectToPage();
        }
    }
}