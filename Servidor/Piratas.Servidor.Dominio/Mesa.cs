namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Passiva;
    using Acoes.Tipos;
    using Baralhos.Tipos;
    using Excecoes.Mesa;

    public class Mesa
    {
        public Guid Id { get; private set; }

        public List<Jogador> Jogadores { get; private set; }

        public DateTime DataHoraInicio { get; private set; }

        public DateTime DataHoraFim { get; private set; }

        public Jogador JogadorAtual { get; private set; }

        public bool EmDuelo { get; private set; }

        public Tuple<Jogador, Jogador> Duelistas { get; private set; }

        public Queue<Jogador> OrdemDeJogadores { get; }

        public BaralhoCentral BaralhoCentral { get; }

        public PilhaDescarte PilhaDescarte { get; }

        public Stack<Acao> HistoricoAcao { get; private set; }

        private Imediata _imediataAposResultantes;

        private readonly List<Acao> _acoesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        private int _turnoAtual = 0;

        public Mesa(List<Jogador> jogadores)
        {
            _acoesPendentes = new List<Acao>();
            _imediataAposResultantes = null;

            Id = Guid.NewGuid();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _gerarOrdemDeJogadores(jogadores);

            _distribuirCartas();
        }

        public Dictionary<Jogador, List<Acao>> ProcessarAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            _verificarPrimariaJogadorAtual(acao);
            _verificarResultantePendente(acao);

            Dictionary<Jogador, List<Acao>> acoesPorJogador = new Dictionary<Jogador, List<Acao>>();

            List<Acao> acoesDisponiveis = acao.AplicarRegra(this).ToList();

            HistoricoAcao.Push(acao);

            foreach (var acaoResultante in acoesDisponiveis)
            {
                if (acaoResultante is Imediata)
                {
                    Dictionary<Jogador, List<Acao>> acoesPosImediata = ProcessarAcao(acaoResultante);

                    foreach ((Jogador jogador, List<Acao> acoes) in acoesPosImediata)
                        acoesPorJogador[jogador] = acoes;
                }
            }

            if (acao is Primaria)
                realizador.SubtrairAcoesDisponiveis();

            else if (acao is Resultante resultante)
            {
                _acoesPendentes.Remove(resultante);

                if (_acoesPendentes.Count == 0 && _imediataAposResultantes != null)
                {
                    Dictionary<Jogador, List<Acao>> acoesPosResultante = ProcessarAcao(_imediataAposResultantes);

                    foreach ((Jogador jogador, List<Acao> acoes) in acoesPosResultante)
                        acoesPorJogador[jogador] = acoes;
                }
            }

            acao.Turno = _turnoAtual;

            _acoesPendentes.AddRange(acoesDisponiveis);

            return acoesPorJogador;
        }

        public Dictionary<Jogador, List<Acao>> MoverParaProximoTurno()
        {
            if (JogadorAtual.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisException(JogadorAtual);

            _turnoAtual++;

            var proximoJogador = _obterProximoJogador();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
                Finalizar(proximoJogador);

            var embarcacao = proximoJogador.Campo.Embarcacao;
            Dictionary<Jogador, List<Acao>> acoesPosEfeitoEmbarcacao = null;

            if (embarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, embarcacao);
                acoesPosEfeitoEmbarcacao = ProcessarAcao(aplicarEfeitoEmbarcacao);
            }

            proximoJogador.ResetarAcoesDisponiveis(_acoesPorTurno);

            return acoesPosEfeitoEmbarcacao;
        }

        public void EntrarModoDuelo(Jogador realizador, Jogador alvo)
        {
            if (EmDuelo)
                throw new EmDueloException();

            EmDuelo = true;
            Duelistas = new Tuple<Jogador, Jogador>(realizador, alvo);
        }

        public void SairModoDuelo()
        {
            if (!EmDuelo)
                throw new SemDueloException();

            // TODO: Verificar se todas as ações resposta de duelo foram realizadas?

            EmDuelo = false;
            Duelistas = null;
        }

        public void Finalizar(Jogador _) => DataHoraFim = DateTime.UtcNow;

        public void RegistrarImediataAposResultantes(Imediata imediata)
        {
            if (_imediataAposResultantes != null)
                throw new ImediataRegistradaException();

            _imediataAposResultantes = imediata;
        }

        public void RemoverImediataAposResultantes() => _imediataAposResultantes = null;

        private Queue<Jogador> _gerarOrdemDeJogadores(List<Jogador> jogadores) => new Queue<Jogador>(jogadores);

        private Jogador _obterProximoJogador()
        {
            var proximoJogador = OrdemDeJogadores.Dequeue();
            OrdemDeJogadores.Enqueue(proximoJogador);

            JogadorAtual = proximoJogador;

            return proximoJogador;
        }

        private void _distribuirCartas()
        {
            foreach (var jogador in Jogadores)
            {
                var cartas = BaralhoCentral.ObterTopo(_cartasIniciaisPorJogador);

                jogador.Mao.Adicionar(cartas);
            }
        }

        private void _verificarPrimariaJogadorAtual(Acao acao)
        {
            var realizador = acao.Realizador;

            if (acao is Primaria)
            {
                if (realizador != JogadorAtual)
                    throw new TurnoDeOutroJogadorException(realizador);
            }
        }

        private void _verificarResultantePendente(Acao acao)
        {
            if (acao is Resultante resultante)
            {
                if (!_acoesPendentes.Contains(resultante))
                    throw new ResultanteNaoEsperadaException(resultante);
            }
        }
    }
}
