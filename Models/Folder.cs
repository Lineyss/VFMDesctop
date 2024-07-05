using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using VFMDesctop.Models.AbsctactModels;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models
{
    internal class Folder : AFileSystemElement
    {
        public Folder(string path) : base(path) { }

        public override (ResponceFileSystemElement, string) Create()
        {
            if(IsExist()) return (null, "Ошибка: Такой файл/директория уже существует");

            try
            {
                DirectoryInfo directory = Directory.CreateDirectory(_Path);
                return (new ResponceFileSystemElement
                {
                    Name = directory.Name,
                    Path = directory.FullName,
                    Size = GetSize(),
                } , "");
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (Directory.Exists(_Path)) return (false, "Ошибка: Такая директория уже существет");

            try
            {
                Directory.Delete(_Path);
                return (true, "");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (ResponceFileSystemElement, string) Open()
        {
            try
            {
                if (!Directory.Exists(_Path)) return (null, "Ошибка: Такая директория не существует");

                ResponceFileSystemElement[] files = Directory.GetFiles(_Path)
                    .Select(file => new ResponceFileSystemElement
                    {
                        Path = file,
                        Name = System.IO.Path.GetFileName(file),
                        Size = new FileInfo(file).Length,
                        Type = nameof(File)
                    }).ToArray();

                ResponceFileSystemElement[] directories = Directory.GetDirectories(_Path)
                    .Select(directory => new ResponceFileSystemElement
                    {
                        Path = directory,
                        Name = System.IO.Path.GetDirectoryName(directory),
                        Size = GetSize(directory),
                        Type = nameof(Folder)
                    }).ToArray();

                return (new ResponceFileSystemElement
                {
                    Name = new DirectoryInfo(_Path).Name,
                    Path = _Path,
                    Type = nameof(Folder),
                    Size = GetSize(),
                    Data = files.Concat(directories)
                }, "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (ResponceFileSystemElement, string) Update(string Name)
        {
            if (!Directory.Exists(_Path)) return (null, "Ошибка: Такая директория не существует");

            string[] newDirectoryPathArray = _Path.Split('/');
            newDirectoryPathArray[newDirectoryPathArray.Length - 1] = Name;
            string newDirectoryPath = string.Join("/", newDirectoryPathArray);

            if (IsExist(newDirectoryPath)) return (null, "Ошибка: Такая директория/папка уже существует");

            try
            {
                Directory.Move(_Path, newDirectoryPath);
                return (new ResponceFileSystemElement
                {
                    Name = new DirectoryInfo(newDirectoryPath).Name,
                    Path = newDirectoryPath,
                    Size = GetSize(newDirectoryPath),
                    Type = nameof(Folder)
                }, "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        protected override double GetSize() => GetSize(_Path);

        public static double GetSize(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            double totalSize = 0;

            totalSize += GetSizeFolders(directories);
            totalSize += GetSizeFiles(files);

            return totalSize;
        }

        protected static double GetSizeFiles(string[] files)
        {
            double size = 0;

            foreach (string file in files)
            {
                size += new FileInfo(file).Length;
            }

            return size;
        }

        private static double GetSizeFolders(string[] directories)
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
