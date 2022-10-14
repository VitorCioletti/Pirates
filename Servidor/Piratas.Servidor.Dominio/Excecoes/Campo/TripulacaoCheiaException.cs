namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class TripulacaoCheiaException : BaseCampoException
    {
        public TripulacaoCheiaException() : base("tripulacao-cheia", "Tripulação do campo está cheia.")
        {
        }
    }
}
