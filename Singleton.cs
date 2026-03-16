using System;
using System.Collections.Generic;

namespace OnlineShop
{
    public class ShopSettings
    {
        // 1. Поле для хранения единственного экземпляра
        private static ShopSettings? _instance;
        
        // Объект-заглушка для потокобезопасности (актуально для Веб-сайта)
        private static readonly object _lock = new object();

        // 2. Данные магазина
        public string Currency { get; set; } = "USD";
        public string StoreName { get; set; } = "Pattern Tech Store";
        public DateTime LastUpdate { get; private set; }

        private List<string> _orderHistory = new List<string>();

        // 3. Приватный конструктор (согласно книге)
        private ShopSettings() 
        {
            LastUpdate = DateTime.Now;
        }

        // 4. Глобальная точка доступа (Thread-Safe Singleton)
        public static ShopSettings GetInstance()
        {
            // Используем double-check locking для надежности в веб-среде
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ShopSettings();
                    }
                }
            }
            return _instance;
        }

        // Методы работы с историей
        public void AddToHistory(string entry) 
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            _orderHistory.Add($"[{timestamp}] {entry}");
        }

        public List<string> GetHistory() => _orderHistory;

        public void ChangeCurrency(string newCurrency)
        {
            Currency = newCurrency;
            AddToHistory($"Валюта магазина изменена на {newCurrency}");
        }
    }
}