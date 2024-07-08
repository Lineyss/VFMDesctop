using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Interfaces
{
    public interface IFileSystemRepository
    {
        (ResponceFileSystemElement FileSystemElement, string Error) Create(string path);
        (bool IsDeleted, string Error) Delete(string path);
        (ResponceFileSystemElement FileSystemElement, string Error) Open(string path);
        (ResponceFileSystemElement FileSystemElement, string Error) Update(string Name, string path);
    }
}