namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class TripulacaoVaziaExcecao : BaseCampoExcecao
    {
        public TripulacaoVaziaExcecao() : base("tripulacao-vazia", "Não há tripulação no campo")
        {
        }
    }
}
