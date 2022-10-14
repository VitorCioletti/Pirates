namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class TripulanteNaoEncontradoException : BaseCampoException
    {
        public TripulanteNaoEncontradoException()
            : base("tripulante-nao-encontrado", "Tripulante não encontrado no campo.")
        {
        }
    }
}
