using PumClient.PumpServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumClient
{
    public class CallbackHandler : IPumpServiceCallback
    {
        
        public void UpdateStatistics(StatisticService statistic)
        {
            Console.Clear();
            Console.WriteLine("Обновление по статистике выполнения скрипта");
            Console.WriteLine($"Всего тактов: {statistic.AllTacts}");
            Console.WriteLine($"Успешных тактов: {statistic.successTacts}");
            Console.WriteLine($"Ошибочных тактов: {statistic.ErrorTcacts}");

        }
    }
}
