using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Repository
{
    internal class FolderRepository : IFileSystemElement<DirectoryInfo>
    {
        public (DirectoryInfo FileSystemElement, string Error) Create(string path)
        {
            if (!IsExist(path))
            {
                try
                {
                    return (Directory.CreateDirectory(path), "");
                }
                catch(Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Директория уже существует");
        }

        public (bool IsDeleted, string Error) Delete(string path)
        {
            if(IsExist(path))
            {
                try
                {
                    Directory.Delete(path);
                    return (true, "");
                }
                catch(Exception e)
                {
                    return (false, e.Message);
                }
            }

            return (false, "Ошибка: Такой директории не существует");
        }

        public (DirectoryInfo FileSystemElement, string Error) Open(string path)
        {
            throw new NotImplementedException();
        }

        public (DirectoryInfo FileSystemElement, string Error) Update(string Name, string path)
        {
            if(IsExist(path))
            {
                try
                {
                    string[] arrayOldPath = path.Split('\\');
                    arrayOldPath[arrayOldPath.Length - 1] = path;
                    string newPath = string.Join("\\", arrayOldPath);

                    if (!IsExist(newPath))
                    {
                        Directory.Move(path, newPath);

                        return (new DirectoryInfo(newPath), "");
                    }

                    return (null, "Ошибка: Директория с таким названием уже существует");
                }
                catch(Exception e)
                {
                    return (null, e.Message);
                }
            }

            return (null, "Ошибка: Такой директории не существует");
        }

        private bool IsExist(string path) => Directory.Exists(path);
    }
}
