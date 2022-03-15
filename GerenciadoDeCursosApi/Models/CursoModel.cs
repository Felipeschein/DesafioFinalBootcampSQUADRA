
using GerenciadorDeCursosApi.Enum;

namespace GerenciadorDeCursosApi.Models
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double Duracao { get; set; }
        public StatusEnum Status { get; set; }

        public CursoModel (int id, string titulo, double duracao, StatusEnum status)
        {
            Id = id;
            Titulo = titulo;
            Duracao = duracao;
            Status = status;
        }
    }
}
