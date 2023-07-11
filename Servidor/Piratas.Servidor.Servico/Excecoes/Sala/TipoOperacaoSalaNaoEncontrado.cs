namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    public class TipoOperacaoSalaNaoEncontrado : BaseServicoExcecao
    {
        public TipoOperacaoSalaNaoEncontrado(int tipoAcao) :
            base("tipo-acao-sala-nao-encontrado", $"Tipo da ação de sala  \"{tipoAcao}\" não foi encontrado.")
        {
        }
    }
}
