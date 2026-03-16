using System;

namespace OnlineShop
{
    // 1. Продукт (Product) - интерфейс того, что создаем
    public interface IDelivery
    {
        // Теперь возвращает строку с описанием процесса для веб-интерфейса
        string GetDeliveryInfo();
    }

    // 2. Конкретные продукты (Concrete Products)
    public class TruckDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Тип: Наземная доставка. Транспорт: Грузовой автомобиль. Срок: 3-5 дней. Стоимость: $20";
    }

    public class AirDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Тип: Экспресс-доставка. Транспорт: Самолет. Срок: 1-2 дня. Стоимость: $100";
    }

    // Новая реализация для масштабирования (Морская доставка)
    public class SeaDelivery : IDelivery
    {
        public string GetDeliveryInfo() => 
            "Тип: Эконом-доставка. Транспорт: Контейнеровоз. Срок: 15-30 дней. Стоимость: $5";
    }

    // 3. Создатель (Creator) - объявляет фабричный метод
    public abstract class DeliveryService
    {
        // Фабричный метод (Factory Method)
        public abstract IDelivery CreateDelivery();

        // Бизнес-логика, использующая продукт
        public string GetFinalStatus()
        {
            var delivery = CreateDelivery();
            return $"Статус отправки: {delivery.GetDeliveryInfo()}";
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

    // Новый создатель для масштабирования
    public class SeaLogistics : DeliveryService
    {
        public override IDelivery CreateDelivery() => new SeaDelivery();
    }
}