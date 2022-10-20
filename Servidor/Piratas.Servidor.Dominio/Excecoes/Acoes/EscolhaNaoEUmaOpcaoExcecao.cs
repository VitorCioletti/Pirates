namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class EscolhaNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public EscolhaNaoEUmaOpcaoExcecao(Acao acao, string idEscolha) :
            base(acao, "escolha-nao-e-uma-opcao", $"Escolha \"{idEscolha}\" não é uma opção.")
        {
        }
    }
}
