using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.AbsctactModels;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models
{
    internal class Drive : AFileSystemElement
    {
        private readonly DriveInfo drive;
        public Drive(string path) : base(path)
        {
            drive = DriveInfo.GetDrives().FirstOrDefault(element => element.Name == _Path);
        }

        public override (ResponceFileSystemElement, string) Create() => (null, "Ошибка: Действие не доступно для данного объекта");

        public override (bool, string) Delete() => (false, "Ошибка: Действие не доступно для данного объекта");

        public override (ResponceFileSystemElement, string) Open()
        {
            try
            {
                ResponceFileSystemElement[] folders = Directory.GetDirectories(_Path)
                    .Select(folder => new ResponceFileSystemElement
                    {
                        Name = Path.GetDirectoryName(folder),
                        Path = folder,
                        Type = nameof(Folder),
                        Size = Folder.GetSize(folder)
                    }).ToArray();

                ResponceFileSystemElement[] files = Directory.GetFiles(_Path)
                    .Select(file => new ResponceFileSystemElement
                    {
                        Name = Path.GetFileName(file),
                        Path = file,
                        Type = nameof(File),
                        Size = new FileInfo(file).Length
                    }).ToArray() ;

                return (new ResponceFileSystemElement
                {
                    Name = drive.Name,
                    Path = _Path,
                    Type = nameof(Drive),
                    Size = GetSize(),
                    Data = folders.Concat(files)
                }, "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (ResponceFileSystemElement, string) Update(string Name) => (null, "Ошибка: Действие не доступно для данного объекта");

        protected override double GetSize() => drive.TotalSize;
    }
}
