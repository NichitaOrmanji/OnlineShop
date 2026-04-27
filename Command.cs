using System;
using System.Collections.Generic;

namespace OnlineShop
{
    // 1. Абстрактная команда (Command)
    // Объявляет интерфейс для выполнения и отмены операции
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }

    // 2. Получатель (Receiver)
    // Содержит реальную бизнес-логику — именно он умеет добавлять и удалять
    public class CartReceiver
    {
        private readonly ShopSettings _settings;

        public CartReceiver()
        {
            _settings = ShopSettings.GetInstance();
        }

        public void AddItem(string item)
        {
            _settings.Cart.Add(item);
            _settings.AddLog($"[Command→Receiver] Товар добавлен: {item}");
        }

        public void RemoveItem(string item)
        {
            _settings.Cart.Remove(item);
            _settings.AddLog($"[Command→Receiver] Товар удалён: {item}");
        }
    }

    // 3. Конкретная команда: Добавить товар (ConcreteCommand)
    public class AddProductCommand : ICommand
    {
        private readonly CartReceiver _receiver;
        private readonly string _item;

        public AddProductCommand(CartReceiver receiver, string item)
        {
            _receiver = receiver;
            _item = item;
        }

        public void Execute() => _receiver.AddItem(_item);

        // Undo — отменяем добавление, то есть удаляем товар
        public void Undo() => _receiver.RemoveItem(_item);

        public string GetDescription() => $"Добавить: {_item}";
    }

    // 4. Конкретная команда: Удалить товар (ConcreteCommand)
    public class RemoveProductCommand : ICommand
    {
        private readonly CartReceiver _receiver;
        private readonly string _item;

        public RemoveProductCommand(CartReceiver receiver, string item)
        {
            _receiver = receiver;
            _item = item;
        }

        public void Execute() => _receiver.RemoveItem(_item);

        // Undo — отменяем удаление, то есть возвращаем товар
        public void Undo() => _receiver.AddItem(_item);

        public string GetDescription() => $"Удалить: {_item}";
    }

    // 5. Invoker — хранит историю команд и умеет их отменять
    // Это ключевая часть паттерна Command
    public class CartCommandHistory
    {
        private static CartCommandHistory? _instance;
        private static readonly object _lock = new object();

        private readonly Stack<ICommand> _history = new Stack<ICommand>();

        private CartCommandHistory() { }

        public static CartCommandHistory GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CartCommandHistory();
                }
            }
            return _instance;
        }

        // Выполнить команду и запомнить её
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
            ShopSettings.GetInstance().AddLog($"[Command→Invoker] Выполнена команда: {command.GetDescription()}");
        }

        // Отменить последнюю команду
        public bool Undo()
        {
            if (_history.Count == 0) return false;

            var command = _history.Pop();
            command.Undo();
            ShopSettings.GetInstance().AddLog($"[Command→Invoker] Отменена команда: {command.GetDescription()}");
            return true;
        }

        public bool HasHistory() => _history.Count > 0;
        public int HistoryCount() => _history.Count;
    }
}