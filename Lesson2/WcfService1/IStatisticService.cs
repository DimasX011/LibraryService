using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    public interface IStatisticService
    {
        int successTacts { get; set; }

        int ErrorTcacts { get; set; }

        int AllTacts { get; set; }
    }
}
