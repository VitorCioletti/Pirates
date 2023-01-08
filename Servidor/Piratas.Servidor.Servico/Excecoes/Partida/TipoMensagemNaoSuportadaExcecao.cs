namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class TipoMensagemNaoSuportadaExcecao : BaseServicoException
    {
        public TipoMensagemNaoSuportadaExcecao() :
            base("tipo-mensagem-nao-suportada", "Tipo de mensagem não suportada.")
        {
        }
    }
}
