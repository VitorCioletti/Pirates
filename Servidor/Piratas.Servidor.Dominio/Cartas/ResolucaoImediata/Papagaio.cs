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

            Acao ultimaAcao = mesa.HistoricoAcao.FirstOrDefault(
                a => a.Turno == acao.Turno && (a is DescerCarta || a is Duelar));

            if (ultimaAcao == null)
                throw new SemAcaoValidaExcecao(this);

            var acoesResultantes = new List<Acao>();

            switch (ultimaAcao)
            {
                case DescerCarta descerCarta:
                    Carta cartaACopiar = descerCarta.Carta;

                    bool tipoNaoPermitido = !(cartaACopiar is ResolucaoImediata || cartaACopiar is Canhao);

                    if (tipoNaoPermitido)
                        throw new ImpossivelCopiarExcecao(this, cartaACopiar);

                    foreach (List<Acao> acoesPorJogador in mesa.ProcessarAcao(ultimaAcao).Values)
                    {
                        foreach (Acao acaoDisponivel in acoesPorJogador)
                        {
                            acoesResultantes.Add(acaoDisponivel);
                        }
                    }

                    break;

                case Duelar duelar:
                    Jogador realizador = duelar.Realizador;

                    Duelo cartaIniciadora = duelar.CartaIniciadora;

                    List<Jogador> outrosJogadores = jogadoresNaMesa.Where(j => j != realizador).ToList();
                    List<string> idsOutrosJogadores = outrosJogadores.Select(j => j.Id.ToString()).ToList();

                    var escolherJogador = new EscolherJogador(
                        acao,
                        realizador,
                        idsOutrosJogadores,
                        DuelarResultante);

                    acoesResultantes.Add(escolherJogador);

                    List<Acao> DuelarResultante(Acao acaoEscolhida, Jogador escolhido)
                    {
                        var duelarCopia = new Duelar(realizador, escolhido, cartaIniciadora);
                        var copiarPrimaria = new CopiarPrimaria(acaoEscolhida, realizador, duelarCopia);

                        var acoesResultantesDuelar = new List<Acao> {copiarPrimaria};

                        return acoesResultantesDuelar;
                    }

                    break;

                default:
                    throw new SemAcaoValidaExcecao(this);
            }

            return acoesResultantes;
        }
    }
}
