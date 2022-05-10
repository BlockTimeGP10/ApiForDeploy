using System;
using System.Collections.Generic;

#nullable disable

namespace BlockTime_Tracking.Domains
{
    public partial class Equipamento
    {
        public int IdEquipamento { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime? UltimaAtt { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string NomeNotebook { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; }
    }
}
