using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // Продукт - то что будем сортировать
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public Product(string name, double price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
        }
    }

    // 1. Абстрактная стратегия (Strategy)
    // Общий интерфейс для всех алгоритмов сортировки
    public interface ISortStrategy
    {
        List<Product> Sort(List<Product> products);
        string GetStrategyName();
    }

    // 2. Конкретные стратегии (ConcreteStrategy)

    // Сортировка по цене — от дешёвых к дорогим
    public class SortByPriceAsc : ISortStrategy
    {
        public List<Product> Sort(List<Product> products)
        {
            var sorted = new List<Product>(products);
            sorted.Sort((a, b) => a.Price.CompareTo(b.Price));
            return sorted;
        }
        public string GetStrategyName() => "Цена: по возрастанию";
    }

    // Сортировка по цене — от дорогих к дешёвым
    public class SortByPriceDesc : ISortStrategy
    {
        public List<Product> Sort(List<Product> products)
        {
            var sorted = new List<Product>(products);
            sorted.Sort((a, b) => b.Price.CompareTo(a.Price));
            return sorted;
        }
        public string GetStrategyName() => "Цена: по убыванию";
    }

    // Сортировка по названию А-Я
    public class SortByName : ISortStrategy
    {
        public List<Product> Sort(List<Product> products)
        {
            var sorted = new List<Product>(products);
            sorted.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            return sorted;
        }
        public string GetStrategyName() => "Название: А-Я";
    }

    // 3. Контекст (Context)
    // Хранит ссылку на стратегию и делегирует ей сортировку
    public class ProductCatalog
    {
        private ISortStrategy _strategy;
        private List<Product> _products;

        public ProductCatalog(ISortStrategy strategy)
        {
            _strategy = strategy;
            // Каталог товаров магазина
            _products = new List<Product>
            {
                new Product("iPhone 15 Pro",      1199, "Смартфоны"),
                new Product("MacBook Pro 14",      2500, "Ноутбуки"),
                new Product("Samsung Galaxy A54",   449, "Смартфоны"),
                new Product("Apple Watch Ultra 2",  799, "Аксессуары"),
                new Product("Acer Chromebook",      299, "Ноутбуки"),
                new Product("Наушники проводные",    15, "Аксессуары"),
            };
        }

        // Смена стратегии прямо во время работы — ключевая фишка паттерна
        public void SetStrategy(ISortStrategy strategy)
        {
            _strategy = strategy;
        }

        // Возвращает отсортированный список через текущую стратегию
        public List<Product> GetSortedProducts()
        {
            return _strategy.Sort(_products);
        }

        public string GetCurrentStrategyName() => _strategy.GetStrategyName();
    }
}