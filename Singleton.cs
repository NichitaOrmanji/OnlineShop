using System;

namespace OnlineShop
{
    public class ShopSettings
    {
        // Статическое поле для хранения единственного экземпляра
        private static ShopSettings? _instance;

        public string Currency { get; set; } = "USD";

        // Приватный конструктор - никто не может вызвать 'new ShopSettings()' снаружи
        private ShopSettings() { }

        // Публичный метод для доступа к экземпляру
        public static ShopSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShopSettings();
            }
            return _instance!;
        }
    }
}