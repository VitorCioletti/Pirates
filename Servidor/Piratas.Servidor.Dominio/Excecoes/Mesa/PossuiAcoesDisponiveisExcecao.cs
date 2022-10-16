namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class PossuiAcoesDisponiveisExcecao : BaseMesaExcecao
    {
        public PossuiAcoesDisponiveisExcecao(Jogador jogador)
            : base("possui-acoes-disponiveis", $"Jogador \"{jogador.Id}\" ainda possui ações disponíveis.")
        {
        }
    }
}
