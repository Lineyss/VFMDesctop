using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Models.Help;

namespace VFMDesctop.Models
{
    internal class Drive : AFileSystemElement<string[]>
    {
        private readonly DriveInfo drive;
        public Drive(string path) : base(path)
        {
            IsFile = false;
            drive = DriveInfo.GetDrives().FirstOrDefault(element => element.Name == this.Path);
        }

        public override (bool, string) Create() => (false, "Ошибка: Действие не доступно для данного объекта");

        public override (bool, string) Delete() => (false, "Ошибка: Действие не доступно для данного объекта");

        public override (AFileSystemElement<string[]>, string) Get() => (this, "");

        public override (string[], string) Open()
        {
            try
            {
                string[] files = Directory.GetDirectories(this.Path);
                string[] directories = Directory.GetFiles(this.Path);

                return (files.Concat(directories).ToArray(), "");
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public override (bool, string) Update(string Name) => (false, "Ошибка: Действие не доступно для данного объекта");

        protected override double GetSize() => drive.TotalSize;
    }
}
