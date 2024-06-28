using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Models.Help
{
    internal abstract class AFileSystemElement : IFileSystemElement
    {
        protected readonly string path;
        public AFileSystemElement(string path)
        {
            this.path = path;
        }

        public abstract (AFileSystemElement, string) Get();
        public abstract (T, string) Open<T>();
        public abstract (bool, string) Delete();
        public abstract (bool, string) Update(string Name);
        public abstract (bool, string) Create();
        protected abstract double GetSize();
    }
}
