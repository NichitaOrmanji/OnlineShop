using System;

namespace OnlineShop
{
    // 1. Прототип (Prototype) - задает интерфейс клонирования
    public abstract class OrderPrototype
    {
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        
        public abstract OrderPrototype Clone();
        public abstract string GetSummary(); // Для вывода на сайт
    }

    // 2. Конкретный прототип (ConcretePrototype)
    public class SimpleOrder : OrderPrototype
    {
        public string ProductName { get; set; }
        public double Price { get; set; }

        public SimpleOrder(string product, double price)
        {
            ProductName = product;
            Price = price;
            OrderDate = DateTime.Now;
        }

        // Реализация клонирования (Мелкое копирование)
        public override OrderPrototype Clone()
        {
            // MemberwiseClone копирует все значимые поля и ссылки
            var copy = (SimpleOrder)this.MemberwiseClone();
            copy.OrderDate = DateTime.Now; // Обновляем дату для нового (клонированного) заказа
            return copy;
        }

        public override string GetSummary() 
            => $"Заказ: {ProductName} | Цена: ${Price} | Клиент: {CustomerName} | Дата: {OrderDate.ToShortDateString()}";

        // Оставляем старый метод для совместимости с консолью
        public void ShowInfo() => Console.WriteLine(GetSummary());
    }
}