namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class NaoPossuiCartaDueloExcecao : BaseAcoesExcecao
    {
        public NaoPossuiCartaDueloExcecao(BaseAcao acao)
            : base(
                acao,
                "nao-possui-carta-duelo",
                $"Jogador \"{acao.Realizador.Id}\" não possui carta de duelo.")
        {
        }
    }
}
