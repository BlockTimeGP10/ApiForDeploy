using System;
using System.Collections.Generic;

#nullable disable

namespace BlockTime_Tracking.Domains
{
    public partial class Empresa
    {
        public Empresa()
        {
            Equipamentos = new HashSet<Equipamento>();
        }

        public int IdEmpresa { get; set; }
        public string NomeEmpresa { get; set; }

        public virtual ICollection<Equipamento> Equipamentos { get; set; }
    }
}
