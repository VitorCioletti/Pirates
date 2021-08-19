namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Duelo;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Tipos;

    public class Papagaio : ResolucaoImediata
    {
        public Papagaio(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.Jogadores, mesa.HistoricoAcao, mesa.ProcessarAcao);

        internal IEnumerable<Resultante> _aplicarEfeito(
             Acao acao, 
             List<Jogador> jogadores, Stack<Acao> historicoAcao, Func<Acao, IEnumerable<Resultante>> processarAcao)
        {
            var ultimaAcao = historicoAcao.FirstOrDefault(
                a => a.Turno == acao.Turno && (a is DescerCarta || a is Duelar));

            if (ultimaAcao == null)
                throw new Exception("Nenhuma ação válida foi realizada");

            if (ultimaAcao is DescerCarta)
            {
                var cartaJogada = ((DescerCarta)ultimaAcao).Carta;

                var tipoNaoPermitido = !(cartaJogada is ResolucaoImediata || cartaJogada is Canhao);

                if (tipoNaoPermitido)
                    throw new Exception($"Não é possível copiar \"{cartaJogada}\".");

                foreach (var resultante in processarAcao(ultimaAcao))
                    yield return resultante;
            }
            else if (ultimaAcao is Duelar)
            {
                var duelarPrimaria = (Duelar)ultimaAcao;
                var realizador = duelarPrimaria.Realizador;

                var cartaIniciadora = duelarPrimaria.CartaIniciadora;

                var outrosJogadores = jogadores.Where(j => j != realizador).ToList();

                Func<Acao, Jogador, IEnumerable<Resultante>> duelarResultante = (acao, jogadorEscolhido) => 
                {
                    var duelar = new Duelar(realizador, jogadorEscolhido, cartaIniciadora);
                    var copiarPrimaria = new CopiarPrimaria(acao, realizador, duelar);

                    return copiarPrimaria as IEnumerable<Resultante>;
                };

                yield return new EscolherJogador(acao, realizador, outrosJogadores, duelarResultante);
            }
            else
                throw new Exception("Ação não reconhecida.");
        }
    }
}