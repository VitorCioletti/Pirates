namespace Piratas.Servidor.Servico.Partida.Excecoes
{
    using System;

    public class PartidaNaoEncontradaException : BasePartidaException
    {
        public PartidaNaoEncontradaException(Guid idPartida) :
            base("partida-nao-encontrada", $"Partida de id \"{idPartida}\" não encontrada.")
        {
        }
    }
}
