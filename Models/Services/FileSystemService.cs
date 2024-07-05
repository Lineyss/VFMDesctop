using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Services
{
    internal class FileSystemService
    {
        private readonly IFileSystemElement fileSystemElement;
        public FileSystemService(string path) => fileSystemElement = getFilSystemElement(path);

        public (ResponceFileSystemElement, string) Open() => fileSystemElement.Open();
        public (ResponceFileSystemElement, string) Delete() => fileSystemElement.Open();
        public (ResponceFileSystemElement, string) Update() => fileSystemElement.Open();
        public (ResponceFileSystemElement, string) Create() => fileSystemElement.Open();

        public static IFileSystemElement getFilSystemElement(string path)
        {
            if (System.IO.File.Exists(path)) return new File(path);
            else if(System.IO.Directory.Exists(path)) return new Folder(path);
            return new Drive(path);
        }
    }
}
