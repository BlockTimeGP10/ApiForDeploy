using BlockTime_Tracking.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTime_Tracking.Interfaces
{
    interface IEmpresaRepository
    {
        Empresa BuscarPorId(int idEmpresa);

        List<Empresa> ListarEmpresas();

        void AdcionarEmpresas();

        void Deletar(int idEmpresa);
    }
}
