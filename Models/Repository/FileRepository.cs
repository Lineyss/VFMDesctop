using System;
using System.Collections.Generic;
using System.IO;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Repository
{
    internal class FileRepository : IFileSystemRepository
    {
        public (ResponceFileSystemElement FileSystemElement, string Error) Create(string path)
        {
            if (!IsExist(path) && !Directory.Exists(path))
            {
                try
                {
                    FileStream file = File.Create(path);
                    file.Close();
                    return (ConvertToResponceFileSystemElement(new FileInfo(path)), string.Empty);
                }
                catch (Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой файл/директория уже существует");
        }

        public (bool IsDeleted, string Error) Delete(string path)
        {
            if (IsExist(path))
            {
                try
                {
                    File.Delete(path);

                    return (true, string.Empty);
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }

            return (false, "Ошибка: Такой файл не существует");
        }

        public (ResponceFileSystemElement FileSystemElement, string Error) Open(string path)
        {
            if (IsExist(path))
            {
                try
                {
                    return (ConvertToResponceFileSystemElement(new FileInfo(path)), string.Empty);
                }
                catch (Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой файл не существует");
        }

        public (ResponceFileSystemElement FileSystemElement, string Error) Update(string Name, string path)
        {
            if (IsExist(path))
            {
                try
                {
                    string[] arrayOldPath = path.Split('\\');
                    arrayOldPath[arrayOldPath.Length - 1] = path;
                    string newPath = string.Join("\\", arrayOldPath);

                    if (!IsExist(newPath) && !Directory.Exists(path))
                    {
                        File.Move(path, newPath);

                        return (ConvertToResponceFileSystemElement(new FileInfo(newPath)), string.Empty);
                    }

                    return (null, "Ошибка: Файл/директория с таким названием уже существует");
                }
                catch (Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой файл не существует");
        }

        private bool IsExist(string path) => File.Exists(path);
        private ResponceFileSystemElement ConvertToResponceFileSystemElement(FileInfo file,
            object data=null) => new ResponceFileSystemElement
            {
                Data = data,
                Path = file.FullName,
                Name = file.Name,
                Type = nameof(File),
                Size = file.Length
            };
    }
}
