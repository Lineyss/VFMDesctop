using System;
using System.IO;
using System.Linq;
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
            if (System.IO.File.Exists(path)) return (false, "Ошибка: Такой файл уже существует");
            try
            {
                System.IO.File.Create(path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (!System.IO.File.Exists(path)) return (false, "Ошибка: Такого файла не существует");

            try
            {
                System.IO.File.Delete(path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (AFileSystemElement, string) Get()
        {
            throw new NotImplementedException();
        }

        public override (T, string) Open<T>()
        {
            throw new NotImplementedException();
        }

        public override (bool, string) Update(string Name)
        {
            if (!System.IO.File.Exists(path)) return (false, "Ошибка: Такого файла не существует");

            string[] newFilePathArray = path.Split('/');
            newFilePathArray[newFilePathArray.Length - 1] = Name;
            string newFilePath = string.Join("/", newFilePathArray);

            if (!System.IO.File.Exists(newFilePath)) return (false, "Ошибка: Такой файл уже существует");

            try
            {
                System.IO.File.Copy(path, newFilePath);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        protected override double GetSize()
        {
            return new FileInfo(path).Length;
        }
    }
}
