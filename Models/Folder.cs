using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Help;

namespace VFMDesctop.Models
{
    internal class Folder : AFileSystemElement<string[]>
    {
        public Folder(string path) : base(path) 
        {
            IsFile = false;
        }

        public override (bool, string) Create()
        {
            if(IsExist()) return (false, "Ошибка: Такой файл/директория уже существует");

            try
            {
                Directory.CreateDirectory(this.Path);
                return (true, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (Directory.Exists(this.Path)) return (false, "Ошибка: Такая директория уже существет");

            try
            {
                Directory.Delete(this.Path);
                return (false, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (AFileSystemElement<string[]>, string) Get() => (this, "");

        public override (string[], string) Open()
        {
            try
            {
                if (!Directory.Exists(this.Path)) return (null, "Ошибка: Такая директория не существует");

                string[] files = Directory.GetFiles(this.Path);
                string[] directories = Directory.GetDirectories(this.Path);

                return (files.Concat(directories).ToArray() , "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (bool, string) Update(string Name)
        {
            if (!Directory.Exists(this.Path)) return (false, "Ошибка: Такая директория не существует");

            string[] newDirectoryPathArray = this.Path.Split('/');
            newDirectoryPathArray[newDirectoryPathArray.Length - 1] = Name;
            string newDirectoryPath = string.Join("/", newDirectoryPathArray);

            if (IsExist(newDirectoryPath)) return (false, "Ошибка: Такая директория/папка уже существует");

            try
            {
                Directory.Move(this.Path, newDirectoryPath);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        protected override double GetSize()
        {
            string[] files = Directory.GetFiles(this.Path);
            string[] directories = Directory.GetDirectories(this.Path);

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
