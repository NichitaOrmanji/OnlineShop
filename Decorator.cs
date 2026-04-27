using System;

namespace OnlineShop
{
    // 1. Общий интерфейс для всех товаров
    public interface IShoppable
    {
        string GetDescription();
        double GetPrice();
    }

    // 2. Базовый товар
    public class SimpleProduct : IShoppable
    {
        private string _name;
        private double _price;

        public SimpleProduct(string name, double price)
        {
            _name = name;
            _price = price;
        }

        public string GetDescription() => _name;
        public double GetPrice() => _price;
    }

    // 3. Базовый декоратор
    public abstract class ProductDecorator : IShoppable
    {
        protected IShoppable _innerItem;

        public ProductDecorator(IShoppable item)
        {
            _innerItem = item;
        }

        public virtual string GetDescription() => _innerItem.GetDescription();
        public virtual double GetPrice() => _innerItem.GetPrice();
    }

    // 4. Конкретный декоратор №1: Подарочная упаковка
    public class GiftWrapDecorator : ProductDecorator
    {
        public GiftWrapDecorator(IShoppable item) : base(item) { }

        public override string GetDescription() => base.GetDescription() + " (в подарочной упаковке)";
        public override double GetPrice() => base.GetPrice() + 10.0;
    }

    // 5. Конкретный декоратор №2: Дополнительная гарантия
    public class ExtraWarrantyDecorator : ProductDecorator
    {
        public ExtraWarrantyDecorator(IShoppable item) : base(item) { }

        public override string GetDescription() => base.GetDescription() + " (+ расширенная гарантия)";
        public override double GetPrice() => base.GetPrice() + 50.0;
    }
}