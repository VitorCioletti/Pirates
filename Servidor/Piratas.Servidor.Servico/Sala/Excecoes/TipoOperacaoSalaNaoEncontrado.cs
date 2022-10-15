namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    public class TipoOperacaoSalaNaoEncontrado : BaseServicoException
    {
        public TipoOperacaoSalaNaoEncontrado(int tipoAcao) :
            base("tipo-acao-sala-nao-encontrado", $"Tipo da ação de sala  \"{tipoAcao}\" não foi encontrado.")
        {
        }
    }
}
