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
    }
}