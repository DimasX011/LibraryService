using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1
{
    public class StatisticService : IStatisticService
    {
        public int successTacts { get ; set ; }
        public int ErrorTcacts { get; set; }
        public int AllTacts { get; set; }
    }
}