using BlockTime_Tracking.Domains;
using BlockTime_Tracking.Interfaces;
using BlockTime_Tracking.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ZabbixApi;

namespace BlockTime_Tracking.Repositories
{
    public class ZabbixRepository : IZabbixRepository
    {
        public IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroups()
        {
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            var host = context.HostGroups.Get();
            return host;
        }

        public IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroupsMonitoring()
        {
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            var host = context.HostGroups.Get(new
            {
                output = "extend",
                with_monitored_items = new { }
            }); ;

            return host;
        }

        public IEnumerable<ZabbixApi.Entities.HostGroup> GetHostGroupByHost(string nameHost)
        {
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            var host = context.HostGroups.Get(new
            {
                output = "hostid",
                selectGroups = "extend",
                filter = new
                {
                    host = nameHost
                }
            }); ;


            return host;
        }

        public ZabbixApi.Entities.HostGroup GetHostGroupByName(string nameHostGroup)
        {using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            var host = context.HostGroups.GetByName(nameHostGroup);
            return host;
        }

        public string CreateHost(EquipamentozbxViewModel NovoEquipamento)
        {
            EquipamentoRepository equipMtds = new();
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            ZabbixApi.Entities.Host equip = new();
            equip.name = NovoEquipamento.nomeEquipmento;

            foreach (ZabbixApi.Entities.HostGroup item in NovoEquipamento.grupos)
            {
                equip.groups.Add(item);
            }
            foreach (ZabbixApi.Entities.Template item in NovoEquipamento.templates)
            {
                equip.templates.Add(item);
            }

            //equip.groups.Add()
            var host = context.Hosts.Create(equip);
            NoteViewModel novoEqp = new();
            novoEqp.NomeNotebook = equip.name;
            equipMtds.Criar(novoEqp);

            return host;
        }

        public string CreateHostGroup(string nomeEmpresa)
        {
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            ZabbixApi.Entities.HostGroup empresa = new();
            empresa.name = nomeEmpresa;
            var hostgp = context.HostGroups.Create(empresa);
            return hostgp;

        }

        public ZabbixApi.Entities.Host GetHostByName(string nameHost)
        {
            using var context = new Context("http://3.17.0.171/zabbix/api_jsonrpc.php", "Admin", "zabbix");
            var host = context.Hosts.GetByName(nameHost);
            return host;
        }
    }
}
