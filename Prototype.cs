using System;

namespace OnlineShop
{
    // 1. Прототип (Prototype) - интерфейс клонирования
    public abstract class OrderPrototype
    {
        public string CustomerName { get; set; } = "Guest";
        public DateTime OrderDate { get; set; }
        
        // Тот самый метод Clone
        public abstract OrderPrototype Clone();
        public abstract string GetSummary(); 
    }

    // 2. Конкретный прототип (Заказ на электронику)
    public class SimpleOrder : OrderPrototype
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string OrderStatus { get; set; } = "Original";

        public SimpleOrder(string product, double price)
        {
            ProductName = product;
            Price = price;
            OrderDate = DateTime.Now;
        }

        // Реализация клонирования (Мелкое копирование)
        public override OrderPrototype Clone()
        {
            // Используем стандартный метод .NET для поверхностного копирования
            var copy = (SimpleOrder)this.MemberwiseClone();
            
            // Настраиваем данные для клона (имитация нового заказа на базе старого)
            copy.OrderDate = DateTime.Now; 
            copy.OrderStatus = "Cloned/Repeat"; 
            
            return copy;
        }

        public override string GetSummary() 
            => $"[{OrderStatus}] Товар: {ProductName} | Цена: ${Price} | Клиент: {CustomerName} | Дата: {OrderDate.ToShortDateString()}";

        public void ShowInfo() => Console.WriteLine(GetSummary());
    }
}