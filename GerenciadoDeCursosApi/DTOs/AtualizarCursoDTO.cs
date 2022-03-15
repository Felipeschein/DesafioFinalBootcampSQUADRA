using GerenciadorDeCursosApi.Enum;

namespace GerenciadorDeCursosApi.DTOs
{
    public class AtualizarCursoDTO
    {
        public int Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
