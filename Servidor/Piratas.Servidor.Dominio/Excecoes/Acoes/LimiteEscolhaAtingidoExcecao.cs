namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class LimiteEscolhaAtingidoExcecao : BaseAcoesExcecao
    {
        public LimiteEscolhaAtingidoExcecao(BaseAcao baseAcao, int quantidadeEscolhas)
            : base(
                baseAcao,
                "limite-escolha-atingido",
                $"Limite de escolhas para a ação \"{baseAcao}\" foi atigindo. Valor \"{quantidadeEscolhas}\".")
        {
        }
    }
}
