using System;
using System.IO;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Repository
{
    internal class DriveRepository : IFileSystemElement<DriveInfo>
    {
        public (DriveInfo FileSystemElement, string Error) Create(string path)
        {
            throw new NotImplementedException();
        }

        public (bool IsDeleted, string Error) Delete(string path)
        {
            throw new NotImplementedException();
        }

        public (DriveInfo FileSystemElement, string Error) Open(string path)
        {
            throw new NotImplementedException();
        }

        public (DriveInfo FileSystemElement, string Error) Update(string Name, string path)
        {
            throw new NotImplementedException();
        }
    }
}
