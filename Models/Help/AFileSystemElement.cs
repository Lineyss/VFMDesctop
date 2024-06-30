using System.Threading.Tasks;

namespace VFMDesctop.Models.Help
{
    internal abstract class AFileSystemElement<ReturnOpenMethodValue>
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool IsFile { get; set; }
        public double Size { get; set; }   

        public AFileSystemElement(string path)
        {
            Path = path;
            FileName = System.IO.Path.GetFileName(path);
            IsFile = false;
            Size = GetSize();
        }

        public abstract (AFileSystemElement<ReturnOpenMethodValue>, string) Get();
        public abstract (ReturnOpenMethodValue, string) Open();
        public abstract (bool, string) Delete();
        public abstract (bool, string) Update(string Name);
        public abstract (bool, string) Create();
        protected abstract double GetSize();
        protected bool IsExist() => System.IO.File.Exists(Path) || System.IO.Directory.Exists(Path);
        protected bool IsExist(string newPath) => System.IO.File.Exists(newPath) || System.IO.Directory.Exists(newPath);
    }
}
