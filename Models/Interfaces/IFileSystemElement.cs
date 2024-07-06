using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Interfaces
{
    internal interface IFileSystemElement<FileSystemElement>
    {
        (FileSystemElement FileSystemElement, string Error) Create(string path);
        (bool IsDeleted, string Error) Delete(string path);
        (FileSystemElement FileSystemElement, string Error) Open(string path);
        (FileSystemElement FileSystemElement, string Error) Update(string Name, string path);
    }
}