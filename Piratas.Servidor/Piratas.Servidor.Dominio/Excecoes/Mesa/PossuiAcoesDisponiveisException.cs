namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class PossuiAcoesDisponiveisException : MesaException
    {
        public PossuiAcoesDisponiveisException(Jogador jogador)
            : base("possui-acoes-disponiveis", $"Jogador \"{jogador.Id}\" ainda possui ações disponíveis.")
        {
        }
    }
}
