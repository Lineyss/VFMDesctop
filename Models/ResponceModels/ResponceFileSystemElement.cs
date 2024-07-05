using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Models.ResponceModels
{
    internal class ResponceFileSystemElement
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public object Data { get; set; } = null;
    }
}
