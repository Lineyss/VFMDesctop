using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Services
{
    internal class FileSystemService : IFileSystemService
    {
        private IFileSystemElement FileSystemElement { get; set; }

        public (ResponceFileSystemElement, string) Open(string path)
        {
            FileSystemElement = getFilSystemElement(path);
            return FileSystemElement.Open();
        }
        public (ResponceFileSystemElement, string) Delete(string path)
        {
            FileSystemElement = getFilSystemElement(path);
            return FileSystemElement.Open();
        }
        public (ResponceFileSystemElement, string) Update(string path)
        {
            FileSystemElement = getFilSystemElement(path);
            return FileSystemElement.Open();
        }
        public (ResponceFileSystemElement, string) Create(string path)
        {
            FileSystemElement = getFilSystemElement(path);
            return FileSystemElement.Open();
        }

        protected IFileSystemElement getFilSystemElement(string path)
        {
            if (System.IO.File.Exists(path)) return new File(path);
            else if (System.IO.Directory.Exists(path)) return new Folder(path);
            return new Drive(path);
        }
    }
}
