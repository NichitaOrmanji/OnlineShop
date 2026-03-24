using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // 1. Продукт (Product) - сложный объект, который мы собираем по частям
    public class Computer
    {
        private List<string> _parts = new List<string>();
        
        public void Add(string part) => _parts.Add(part);

        // Используем этот метод для вывода списка в веб-интерфейс
        public List<string> GetParts() => _parts;

        public void Show() => Console.WriteLine("Конфигурация системы: " + string.Join(", ", _parts));
    }

    // 2. Абстрактный Строитель (Builder)
    public interface IComputerBuilder
    {
        void BuildCPU();
        void BuildRAM();
        void BuildStorage();
        void BuildGPU();
        Computer GetResult();
    }

    // 3. Конкретный Строитель для Игрового ПК (High-End сегмент)
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();
        
        public void BuildCPU() => _computer.Add("Процессор: Intel Core i9-14900K (6.0 GHz)");
        public void BuildRAM() => _computer.Add("ОЗУ: 64GB DDR5 Fury RGB");
        public void BuildStorage() => _computer.Add("SSD: 2TB NVMe Gen5");
        public void BuildGPU() => _computer.Add("Видеокарта: NVIDIA RTX 4090 24GB");
        
        public Computer GetResult() => _computer;
    }

    // 4. Конкретный Строитель для Офисного ПК (Бюджетный сегмент)
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public void BuildCPU() => _computer.Add("Процессор: Intel Core i3-12100");
        public void BuildRAM() => _computer.Add("ОЗУ: 8GB DDR4 Kingston");
        public void BuildStorage() => _computer.Add("SSD: 256GB SATA");
        public void BuildGPU() => _computer.Add("Видеокарта: Интегрированная Intel UHD");

        public Computer GetResult() => _computer;
    }

    // 5. Распорядитель (Director) - управляет процессом сборки
    public class ShopManager
    {
        // Алгоритм сборки всегда одинаков, но результат зависит от выбранного строителя
        public void Construct(IComputerBuilder builder)
        {
            builder.BuildCPU();
            builder.BuildRAM();
            builder.BuildStorage();
            builder.BuildGPU();
        }
    }
}