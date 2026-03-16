using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // 1. Продукт (Product) - сложный объект, который мы собираем
    public class Computer
    {
        private List<string> _parts = new List<string>();
        public void Add(string part) => _parts.Add(part);
        public void Show() => Console.WriteLine("Комплектация: " + string.Join(", ", _parts));
    }

    // 2. Абстрактный Строитель (Builder) - задает интерфейс для создания частей
    public interface IComputerBuilder
    {
        void BuildCPU();
        void BuildRAM();
        void BuildStorage();
        Computer GetResult();
    }

    // 3. Конкретный Строитель (ConcreteBuilder) для игрового ПК
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();
        public void BuildCPU() => _computer.Add("Мощный CPU (Core i9)");
        public void BuildRAM() => _computer.Add("32GB RAM");
        public void BuildStorage() => _computer.Add("2TB NVMe SSD");
        public Computer GetResult() => _computer;
    }

    // 4. Распорядитель (Director) - управляет процессом сборки
    public class ShopManager
    {
        public void Construct(IComputerBuilder builder)
        {
            builder.BuildCPU();
            builder.BuildRAM();
            builder.BuildStorage();
        }
    }
}