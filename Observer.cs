using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // 1. Абстрактный наблюдатель (Observer)
    // Интерфейс который должен реализовать каждый подписчик
    public interface ICartObserver
    {
        void Update(List<string> cart);
        string GetObserverName();
    }

    // 2. Субъект (Subject)
    // Хранит список наблюдателей и оповещает их при изменении
    public interface ICartSubject
    {
        void Attach(ICartObserver observer);
        void Detach(ICartObserver observer);
        void Notify();
    }

    // 3. Конкретный субъект (ConcreteSubject)
    // Корзина — при каждом изменении оповещает всех подписчиков
    public class ObservableCart : ICartSubject
    {
        private readonly List<ICartObserver> _observers = new List<ICartObserver>();
        private readonly ShopSettings _settings;

        public ObservableCart()
        {
            _settings = ShopSettings.GetInstance();
        }

        public void Attach(ICartObserver observer)
        {
            _observers.Add(observer);
            _settings.AddLog($"[Observer] Подписчик подключён: {observer.GetObserverName()}");
        }

        public void Detach(ICartObserver observer)
        {
            _observers.Remove(observer);
            _settings.AddLog($"[Observer] Подписчик отключён: {observer.GetObserverName()}");
        }

        // Оповещаем всех подписчиков — передаём им текущее состояние корзины
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_settings.Cart);
                _settings.AddLog($"[Observer] Уведомлён: {observer.GetObserverName()}");
            }
        }
    }

    // 4. Конкретные наблюдатели (ConcreteObserver)

    // Наблюдатель 1 — считает количество товаров в корзине
    public class CartCounterObserver : ICartObserver
    {
        public int ItemCount { get; private set; } = 0;

        public void Update(List<string> cart)
        {
            ItemCount = cart.Count;
        }

        public string GetObserverName() => "Счётчик товаров";
    }

    // Наблюдатель 2 — считает итоговую сумму корзины
    public class CartTotalObserver : ICartObserver
    {
        public double Total { get; private set; } = 0;

        public void Update(List<string> cart)
        {
            Total = 0;
            foreach (var item in cart)
            {
                // Извлекаем цену из строки формата "Название ($цена)"
                int start = item.LastIndexOf('$');
                int end = item.LastIndexOf(')');
                if (start >= 0 && end > start)
                {
                    string priceStr = item.Substring(start + 1, end - start - 1);
                    if (double.TryParse(priceStr,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double price))
                    {
                        Total += price;
                    }
                }
            }
        }

        public string GetObserverName() => "Итоговая сумма";
    }

    // Наблюдатель 3 — логирует каждое изменение корзины
    public class CartLoggerObserver : ICartObserver
    {
        public string LastMessage { get; private set; } = "";

        public void Update(List<string> cart)
        {
            LastMessage = cart.Count == 0
                ? "Корзина очищена"
                : $"Товаров в корзине: {cart.Count}. Последний: {cart[cart.Count - 1]}";
        }

        public string GetObserverName() => "Логгер корзины";
    }

    // 5. Менеджер наблюдателей — Singleton чтобы состояние жило между запросами
    public class CartObserverManager
    {
        private static CartObserverManager? _instance;
        private static readonly object _lock = new object();

        public ObservableCart Cart { get; } = new ObservableCart();
        public CartCounterObserver Counter { get; } = new CartCounterObserver();
        public CartTotalObserver Total { get; } = new CartTotalObserver();
        public CartLoggerObserver Logger { get; } = new CartLoggerObserver();

        private CartObserverManager()
        {
            // Подписываем всех наблюдателей сразу при создании
            Cart.Attach(Counter);
            Cart.Attach(Total);
            Cart.Attach(Logger);
        }

        public static CartObserverManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CartObserverManager();
                }
            }
            return _instance;
        }

        // Вызывается после каждого изменения корзины
        public void NotifyAll()
        {
            Cart.Notify();
        }
    }
}