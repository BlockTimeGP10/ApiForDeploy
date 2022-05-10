using BlockTime_Tracking.Domains;
using BlockTime_Tracking.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Interfaces
{
    interface IEquipamentoRepository
    {

        Equipamento Criar(NoteViewModel note);

        public Equipamento BuscarPorId(int id);

        List<Equipamento> ListarPorEmpresa(int idEmpresa);

        List<Equipamento> ListarEquipamentos();

        Equipamento BuscarPorNome(string nome);

        void AtualizarEquipamento(NoteViewModel note);
    }
}
