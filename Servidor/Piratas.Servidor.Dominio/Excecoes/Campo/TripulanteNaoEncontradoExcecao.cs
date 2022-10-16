namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class TripulanteNaoEncontradoExcecao : BaseCampoExcecao
    {
        public TripulanteNaoEncontradoExcecao()
            : base("tripulante-nao-encontrado", "Tripulante não encontrado no campo.")
        {
        }
    }
}
