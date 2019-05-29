using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.DTO.File
{
    public class FileListModelDTO : ListModelBase   
    {
        public string Name { get; set; }

        public int Size { get; set; }
    }
}
