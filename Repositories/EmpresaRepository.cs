using BlockTime_Tracking.Contexts;
using BlockTime_Tracking.Domains;
using BlockTime_Tracking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        readonly BlockTrackingContext ctx = new();

        public Empresa BuscarPorId(int idEmpresa)
        {
            return ctx.Empresas.FirstOrDefault(ab => ab.IdEmpresa == idEmpresa);
        }

        public List<Empresa> ListarEmpresas()
        {
            return ctx.Empresas.ToList();
        }

        public void Deletar(int idEmpresa)
        {

            Empresa empresaBuscada = BuscarPorId(idEmpresa);

            ctx.Empresas.Remove(empresaBuscada);
            ctx.SaveChanges();

        }

        public void CadastrarEmpresa()
        {

        }

        public void AdcionarEmpresas()
        {
            ZabbixRepository zabbix = new();

            var listaGrupos = zabbix.GetHostGroupsMonitoring();

            foreach(ZabbixApi.Entities.HostGroup item in listaGrupos)
            {
                string regex = @"^\b[e | E]\Bmpres\Ba\b";
                if (Regex.IsMatch(item.name, regex))
                {
                    var empresaExistente = BuscarPorId(Int32.Parse(item.Id));

                    if(empresaExistente == null)
                    {
                        Empresa empresaBd = new();

                        empresaBd.IdEmpresa = Int32.Parse(item.Id);
                        empresaBd.NomeEmpresa = item.name;

                        ctx.Empresas.Add(empresaBd);
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}
