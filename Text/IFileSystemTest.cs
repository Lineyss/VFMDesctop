using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Text
{
    internal interface IFileSystemTest
    {
        string[] Open();
        void Get();
        void Create();
        void Delete();
    }
}