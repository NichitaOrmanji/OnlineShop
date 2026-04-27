using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop;

namespace OnlineShopProject_TMPPP.Pages 
{
    public class IndexModel : PageModel
    {
        public List<Product> SortedProducts { get; set; } = new List<Product>();
        public string CurrentSortName { get; set; } = "";
        public bool CanUndo { get; set; } = false;

        public int ObserverItemCount { get; set; } = 0;
        public double ObserverTotal { get; set; } = 0;
        public string ObserverLastLog { get; set; } = "";

        public string FormatPrice(double usdPrice)
        {
            var settings = ShopSettings.GetInstance();
            if (settings.Currency == "EUR") return new EuroAdapter().GetConvertedPrice(usdPrice);
            if (settings.Currency == "MDL") return new LeuAdapter().GetConvertedPrice(usdPrice);
            return $"${usdPrice}";
        }

        public void OnGet(string sort = "price_asc")
        {
            var settings = ShopSettings.GetInstance();

            ISortStrategy strategy = sort switch
            {
                "price_desc" => new SortByPriceDesc(),
                "name"       => new SortByName(),
                _            => new SortByPriceAsc()
            };

            var catalog = new ProductCatalog(strategy);
            SortedProducts = catalog.GetSortedProducts();
            CurrentSortName = catalog.GetCurrentStrategyName();

            settings.AddLog($"[Strategy] Каталог отсортирован: {CurrentSortName}");

            CanUndo = CartCommandHistory.GetInstance().HasHistory();

            var manager = CartObserverManager.GetInstance();
            manager.NotifyAll();
            ObserverItemCount = manager.Counter.ItemCount;
            ObserverTotal     = manager.Total.Total;
            ObserverLastLog   = manager.Logger.LastMessage;
        }

        // Смена валюты (Adapter)
        public IActionResult OnPostChangeCurrency(string currency)
        {
            ShopSettings.GetInstance().Currency = currency;
            ShopSettings.GetInstance().AddLog($"Валюта изменена на {currency}");
            return RedirectToPage();
        }

        // Добавление товара (Decorator + Command + Observer)
        // giftWrap передаётся из чекбокса на карточке товара
        public IActionResult OnPostAddToCart(string item, double price, bool giftWrap = false)
        {
            var settings = ShopSettings.GetInstance();

            // Базовый продукт — всегда
            IShoppable product = new SimpleProduct(item, price);

            // Decorator 1: расширенная гарантия — только для дорогих товаров
            if (price >= 1000)
                product = new ExtraWarrantyDecorator(product);

            // Decorator 2: подарочная упаковка — только если пользователь выбрал
            if (giftWrap)
                product = new GiftWrapDecorator(product);

            string finalName      = product.GetDescription();
            double finalPrice     = product.GetPrice();
            string formattedPrice = FormatPrice(finalPrice);
            string cartItem       = $"{finalName} ({formattedPrice})";

            // Command
            var receiver = new CartReceiver();
            var command  = new AddProductCommand(receiver, cartItem);
            CartCommandHistory.GetInstance().ExecuteCommand(command);

            // Observer
            CartObserverManager.GetInstance().NotifyAll();

            return RedirectToPage();
        }

        // Покупка комплекта (Abstract Factory)
        public IActionResult OnPostBuyGadget(string type)
        {
            var settings = ShopSettings.GetInstance();
            IShopFactory factory = type == "premium" ? new PremiumShopFactory() : new BudgetShopFactory();
            
            var gadget = factory.CreateGadget();
            var laptop = factory.CreateLaptop();
            
            settings.Cart.Add($"Комплект: {gadget.GetDetails()}");
            settings.Cart.Add($"Комплект: {laptop.GetDetails()}");
            
            settings.AddLog($"[Abstract Factory] Добавлен комплект оборудования ({type})");
            CartObserverManager.GetInstance().NotifyAll();

            return RedirectToPage();
        }

        // Сборка ПК (Builder)
        public IActionResult OnPostBuildPc(string pcType)
        {
            var settings = ShopSettings.GetInstance();
            var manager  = new ShopManager();
            
            IComputerBuilder builder = pcType == "gaming" ? new GamingComputerBuilder() : new OfficeComputerBuilder();
            
            manager.Construct(builder);
            var pc = builder.GetResult();
            
            settings.Cart.Add($"Сборка ПК ({pcType}): {string.Join(", ", pc.GetParts())}");
            settings.AddLog($"[Builder] Сформирована конфигурация {pcType} ПК");
            CartObserverManager.GetInstance().NotifyAll();

            return RedirectToPage();
        }

        // Оформление заказа (Factory Method)
        public IActionResult OnPostCheckout(string deliveryType)
        {
            var settings = ShopSettings.GetInstance();
            if (settings.Cart.Count == 0) return RedirectToPage();

            DeliveryService logistics = deliveryType == "air" ? new AirLogistics() : new GroundLogistics();
            var delivery = logistics.CreateDelivery();

            var order = new SimpleOrder(string.Join(", ", settings.Cart), 0) { CustomerName = "Никита" };
            settings.CompletedOrders.Add(order);

            settings.AddLog($"Заказ оформлен! {delivery.GetDeliveryInfo()}");
            settings.Cart.Clear();
            CartObserverManager.GetInstance().NotifyAll();
            
            return RedirectToPage();
        }

        // Повтор заказа (Prototype)
        public IActionResult OnPostRepeatLast()
        {
            var settings = ShopSettings.GetInstance();
            if (settings.CompletedOrders.Count > 0)
            {
                var lastOrder   = settings.CompletedOrders[settings.CompletedOrders.Count - 1];
                var clonedOrder = (SimpleOrder)lastOrder.Clone();
                settings.AddLog($"[Prototype] Скопирован заказ: {clonedOrder.ProductName}");
            }
            return RedirectToPage();
        }

        // Удаление из корзины (Command + Observer)
        public IActionResult OnPostRemoveFromCart(int index)
        {
            var settings = ShopSettings.GetInstance();

            if (index >= 0 && index < settings.Cart.Count)
            {
                string item  = settings.Cart[index];
                var receiver = new CartReceiver();
                var command  = new RemoveProductCommand(receiver, item);
                CartCommandHistory.GetInstance().ExecuteCommand(command);
                CartObserverManager.GetInstance().NotifyAll();
            }

            return RedirectToPage();
        }

        // Отмена последнего действия (Command — Undo)
        public IActionResult OnPostUndoCart()
        {
            var history = CartCommandHistory.GetInstance();
            if (!history.Undo())
                ShopSettings.GetInstance().AddLog("[Command] Нечего отменять — история пуста");

            CartObserverManager.GetInstance().NotifyAll();

            return RedirectToPage();
        }
    }
}