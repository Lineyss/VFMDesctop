using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VFMDesctop.Models.Help;

namespace VFMDesctop.Models
{
    internal class File : AFileSystemElement<object>
    {
        public File(string path) : base(path)
        {
        }

        public override (bool, string) Create()
        {
            if (IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                System.IO.File.Create(this.Path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (!System.IO.File.Exists(this.Path)) return (false, "Ошибка: Такого файла не существует");

            try
            {
                System.IO.File.Delete(this.Path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (AFileSystemElement<object>, string) Get() => (this, "");

        public override (object, string) Open()
        {
            if (!System.IO.File.Exists(this.Path)) return (null, "Ошибка: Такого файла не существует");
            throw new NotImplementedException();
        }

        public override (bool, string) Update(string Name)
        {
            if (!System.IO.File.Exists(this.Path)) return (false, "Ошибка: Такого файла не существует");

            string[] newFilePathArray = this.Path.Split('/');
            newFilePathArray[newFilePathArray.Length - 1] = Name;
            string newFilePath = string.Join("/", newFilePathArray);

            if (!IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                System.IO.File.Move(this.Path, newFilePath);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        protected override double GetSize()
        {
            return new FileInfo(this.Path).Length;
        }
    }
}
