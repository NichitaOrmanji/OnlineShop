using System;

namespace OnlineShop
{
    // 1. Абстрактные продукты (Интерфейсы)
    // Каждый интерфейс представляет отдельный тип товара в магазине техники
    public interface IGadget { string GetDetails(); }
    public interface ILaptop { string GetDetails(); }
    public interface IAccessory { string GetDetails(); }

    // 2. Абстрактная фабрика (Abstract Factory)
    // Создает целое семейство связанных товаров (например, только Apple или только Android/Windows)
    public interface IShopFactory
    {
        IGadget CreateGadget();
        ILaptop CreateLaptop();
        IAccessory CreateAccessory(); 
    }

    // 3. Конкретная фабрика: Premium (Семейство Apple/High-end)
    public class PremiumShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new iPhone();
        public ILaptop CreateLaptop() => new MacBook();
        public IAccessory CreateAccessory() => new AppleWatch();
    }

    // 4. Конкретная фабрика: Budget (Семейство доступной техники)
    public class BudgetShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new AndroidPhone();
        public ILaptop CreateLaptop() => new Chromebook();
        public IAccessory CreateAccessory() => new WiredHeadphones();
    }

    // --- Реализации продуктов для Премиум сегмента ---

    public class iPhone : IGadget 
    { 
        public string GetDetails() => "Смартфон: iPhone 15 Pro, 256GB, Titanium. Цена: $1199"; 
    }

    public class MacBook : ILaptop 
    { 
        public string GetDetails() => "Ноутбук: MacBook Pro 14, M3 Max, 32GB RAM. Цена: $3200"; 
    }

    public class AppleWatch : IAccessory 
    { 
        public string GetDetails() => "Аксессуар: Apple Watch Ultra 2, Titanium Case. Цена: $799"; 
    }

    // --- Реализации продуктов для Бюджетного сегмента ---

    public class AndroidPhone : IGadget 
    { 
        public string GetDetails() => "Смартфон: Samsung Galaxy A54, 128GB. Цена: $449"; 
    }

    public class Chromebook : ILaptop 
    { 
        public string GetDetails() => "Ноутбук: Acer Chromebook, 4GB RAM, 64GB eMMC. Цена: $299"; 
    }

    public class WiredHeadphones : IAccessory 
    { 
        public string GetDetails() => "Аксессуар: Наушники-вкладыши проводные (3.5мм). Цена: $15"; 
    }
}