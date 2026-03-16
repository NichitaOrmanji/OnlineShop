using System;

namespace OnlineShop
{
    // 1. Прототип (Prototype)
    public abstract class OrderPrototype
    {
        
        public required string CustomerName { get; set; }
        
        public abstract OrderPrototype Clone();
        public abstract void ShowInfo();
    }

    // 2. Конкретный прототип (ConcretePrototype)
    public class SimpleOrder : OrderPrototype
    {
        public string ProductName { get; set; }

        public SimpleOrder(string product) => ProductName = product;

        public override OrderPrototype Clone()
        {
            Console.WriteLine($"--- Клонирование заказа на {ProductName} ---");
            // MemberwiseClone копирует все поля, включая CustomerName
            return (OrderPrototype)this.MemberwiseClone();
        }

        public override void ShowInfo() 
            => Console.WriteLine($"Заказ: {ProductName} для клиента: {CustomerName}");
    }
}