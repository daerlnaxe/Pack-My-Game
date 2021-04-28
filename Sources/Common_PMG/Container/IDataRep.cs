using DxLocalTransf.Copy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.Container
{
    public interface IDataRep: IData
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

    }
}
