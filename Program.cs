using System;
using OnlineShop;

class Program
{
    static void Main()
    {
        // Выбираем тип магазина (фабрику)
        IShopFactory factory = new PremiumShopFactory(); 

        // Создаем товары этой фабрики
        var gadget = factory.CreateGadget();
        var cloth = factory.CreateClothing();

        Console.WriteLine("--- Ваш заказ в магазине ---");
        gadget.GetInfo();
        cloth.GetInfo();

        // Паттерн 2: Builder
        Console.WriteLine("\n--- Сборка кастомного ПК (Builder) ---");
        
        ShopManager manager = new ShopManager();
        IComputerBuilder gamingBuilder = new GamingComputerBuilder();

        // Распорядитель (Manager) руководит процессом сборки
        manager.Construct(gamingBuilder);
        
        // Получаем готовый продукт
        Computer myPC = gamingBuilder.GetResult();
        myPC.Show();
    }
}