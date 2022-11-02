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

    public class Papagaio : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            List<Jogador> jogadoresNaMesa = mesa.Jogadores;

            BaseAcao ultimaBaseAcao = mesa.HistoricoAcao.FirstOrDefault(
                a => a.Turno == baseAcao.Turno && (a is DescerCarta || a is Duelar));

            if (ultimaBaseAcao == null)
                throw new SemAcaoValidaExcecao(this);

            var acoesResultantes = new List<BaseAcao>();

            switch (ultimaBaseAcao)
            {
                case DescerCarta descerCarta:
                    Carta cartaACopiar = descerCarta.Carta;

                    bool tipoNaoPermitido = !(cartaACopiar is BaseResolucaoImediata || cartaACopiar is Canhao);

                    if (tipoNaoPermitido)
                        throw new ImpossivelCopiarExcecao(this, cartaACopiar);

                    foreach (List<BaseAcao> acoesPorJogador in mesa.ProcessarAcao(ultimaBaseAcao).Values)
                    {
                        foreach (BaseAcao acaoDisponivel in acoesPorJogador)
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
                        baseAcao,
                        realizador,
                        idsOutrosJogadores,
                        DuelarResultante);

                    acoesResultantes.Add(escolherJogador);

                    List<BaseAcao> DuelarResultante(BaseAcao acaoEscolhida, Jogador escolhido)
                    {
                        var duelarCopia = new Duelar(realizador, escolhido, cartaIniciadora);
                        var copiarPrimaria = new CopiarPrimaria(realizador, duelarCopia);

                        var acoesResultantesDuelar = new List<BaseAcao> {copiarPrimaria};

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
