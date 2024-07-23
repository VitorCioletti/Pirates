namespace Pirates.Server.Service.Exception.Room
{
    public class TipoOperacaoSalaNaoEncontrado : BaseServiceException
    {
        public TipoOperacaoSalaNaoEncontrado(int tipoAcao) :
            base("tipo-acao-sala-nao-encontrado", $"Tipo da ação de sala  \"{tipoAcao}\" não foi encontrado.")
        {
        }
    }
}
