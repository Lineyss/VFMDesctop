using System;
using Microsoft.Extensions.DependencyInjection;
using VFMDesctop.Models.AbsctactModels;
using VFMDesctop.Models.Interfaces;

namespace VFMDesctop.Models.Services
{
    // Класс для расширения IServiceCollection
    // Будет работать похоже с Transisent или Singlenton
    public static class ServiceExtensions
    {
        public static void AddFormFactory<TForm>(this IServiceCollection services) 
            where TForm : class
        {
            // Добавляем модель в служду зависимостей через Transient
            // Чтобы при обращении к модели создавался новый экземпляр
            services.AddTransient<TForm>();
            // Регестрирует делегат который при вызову будет возвращать экземпляр TForm
            services.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>());
            // Регестрируем саму фабрику в качеству службы чтобы можно было обращаться к фабрике
            services.AddSingleton<IFactory<TForm>, AFactory<TForm>>();
        }
    }
}
