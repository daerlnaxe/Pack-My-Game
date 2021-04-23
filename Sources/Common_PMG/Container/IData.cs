using DxLocalTransf.Copy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.Container
{
    public interface  IData
    {
        public string Name { get; set; }
        public string CurrentPath { get; set; }
        public string DestPath { get; set; }
        public bool IsSelected { get; set; }

    }
}
