namespace VFMDesctop.Models.Help
{
    internal interface IFileSystemElement
    {
        (bool, string) Create();
        (bool, string) Delete();
        (AFileSystemElement, string) Get();
        (T, string) Open<T>();
        (bool, string) Update(string Name);
    }
}