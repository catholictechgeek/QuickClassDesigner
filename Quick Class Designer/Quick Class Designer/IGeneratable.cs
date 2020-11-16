using System;
using System.Collections.Generic;
using System.Text;

namespace QuickClassDesigner
{
    public interface IGeneratable
    {
        string generateSourceCode(int separatorcount = 1);
    }
}
