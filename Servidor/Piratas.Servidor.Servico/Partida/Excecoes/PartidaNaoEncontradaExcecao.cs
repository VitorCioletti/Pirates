namespace Piratas.Servidor.Servico.Partida.Excecoes
{
    using System;

    public class PartidaNaoEncontradaExcecao : BasePartidaExcecao
    {
        public PartidaNaoEncontradaExcecao(Guid idPartida) :
            base("partida-nao-encontrada", $"Partida de id \"{idPartida}\" não encontrada.")
        {
        }
    }
}
