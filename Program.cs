using System;
using OnlineShop;

class Program
{
    static void Main()
    {
        // --- 1. Abstract Factory ---
        Console.WriteLine("--- Магазин (Abstract Factory) ---");
        IShopFactory factory = new PremiumShopFactory();
        var gadget = factory.CreateGadget();
        gadget.GetInfo();

        // --- 2. Builder ---
        Console.WriteLine("\n--- Сборка ПК (Builder) ---");
        ShopManager manager = new ShopManager();
        IComputerBuilder gamingBuilder = new GamingComputerBuilder();
        manager.Construct(gamingBuilder);
        Computer myPC = gamingBuilder.GetResult();
        myPC.Show();

        // --- 3. Factory Method ---
        Console.WriteLine("\n--- Доставка (Factory Method) ---");
        DeliveryService deliveryService = new AirLogistics();
        deliveryService.SendOrder();

        // --- 4. Prototype ---
        Console.WriteLine("\n--- Копирование заказа (Prototype) ---");
        
        // ВАЖНО: Используем инициализатор { CustomerName = ... }, так как поле required
        SimpleOrder originalOrder = new SimpleOrder("Ноутбук") 
        { 
            CustomerName = "Иван" 
        };

        // Клонируем
        SimpleOrder clonedOrder = (SimpleOrder)originalOrder.Clone();
        clonedOrder.CustomerName = "Мария"; // Меняем имя в клоне

        originalOrder.ShowInfo();
        clonedOrder.ShowInfo();

        // --- 5. Singleton ---
        Console.WriteLine("\n--- Настройки (Singleton) ---");
        ShopSettings settings = ShopSettings.GetInstance();
        Console.WriteLine($"Текущая валюта: {settings.Currency}");
        
        ShopSettings secondSettings = ShopSettings.GetInstance();
        secondSettings.Currency = "EUR";
        Console.WriteLine($"Валюта после изменения: {settings.Currency}");
    }
}