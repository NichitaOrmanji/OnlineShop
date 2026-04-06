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
        }

        // 1. Смена валюты (Adapter)
        public IActionResult OnPostChangeCurrency(string currency)
        {
            ShopSettings.GetInstance().Currency = currency;
            ShopSettings.GetInstance().AddLog($"Валюта изменена на {currency}");
            return RedirectToPage();
        }

        // 2. Добавление одиночного товара
        public IActionResult OnPostAddToCart(string item, double price)
        {
            var settings = ShopSettings.GetInstance();
            string formattedPrice = FormatPrice(price);
            settings.Cart.Add($"{item} ({formattedPrice})");
            settings.AddLog($"В корзину добавлен: {item}");
            return RedirectToPage();
        }

        // --- НОВЫЙ МЕТОД: Покупка линейки товаров (Abstract Factory) ---
        public IActionResult OnPostBuyGadget(string type)
        {
            var settings = ShopSettings.GetInstance();
            // Выбираем конкретную фабрику
            IShopFactory factory = type == "premium" ? new PremiumShopFactory() : new BudgetShopFactory();
            
            // Создаем продукты через фабрику
            var gadget = factory.CreateGadget();
            var laptop = factory.CreateLaptop();
            
            // Добавляем в корзину сразу комплектом
            settings.Cart.Add($"Комплект: {gadget.GetDetails()}");
            settings.Cart.Add($"Комплект: {laptop.GetDetails()}");
            
            settings.AddLog($"[Abstract Factory] Добавлен комплект оборудования ({type})");
            return RedirectToPage();
        }

        // --- НОВЫЙ МЕТОД: Сборка ПК (Builder) ---
        public IActionResult OnPostBuildPc(string pcType)
        {
            var settings = ShopSettings.GetInstance();
            var manager = new ShopManager();
            
            // Выбираем строителя
            IComputerBuilder builder = pcType == "gaming" ? new GamingComputerBuilder() : new OfficeComputerBuilder();
            
            // Процесс пошаговой сборки
            manager.Construct(builder);
            var pc = builder.GetResult();
            
            // Добавляем результат сборки в корзину
            settings.Cart.Add($"Сборка ПК ({pcType}): {string.Join(", ", pc.GetParts())}");
            
            settings.AddLog($"[Builder] Сформирована конфигурация {pcType} ПК");
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
            settings.Cart.Clear(); 
            
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
        public IActionResult OnPostRemoveFromCart(int index)
        {
                var settings = ShopSettings.GetInstance();
                settings.RemoveFromCart(index);
                return RedirectToPage();
        }
    }

}