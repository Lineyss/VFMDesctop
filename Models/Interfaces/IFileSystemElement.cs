using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Interfaces
{
    internal interface IFileSystemElement
    {
        (ResponceFileSystemElement, string) Create();
        (bool, string) Delete();
        (ResponceFileSystemElement, string) Open();
        (ResponceFileSystemElement, string) Update(string Name);
    }
}