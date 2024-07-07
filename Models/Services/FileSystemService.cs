using System.Collections.Generic;
using System.IO;
using System.Linq;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.Repository;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Services
{
    internal class FileSystemService : IFileSystemService
    {
        private readonly IEnumerable<IFileSystemRepository> fileSystemRepositories;

        private IFileSystemRepository fileSystemRepository;

        public FileSystemService(IEnumerable<IFileSystemRepository> fileSystemRepositories)
        {
            this.fileSystemRepositories = fileSystemRepositories;
        }

        public (ResponceFileSystemElement, string) Open(string path)
        {
            GetFileSystemRepository(path);

            return fileSystemRepository.Open(path);
        }
        public (ResponceFileSystemElement, string) Delete(string path)
        {
            GetFileSystemRepository(path);

            (bool isDeleted, string error) = fileSystemRepository.Delete(path);

            return isDeleted ? (new ResponceFileSystemElement(), string.Empty) : (null, error);
        }
        public (ResponceFileSystemElement, string) Update(string name, string path)
        {
            GetFileSystemRepository(path);

            return fileSystemRepository.Update(name, path);
        }
        public (ResponceFileSystemElement, string) Create(string path)
        {
            GetFileSystemRepository(path);

            return fileSystemRepository.Create(path);
        }

        protected void GetFileSystemRepository(string path)
            => fileSystemRepository = File.Exists(path) ?
                fileSystemRepositories.First(repository => repository.GetType() == typeof(FileRepository)) :
                fileSystemRepositories.First(repository => repository.GetType() == typeof(FolderRepository));
    }
}
