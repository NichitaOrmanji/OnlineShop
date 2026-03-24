using System;

namespace OnlineShop
{
    // 1. Продукт (Product) - интерфейс услуги доставки
    public interface IDelivery
    {
        string GetDeliveryInfo();
    }

    // 2. Конкретные продукты (Concrete Products)
    public class TruckDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Наземная доставка: Защищенный фургон. Страховка электроники включена. Срок: 3-5 дней. Цена: $20";
    }

    public class AirDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Экспресс-авиа: Бережная перевозка хрупких гаджетов. Срок: 1 день. Цена: $100";
    }

    public class SeaDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Морской фрахт: Контейнерная перевозка оптовых партий. Срок: 20-30 дней. Цена: $5";
    }

    // 3. Создатель (Creator) - объявляет фабричный метод
    public abstract class DeliveryService
    {
        // Тот самый Factory Method
        public abstract IDelivery CreateDelivery();

        // Основная логика, которая не зависит от типа транспорта
        public string GetFinalStatus()
        {
            var delivery = CreateDelivery();
            return $"[Логистика TechStore] {delivery.GetDeliveryInfo()}";
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

    public class SeaLogistics : DeliveryService
    {
        public override IDelivery CreateDelivery() => new SeaDelivery();
    }
}