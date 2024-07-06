using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Interfaces
{
    internal interface IFileSystemService
    {
        (ResponceFileSystemElement, string) Create(string path);
        (ResponceFileSystemElement, string) Delete(string path);
        (ResponceFileSystemElement, string) Open(string path);
        (ResponceFileSystemElement, string) Update(string path);
    }
}