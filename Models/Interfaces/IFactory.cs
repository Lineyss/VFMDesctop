namespace VFMDesctop.Models.Interfaces
{
    public interface IFactory<T>
    {
        T Create();
    }
}