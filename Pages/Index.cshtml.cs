using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop; 

namespace OnlineShopProject_TMPPP.Pages 
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        // Список для вывода всей истории работы паттернов на страницу
        public List<string> ShopHistory { get; set; } = new List<string>();
        
        // Название магазина из Синглтона
        public string StoreName { get; set; } = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // 1. Singleton: Получаем настройки и название магазина
            var settings = ShopSettings.GetInstance();
            StoreName = settings.StoreName;
            
            // 2. Abstract Factory: Создаем товар и аксессуар
            IShopFactory factory = new PremiumShopFactory();
            var gadget = factory.CreateGadget();
            var accessory = factory.CreateAccessory();
            settings.AddToHistory($"[Factory] Выставлен товар: {gadget.GetDetails()}");
            settings.AddToHistory($"[Factory] Рекомендуем к покупке: {accessory.GetDetails()}");

            // 3. Builder: Собираем игровой ПК
            var manager = new ShopManager();
            var builder = new GamingComputerBuilder();
            manager.Construct(builder);
            var pc = builder.GetResult();
            settings.AddToHistory($"[Builder] Собрана конфигурация: {string.Join(" | ", pc.GetParts())}");

            // 4. Factory Method: Рассчитываем доставку
            DeliveryService deliveryService = new AirLogistics();
            settings.AddToHistory($"[Logistics] {deliveryService.GetFinalStatus()}");

            // 5. Prototype: Клонируем заказ для повторной покупки
            var originalOrder = new SimpleOrder("Игровой Монитор", 450) { CustomerName = "Никита" };
            var repeatOrder = (SimpleOrder)originalOrder.Clone();
            repeatOrder.CustomerName = "Повторный заказ (Никита)";
            settings.AddToHistory($"[Prototype] Оригинал: {originalOrder.GetSummary()}");
            settings.AddToHistory($"[Prototype] Клон: {repeatOrder.GetSummary()}");

            // Загружаем всю накопленную историю для отображения в браузере
            ShopHistory = settings.GetHistory();
        }
    }
}