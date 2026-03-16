using System;

namespace OnlineShop
{
    // 1. Продукт (Product) - интерфейс того, что создаем
    public interface IDelivery
    {
        void Deliver();
    }

    // 2. Конкретные продукты (Concrete Products)
    public class TruckDelivery : IDelivery
    {
        public void Deliver() => Console.WriteLine("Доставка наземным транспортом (Грузовик).");
    }

    public class AirDelivery : IDelivery
    {
        public void Deliver() => Console.WriteLine("Быстрая доставка по воздуху (Самолет).");
    }

    // 3. Создатель (Creator) - объявляет фабричный метод
    public abstract class DeliveryService
    {
        // Тот самый Фабричный Метод
        public abstract IDelivery CreateDelivery();

        public void SendOrder()
        {
            var delivery = CreateDelivery();
            delivery.Deliver();
        }
    }

    // 4. Конкретные создатели (Concrete Creators)
    public class GroundLogistics : DeliveryService
    {
        public override IDelivery CreateDelivery() => new TruckDelivery();
    }

    public class AirLogistics : DeliveryService
    {
        public override IDelivery CreateDelivery() => new AirDelivery();
    }
}