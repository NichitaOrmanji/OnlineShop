using System;

namespace OnlineShop
{
    // 1. Абстрактные продукты (Интерфейсы)
    // Согласно книге, каждый продукт семейства должен иметь свой интерфейс
    public interface IGadget { string GetDetails(); }
    public interface IClothing { string GetDetails(); }
    public interface IAccessory { string GetDetails(); }

    // 2. Абстрактная фабрика (Abstract Factory)
    // Объявляет интерфейс для операций, создающих абстрактные объекты-продукты
    public interface IShopFactory
    {
        IGadget CreateGadget();
        IClothing CreateClothing();
        IAccessory CreateAccessory(); 
    }

    // 3. Конкретная фабрика: Premium (Конкретизирует создание продуктов семейства)
    public class PremiumShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new iPhone();
        public IClothing CreateClothing() => new SilkShirt();
        public IAccessory CreateAccessory() => new AppleWatch();
    }

    // 4. Конкретная фабрика: Budget
    public class BudgetShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new AndroidPhone();
        public IClothing CreateClothing() => new CottonTShirt();
        public IAccessory CreateAccessory() => new WiredHeadphones();
    }

    // --- Реализации продуктов для Премиум сегмента ---

    public class iPhone : IGadget 
    { 
        public string GetDetails() => "Смартфон: iPhone 15 Pro, 256GB, Titanium Finish. Цена: $1199"; 
    }

    public class SilkShirt : IClothing 
    { 
        public string GetDetails() => "Одежда: Рубашка из натурального шелка, Premium Slim Fit. Цена: $150"; 
    }

    public class AppleWatch : IAccessory 
    { 
        public string GetDetails() => "Аксессуар: Apple Watch Ultra 2, Titanium Case, Ocean Band. Цена: $799"; 
    }

    // --- Реализации продуктов для Бюджетного сегмента ---

    public class AndroidPhone : IGadget 
    { 
        public string GetDetails() => "Смартфон: Samsung Galaxy A54, 128GB, Awesome Graphite. Цена: $449"; 
    }

    public class CottonTShirt : IClothing 
    { 
        public string GetDetails() => "Одежда: Футболка 100% Хлопок, Классический крой. Цена: $25"; 
    }

    public class WiredHeadphones : IAccessory 
    { 
        public string GetDetails() => "Аксессуар: Наушники-вкладыши с микрофоном (3.5мм). Цена: $15"; 
    }
}