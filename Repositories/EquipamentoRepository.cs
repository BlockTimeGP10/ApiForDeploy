using BlockTime_Tracking.Contexts;
using BlockTime_Tracking.Domains;
using BlockTime_Tracking.Interfaces;
using BlockTime_Tracking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        readonly BlockTrackingContext ctx = new();
        public Equipamento Criar(NoteViewModel noteAgente)
        {
            ZabbixRepository zabbix = new();
            EmpresaRepository empresa = new();
            Equipamento equipamento = new();

            var noteBuscado = BuscarPorNome(noteAgente.NomeNotebook);
            
            if (noteBuscado == null)
            {
                var hostZabbix = zabbix.GetHostByName(noteAgente.NomeNotebook);

                equipamento.Lat = noteAgente.Lat;
                equipamento.Lng = noteAgente.Lng;
                equipamento.IdEquipamento = Int32.Parse(hostZabbix.Id);
                equipamento.NomeNotebook = noteAgente.NomeNotebook;
                equipamento.UltimaAtt = DateTime.Now;

                var Grupos = hostZabbix.groups.ToArray();
                foreach (ZabbixApi.Entities.HostGroup item in Grupos)
                {
                    var empresaBanco = empresa.BuscarPorId(Int32.Parse(item.Id));

                    if (empresaBanco != null)
                    {
                        equipamento.IdEmpresa = empresaBanco.IdEmpresa;
                    }
                }
                ctx.Equipamentos.Add(equipamento);
                ctx.SaveChanges();

                return equipamento;
            }
            else
            {
                Equipamento equipAtualizar = BuscarPorNome(noteAgente.NomeNotebook);
                if (equipAtualizar != null)
                {
                    equipAtualizar.Lat = noteAgente.Lat;
                    equipAtualizar.Lng = noteAgente.Lng;
                    equipAtualizar.UltimaAtt = DateTime.Now;
                }

                ctx.Equipamentos.Update(equipAtualizar);
                ctx.SaveChanges();

                return equipAtualizar;
            }
         }

        public Equipamento BuscarPorId(int id)
        {
            return ctx.Equipamentos.FirstOrDefault(ab => ab.IdEquipamento == id);
        }

        public List<Equipamento> ListarPorEmpresa(int idEmpresa)
        {
            List<Equipamento> equipamentos = new();

            EmpresaRepository empresaMtds = new();
            ZabbixRepository zabbix = new();

            var empresaEquips = empresaMtds.BuscarPorId(idEmpresa);
            var  resposta = zabbix.GetHostGroupByName(empresaEquips.NomeEmpresa);

            foreach (ZabbixApi.Entities.Host item in resposta.hosts)
            {
                var equipamento = BuscarPorNome(item.name);
                equipamentos.Add(equipamento);
            }

            return equipamentos;
        }

        public List<Equipamento> ListarEquipamentos()
        {
            return ctx.Equipamentos.ToList();
        }

        public Equipamento BuscarPorNome(string nome)
        {
            ZabbixRepository zbx = new();
            var equipamento = zbx.GetHostByName(nome);
            Equipamento equip = BuscarPorId(Int32.Parse(equipamento.Id));

            return equip;
        }

        public void AtualizarEquipamento(NoteViewModel note)
        {
            Equipamento equipAtualizar = BuscarPorNome(note.NomeNotebook);
            if (equipAtualizar != null)
            {
                equipAtualizar.Lat = note.Lat;
                equipAtualizar.Lng = note.Lng;
                equipAtualizar.UltimaAtt = DateTime.Now;
            }

            ctx.Equipamentos.Update(equipAtualizar);
            ctx.SaveChanges();
        }

    }
}
