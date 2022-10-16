namespace Piratas.Servidor.Servico.Excecoes.Partida
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
