namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Duelo;
    using Excecoes.Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Tipos;

    public class Papagaio : ResolucaoImediata
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.Jogadores, mesa.HistoricoAcao, mesa.ProcessarAcao);

        internal IEnumerable<Resultante> _aplicarEfeito(
             Acao acao,
             List<Jogador> jogadores,
             Stack<Acao> historicoAcao,
             Func<Acao, IEnumerable<Resultante>> processarAcao)
        {
            var ultimaAcao = historicoAcao.FirstOrDefault(
                a => a.Turno == acao.Turno && (a is DescerCarta || a is Duelar));

            if (ultimaAcao == null)
                throw new SemAcaoValidaException(this);

            switch (ultimaAcao)
            {
                case DescerCarta descerCarta:
                    var cartaACopiar = descerCarta.Carta;

                    var tipoNaoPermitido = !(cartaACopiar is ResolucaoImediata || cartaACopiar is Canhao);

                    if (tipoNaoPermitido)
                        throw new ImpossivelCopiarException(this, cartaACopiar);

                    foreach (var resultante in processarAcao(ultimaAcao))
                        yield return resultante;

                    break;

                case Duelar duelar:
                    var realizador = duelar.Realizador;

                    var cartaIniciadora = duelar.CartaIniciadora;

                    var outrosJogadores = jogadores.Where(j => j != realizador).ToList();

                    IEnumerable<Resultante> duelarResultante(Acao acao, Jogador escolhido)
                    {
                        var duelar = new Duelar(realizador, escolhido, cartaIniciadora);
                        var copiarPrimaria = new CopiarPrimaria(acao, realizador, duelar);

                        return copiarPrimaria as IEnumerable<Resultante>;
                    }

                    yield return new EscolherJogador(acao, realizador, outrosJogadores, duelarResultante);
                    break;

                default:
                    throw new SemAcaoValidaException(this);
            }
        }
    }
}
