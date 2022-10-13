namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    public class TipoAcaoSalaNaoEncontrado : BaseServicoException
    {
        public TipoAcaoSalaNaoEncontrado(int tipoAcao) :
            base("tipo-acao-sala-nao-encontrado", $"Tipo da ação de sala  \"{tipoAcao}\" não foi encontrado.")
        {
        }
    }
}
