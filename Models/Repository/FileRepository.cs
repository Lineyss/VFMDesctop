using System;
using System.Collections.Generic;
using System.IO;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Repository
{
    internal class FileRepository : IFileSystemElement<FileInfo>
    {
        public (FileInfo FileSystemElement, string Error) Create(string path)
        {
            if (!IsExist(path))
            {
                try
                {
                    FileStream file = File.Create(path);
                    file.Close();
                    return (new FileInfo(path), "");
                }
                catch(Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой файл уже существует");
        }

        public (bool IsDeleted, string Error) Delete(string path)
        {
            if(IsExist(path))
            {
                try
                {
                    File.Delete(path);

                    return (true, "");
                }
                catch(Exception e)
                {
                    return (false, e.Message);
                }
            }

            return (false, "Ошибка: Такой файл не существует");
        }

        public (FileInfo FileSystemElement, string Error) Open(string path)
        {
            throw new NotImplementedException();
        }

        public (FileInfo FileSystemElement, string Error) Update(string Name, string path)
        {
            if (IsExist(path))
            {
                try
                {
                    string[] arrayOldPath = path.Split('\\');
                    arrayOldPath[arrayOldPath.Length - 1] = path;
                    string newPath = string.Join("\\", arrayOldPath);

                    if (!IsExist(newPath))
                    {
                        File.Move(path, newPath);

                        return (new FileInfo(newPath), "");
                    }

                    return (null, "Ошибка: Файл с таким названием уже существует");
                }
                catch (Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой файл не существует");
        }

        private bool IsExist(string path) => File.Exists(path);
    }
}
