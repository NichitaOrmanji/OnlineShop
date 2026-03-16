using System;

namespace OnlineShop
{
    // 1. Абстрактные продукты (Интерфейсы для категорий товаров)
    public interface IGadget { void GetInfo(); }
    public interface IClothing { void GetInfo(); }

    // 2. Абстрактная фабрика (Интерфейс для создания семейств)
    public interface IShopFactory
    {
        IGadget CreateGadget();
        IClothing CreateClothing();
    }

    // 3. Конкретная фабрика №1: "Премиум магазин"
    public class PremiumShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new iPhone();
        public IClothing CreateClothing() => new SilkShirt();
    }

    // 4. Конкретная фабрика №2: "Бюджетный магазин"
    public class BudgetShopFactory : IShopFactory
    {
        public IGadget CreateGadget() => new AndroidPhone();
        public IClothing CreateClothing() => new CottonTShirt();
    }

    // Конкретные продукты (реализации)
    public class iPhone : IGadget { public void GetInfo() => Console.WriteLine("Смартфон: iPhone 15"); }
    public class AndroidPhone : IGadget { public void GetInfo() => Console.WriteLine("Смартфон: Дешевый Android"); }
    public class SilkShirt : IClothing { public void GetInfo() => Console.WriteLine("Одежда: Шелковая рубашка"); }
    public class CottonTShirt : IClothing { public void GetInfo() => Console.WriteLine("Одежда: Хлопковая футболка"); }
}