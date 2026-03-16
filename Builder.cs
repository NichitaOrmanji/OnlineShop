using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // 1. Продукт (Product) - теперь хранит данные для веб-отображения
    public class Computer
    {
        private List<string> _parts = new List<string>();
        
        public void Add(string part) => _parts.Add(part);

        // Метод для получения списка комплектующих (нужен для сайта)
        public List<string> GetParts() => _parts;

        // Оставляем для тестов в консоли
        public void Show() => Console.WriteLine("Комплектация: " + string.Join(", ", _parts));
    }

    // 2. Абстрактный Строитель (Builder)
    public interface IComputerBuilder
    {
        void BuildCPU();
        void BuildRAM();
        void BuildStorage();
        void BuildGPU(); // Добавили видеокарту для масштабирования
        Computer GetResult();
    }

    // 3. Конкретный Строитель для игрового ПК (ConcreteBuilder)
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();
        
        public void BuildCPU() => _computer.Add("Процессор: Intel Core i9-14900K");
        public void BuildRAM() => _computer.Add("Оперативная память: 64GB DDR5 RGB");
        public void BuildStorage() => _computer.Add("Накопитель: 2TB Samsung 990 Pro");
        public void BuildGPU() => _computer.Add("Видеокарта: NVIDIA RTX 4090 24GB");
        
        public Computer GetResult() => _computer;
    }

    // 4. Добавили новый Конкретный Строитель для Офисного ПК (Масштабирование)
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public void BuildCPU() => _computer.Add("Процессор: Intel Core i3-12100");
        public void BuildRAM() => _computer.Add("Оперативная память: 8GB DDR4");
        public void BuildStorage() => _computer.Add("Накопитель: 256GB SSD");
        public void BuildGPU() => _computer.Add("Видеокарта: Встроенная Intel UHD Graphics");

        public Computer GetResult() => _computer;
    }

    // 5. Распорядитель (Director)
    public class ShopManager
    {
        // Директор строго следует алгоритму сборки (алгоритм один для всех)
        public void Construct(IComputerBuilder builder)
        {
            builder.BuildCPU();
            builder.BuildRAM();
            builder.BuildStorage();
            builder.BuildGPU();
        }
    }
}