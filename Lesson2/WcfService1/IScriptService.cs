using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    public interface IScriptService
    {
        bool compile();

        void Run(int count);
    }
}
