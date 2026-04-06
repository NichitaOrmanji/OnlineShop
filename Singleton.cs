using System;
using System.Collections.Generic;

namespace OnlineShop
{
    public class ShopSettings
    {
        private static ShopSettings? _instance;
        private static readonly object _lock = new object();

        // Данные магазина
        public string Currency { get; set; } = "USD";
        public string StoreName { get; set; } = "TechPoint Pro Store";
        public DateTime SystemStartTime { get; private set; }

        // --- НОВЫЕ ПОЛЯ ДЛЯ ФУНКЦИОНАЛА ---
        public List<string> Cart { get; set; } = new List<string>(); // Текущая корзина
        public List<SimpleOrder> CompletedOrders { get; set; } = new List<SimpleOrder>(); // История реальных заказов
        private List<string> _systemLog = new List<string>(); // Технический лог для админки

        private ShopSettings() 
        {
            SystemStartTime = DateTime.Now;
            AddLog("Система TechPoint инициализирована.");
        }

        public static ShopSettings GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new ShopSettings();
                }
            }
            return _instance;
        }

        // --- НОВЫЙ МЕТОД: Удаление из корзины ---
        public void RemoveFromCart(int index)
        {
            if (index >= 0 && index < Cart.Count)
            {
                string removedItem = Cart[index];
                Cart.RemoveAt(index);
                AddLog($"Товар удален из корзины: {removedItem}");
            }
        }

        // Метод для записи действий (заменяет старый AddToHistory)
        public void AddLog(string entry) 
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            _systemLog.Add($"[{timestamp}] {entry}");
        }

        public List<string> GetLogs() => _systemLog;

        // Для обратной совместимости, если где-то остался вызов GetHistory
        public List<string> GetHistory() => _systemLog;
    }
}