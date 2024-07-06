using System;
using VFMDesctop.Models.Interfaces;

namespace VFMDesctop.Models.AbsctactModels
{
    internal class AFactory<T> : IFactory<T>
    {
        // Метод для создания какого-то объекта
        private readonly Func<T> factory;

        // Класс для получения метода по созданию объекта
        public AFactory(Func<T> factory)
        {
            this.factory = factory;
        }

        // Метод который вызывает метод по созданию объекта
        public T Create() => factory();

    }
}
