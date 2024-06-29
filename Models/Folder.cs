using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Help;

namespace VFMDesctop.Models
{
    internal class Folder : AFileSystemElement
    {
        public Folder(string path) : base(path) { }

        public override (bool, string) Create()
        {
            if(IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                Directory.CreateDirectory(Path);
                return (true, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (Directory.Exists(Path)) return (false, "Ошибка: Такая директория уже существет");

            try
            {
                Directory.Delete(Path);
                return (false, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (AFileSystemElement, string) Get()
        {
            IsFile = false;
            return (this, "");
        }

        public override (T, string) Open<T>()
        {
            throw new NotImplementedException();
        }

        public override (bool, string) Update(string Name)
        {
            if (!Directory.Exists(Path)) return (false, "Ошибка: Такая директория не существует");

            string[] newDirectoryPathArray = Path.Split('/');
            newDirectoryPathArray[newDirectoryPathArray.Length - 1] = Name;
            string newDirectoryPath = string.Join("/", newDirectoryPathArray);

            if (IsExist(newDirectoryPath)) return (false, "Ошибка: Такая директория/папка уже существует");

            try
            {
                Directory.Move(Path, newDirectoryPath);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        protected override double GetSize()
        {
            string[] files = Directory.GetFiles(Path);
            string[] directories = Directory.GetDirectories(Path);

            double totalSize = 0;

            totalSize += GetSizeFolders(directories);
            totalSize +=  GetSizeFiles(files);

            return totalSize;
        }

        private double GetSizeFiles(string[] files)
        {
            double size = 0;

            foreach (string file in files)
            {
                size += new FileInfo(file).Length;
            }

            return size;
        }

        private double GetSizeFolders(string[] directories)
        {
            double size = 0;

            foreach (string directory in directories)
            {
                string[] files = Directory.GetFiles(directory);
                size += GetSizeFiles(files);

                string[] subDirectories = Directory.GetDirectories(directory);
                size += GetSizeFolders(subDirectories);
            }

            return size;
        }
    }
}
