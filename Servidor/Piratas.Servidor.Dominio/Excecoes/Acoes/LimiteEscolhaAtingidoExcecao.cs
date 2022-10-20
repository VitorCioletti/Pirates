namespace Piratas.Servidor.Dominio.Excecoes.Acoes;

using Dominio.Acoes;

public class LimiteEscolhaAtingidoExcecao : BaseAcoesExcecao
{
    public LimiteEscolhaAtingidoExcecao(Acao acao, int quantidadeEscolhas)
        : base(acao, "limite-escolha-atingido", $"Limite de escolhas para a ação \"{acao}\" foi atigindo. Valor \"{quantidadeEscolhas}\".")
    {
    }
}
