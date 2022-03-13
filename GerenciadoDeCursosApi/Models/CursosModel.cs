using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadoDeCursosApi.Models
{
    public class CursosModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double Duracao { get; set; }
        public string Status { get; set; }
    }
}
