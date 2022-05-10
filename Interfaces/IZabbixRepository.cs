using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Interfaces
{
    interface IZabbixRepository
    {
        IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroups();
        public IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroupsMonitoring();

        IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroupByHost(string nameHost);

        public ZabbixApi.Entities.HostGroup GetHostGroupByName(string nameHostGroup);

        public ZabbixApi.Entities.Host GetHostByName(string nameHost);
    }
}
