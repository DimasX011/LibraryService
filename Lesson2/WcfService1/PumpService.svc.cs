using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PumpService : IPumpService
    {
        private readonly IScriptService _scriptService;
        private readonly IStatisticService _statisticService;
        private readonly ISettingService _serviceSettings;

        public PumpService()
        {
            _statisticService = new StatisticService();
            _serviceSettings = new SettingService();
            _scriptService = new ScriptService(_statisticService, _serviceSettings, Callback);
        }
        public void RunScript()
        {
            _scriptService.Run(10);
        }

        public void UpdateAndCompileScript(string filename)
        {
            _serviceSettings.filename = filename;
            _scriptService.compile();
        }

        IPumpServiceCallback Callback
        {
            get
            {
                if(OperationContext.Current != null)
                    return OperationContext.Current.GetCallbackChannel<IPumpServiceCallback>();
                    else
                        return null;
            }
        }
    }
}
