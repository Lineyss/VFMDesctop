using System;
using System.IO;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Repository
{
    internal class FolderRepository : IFileSystemRepository
    {
        public (ResponceFileSystemElement FileSystemElement, string Error) Create(string path)
        {
            if (!IsExist(path) && !File.Exists(path))
            {
                try
                {
                    return (ConvertToResponceFileSystemElement(Directory.CreateDirectory(path)), string.Empty);
                }
                catch(Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Директория/файл уже существует");
        }

        public (bool IsDeleted, string Error) Delete(string path)
        {
            if(IsExist(path))
            {
                try
                {
                    Directory.Delete(path);
                    return (true, string.Empty);
                }
                catch(Exception e)
                {
                    return (false, e.Message);
                }
            }

            return (false, "Ошибка: Такой директории не существует");
        }

        public (ResponceFileSystemElement FileSystemElement, string Error) Open(string path)
        {
            if(IsExist(path))
            {
                try
                {
                    string[] filesAndDirectoryes = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                    return (ConvertToResponceFileSystemElement(new DirectoryInfo(path), filesAndDirectoryes), string.Empty);
                }
                catch (Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой директории не существует");
        }

        public (ResponceFileSystemElement FileSystemElement, string Error) Update(string Name, string path)
        {
            if(IsExist(path))
            {
                try
                {
                    string[] arrayOldPath = path.Split('\\');
                    arrayOldPath[arrayOldPath.Length - 1] = path;
                    string newPath = string.Join("\\", arrayOldPath);

                    if (!IsExist(newPath) && !File.Exists(newPath))
                    {
                        Directory.Move(path, newPath);

                        return (ConvertToResponceFileSystemElement(new DirectoryInfo(newPath)), string.Empty);
                    }

                    return (null, "Ошибка: Директория/файл с таким названием уже существует");
                }
                catch(Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой директории не существует");
        }

        private ResponceFileSystemElement ConvertToResponceFileSystemElement(DirectoryInfo directoryInfo,
            object data = null) =>
            new ResponceFileSystemElement
            {
                Name = directoryInfo.Name,
                Path = directoryInfo.FullName,
                Type = nameof(Directory),
                Size = GetSize(directoryInfo.FullName),
                Data = data,
            };

        private bool IsExist(string path) => Directory.Exists(path);

        private double GetSize(string path)
        {
            string[] filesAndDirectoryes = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            long size = 0;

            foreach (string file in filesAndDirectoryes)
            {
                FileInfo fileInfo = new FileInfo(file);
                size += fileInfo.Length;
            }

            return size;
        }
    }
}
