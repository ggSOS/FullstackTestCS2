using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackTestCS2.model
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Patrimonio { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
