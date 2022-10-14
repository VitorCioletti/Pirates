namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class TripulacaoVaziaException : BaseCampoException
    {
        public TripulacaoVaziaException() : base("tripulacao-vazia", "Não há tripulação no campo")
        {
        }
    }
}
