using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.AbsctactModels
{
    internal abstract class AFileSystemElement : IFileSystemElement
    {
        protected readonly string _Path;
        public AFileSystemElement(string Path)
        {
            _Path = Path;
        }

        public static AFileSystemElement GetFileSystemElemen()
        {
            return null;
        }

        public abstract (ResponceFileSystemElement, string) Create();
        public abstract (ResponceFileSystemElement, string) Update(string Name);
        public abstract (bool, string) Delete();
        public abstract (ResponceFileSystemElement, string) Open();
        protected abstract double GetSize();
        protected bool IsExist() => System.IO.File.Exists(_Path) || System.IO.Directory.Exists(_Path);
        protected static bool IsExist(string path) => System.IO.File.Exists(path) || System.IO.Directory.Exists(path);
    }
}
