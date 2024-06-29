using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VFMDesctop.Models.Help;

namespace VFMDesctop.Models
{
    internal class File : AFileSystemElement
    {
        public File(string path) : base(path)
        {
        }

        public override (bool, string) Create()
        {
            if (IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                System.IO.File.Create(Path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (!System.IO.File.Exists(Path)) return (false, "Ошибка: Такого файла не существует");

            try
            {
                System.IO.File.Delete(Path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (AFileSystemElement, string) Get() => (this, "");

        public override (T, string) Open<T>()
        {
            throw new NotImplementedException();
        }

        public override (bool, string) Update(string Name)
        {
            if (!System.IO.File.Exists(Path)) return (false, "Ошибка: Такого файла не существует");

            string[] newFilePathArray = Path.Split('/');
            newFilePathArray[newFilePathArray.Length - 1] = Name;
            string newFilePath = string.Join("/", newFilePathArray);

            if (!IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                System.IO.File.Move(Path, newFilePath);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        protected override double GetSize()
        {
            return new FileInfo(Path).Length;
        }
    }
}
