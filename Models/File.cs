using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using VFMDesctop.Models.AbsctactModels;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models
{
    internal class File : AFileSystemElement
    {
        public File(string path) : base(path)
        {
        }

        public override (ResponceFileSystemElement, string) Create()
        {
            if (IsExist()) return (null, "Ошибка: Такой файл/директория уже существует");

            try
            {
                FileStream file = System.IO.File.Create(_Path);
                file.Close();
                return (new ResponceFileSystemElement
                {
                    Name = file.Name,
                    Path = _Path,
                    Type = nameof(System.IO.File),
                    Size = file.Length,
                }, "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (bool, string) Delete()
        {
            if (!System.IO.File.Exists(_Path)) return (false, "Ошибка: Такого файла не существует");

            try
            {
                System.IO.File.Delete(_Path);
                return (true, "");
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public override (ResponceFileSystemElement, string) Open()
        {
            if (!System.IO.File.Exists(_Path)) return (null, "Ошибка: Такого файла не существует");

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes(_Path);

                using (MemoryStream memStream = new MemoryStream(bytes))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    object obj = binaryFormatter.Deserialize(memStream);

                    return (new ResponceFileSystemElement
                    {
                        Data = obj,
                        Path = _Path,
                        Name = Path.GetFileName(_Path),
                        Type = nameof(File),
                        Size = GetSize()
                    }, "");
                }
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }
        
        public override (ResponceFileSystemElement, string) Update(string Name)
        {
            if (!System.IO.File.Exists(_Path)) return (null, "Ошибка: Такого файла не существует");

            string[] newFilePathArray = _Path.Split('/');
            newFilePathArray[newFilePathArray.Length - 1] = Name;
            string newFilePath = string.Join("/", newFilePathArray);

            if (!IsExist()) return (null, "Ошибка: Такой файл/директория уже существует");

            try
            {
                System.IO.File.Move(_Path, newFilePath);
                return (new ResponceFileSystemElement
                {
                    Name = Path.GetDirectoryName(newFilePath),
                    Path = newFilePath,
                    Size = Folder.GetSize(newFilePath),
                    Type = nameof(Folder),
                    Data = null
                }, "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }
        protected override double GetSize() => new FileInfo(_Path).Length;
    }
}
