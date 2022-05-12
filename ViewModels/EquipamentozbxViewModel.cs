using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.ViewModels
{
    public class EquipamentozbxViewModel
    {
        public string nomeEquipmento { get; set; }
        public string ip { get; set; }

        public List<ZabbixApi.Entities.Template> templates { get; set;}

        public List<ZabbixApi.Entities.HostGroup> grupos { get; set; }
        
        
    }
}
