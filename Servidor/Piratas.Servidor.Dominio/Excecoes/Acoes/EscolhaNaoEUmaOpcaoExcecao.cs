namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class EscolhaNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public EscolhaNaoEUmaOpcaoExcecao(BaseAcao baseAcao, string idEscolha) :
            base(baseAcao, "escolha-nao-e-uma-opcao", $"Escolha \"{idEscolha}\" não é uma opção.")
        {
        }
    }
}
