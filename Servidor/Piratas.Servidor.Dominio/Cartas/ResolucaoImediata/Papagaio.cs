namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Duelo;
    using Excecoes.Cartas;
    using Tipos;

    public class Papagaio : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            var ultimaAcao = mesa.HistoricoAcao.FirstOrDefault(
                a => a.Turno == acao.Turno && (a is DescerCarta || a is Duelar));

            if (ultimaAcao == null)
                throw new SemAcaoValidaException(this);

            var acoesResultantes = new List<Acao>();

            switch (ultimaAcao)
            {
                case DescerCarta descerCarta:
                    var cartaACopiar = descerCarta.Carta;

                    var tipoNaoPermitido = !(cartaACopiar is ResolucaoImediata || cartaACopiar is Canhao);

                    if (tipoNaoPermitido)
                        throw new ImpossivelCopiarException(this, cartaACopiar);

                    foreach (List<Acao> acoesPorJogador in mesa.ProcessarAcao(ultimaAcao).Values)
                    {
                        foreach (Acao acaoDisponivel in acoesPorJogador)
                        {
                            acoesResultantes.Add(acaoDisponivel);
                        }
                    }

                    break;

                case Duelar duelar:
                    var realizador = duelar.Realizador;

                    var cartaIniciadora = duelar.CartaIniciadora;

                    var outrosJogadores = jogadoresNaMesa.Where(j => j != realizador).ToList();
                    var escolherJogador = new EscolherJogador(acao, realizador, outrosJogadores, DuelarResultante);

                    acoesResultantes.Add(escolherJogador);

                    List<Acao> DuelarResultante(Acao acaoEscolhida, Jogador escolhido)
                    {
                        var duelarCopia = new Duelar(realizador, escolhido, cartaIniciadora);
                        var copiarPrimaria = new CopiarPrimaria(acaoEscolhida, realizador, duelarCopia);

                        var acoesResultantesDuelar = new List<Acao> { copiarPrimaria };

                        return acoesResultantesDuelar;
                    }

                    break;

                default:
                    throw new SemAcaoValidaException(this);
            }

            return acoesResultantes;
        }
    }
}
