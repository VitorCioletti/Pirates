namespace Piratas.Servidor.Servico.Sala
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Partida;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;
    using Servico.Excecoes.Sala;

    public static class SalaServico
    {
        // TODO: Implementar líder da sala que pode iniciar a partida.
        private static Dictionary<Guid, List<Guid>> _salasAbertas { get; }

        private static object _lockSala { get; }

        static SalaServico()
        {
            _salasAbertas = new Dictionary<Guid, List<Guid>>();
            _lockSala = new object();
        }

        public static List<MensagemSalaServidor> ProcessarMensagemCliente(MensagemSalaCliente mensagemSalaCliente)
        {
            lock (_lockSala)
                return _processarMensagemCliente(mensagemSalaCliente);
        }

        private static List<MensagemSalaServidor> _processarMensagemCliente(MensagemSalaCliente mensagemSalaCliente)
        {
            var mensagensSalaServidor = new List<MensagemSalaServidor>();

            switch (mensagemSalaCliente.TipoOperacaoSala)
            {
                case TipoOperacaoSala.Criar:
                    MensagemSalaServidor mensagemCriacaoSala = _criarSala(mensagemSalaCliente.IdJogadorRealizouAcao);

                    mensagensSalaServidor.Add(mensagemCriacaoSala);
                    break;

                case TipoOperacaoSala.Sair:
                    IEnumerable<MensagemSalaServidor> mensagensSaidaSala =
                        _sairSala(mensagemSalaCliente.IdJogadorRealizouAcao);

                    mensagensSalaServidor.AddRange(mensagensSaidaSala);
                    break;

                case TipoOperacaoSala.Entrar:
                    IEnumerable<MensagemSalaServidor> mensagensEntradaSala = _entrarSala(
                        mensagemSalaCliente.IdJogadorRealizouAcao,
                        mensagemSalaCliente.IdSala);

                    mensagensSalaServidor.AddRange(mensagensEntradaSala);
                    break;

                case TipoOperacaoSala.IniciarPartida:
                    IEnumerable<MensagemSalaServidor> mensagensInicioPartida =
                        _iniciarPartida(mensagemSalaCliente.IdJogadorRealizouAcao);

                    mensagensSalaServidor.AddRange(mensagensInicioPartida);
                    break;

                default:
                    throw new TipoOperacaoSalaNaoEncontrado((int)mensagemSalaCliente.TipoOperacaoSala);
            }

            return mensagensSalaServidor;
        }

        private static MensagemSalaServidor _criarSala(Guid idJogadorCriador)
        {
            bool estaNumaSala = _estaNumaSala(idJogadorCriador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaExcecao(idJogadorCriador);

            var idNovaSala = Guid.NewGuid();

            _salasAbertas[idNovaSala] = new List<Guid> { idJogadorCriador };

            return new MensagemSalaServidor(
                idNovaSala,
                idJogadorCriador,
                idJogadorCriador,
                Guid.Empty,
                TipoOperacaoSalaServidor.Criou);
        }

        private static IEnumerable<MensagemSalaServidor> _sairSala(Guid idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idSala == Guid.Empty)
                throw new JogadorNaoEstaEmNenhumaSala(idJogador);

            List<Guid> sala = _salasAbertas[idSala];

            IEnumerable<MensagemSalaServidor> mensagensSaidaSala = _criarMensagensServidor(
                idJogador,
                idSala,
                Guid.Empty,
                TipoOperacaoSalaServidor.JogadorSaiu);

            sala.Remove(idJogador);

            return mensagensSaidaSala;
        }

        private static IEnumerable<MensagemSalaServidor> _entrarSala(Guid idJogador, Guid idSala)
        {
            bool estaNumaSala = _estaNumaSala(idJogador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaExcecao(idJogador);

            bool salaNaoExiste = !_salasAbertas.ContainsKey(idSala);

            if (salaNaoExiste)
                throw new SalaNaoEncontradaExcecao(idSala);

            IEnumerable<MensagemSalaServidor> mensagensEntradaSala = _criarMensagensServidor(
                idJogador,
                idSala,
                Guid.Empty,
                TipoOperacaoSalaServidor.JogadorEntrou);

            _salasAbertas[idSala].Add(idJogador);

            return mensagensEntradaSala;
        }

        private static IEnumerable<MensagemSalaServidor> _iniciarPartida(Guid idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idSala == Guid.Empty)
                throw new JogadorNaoEstaEmNenhumaSala(idJogador);

            List<Guid> idsJogadores = _salasAbertas[idSala];

            Guid idPartida = GerenciadorPartidaServico.CriarPartida(idsJogadores);

            IEnumerable<MensagemSalaServidor> mensagensCriacaoPartida = _criarMensagensServidor(
                idJogador,
                Guid.Empty,
                idPartida,
                TipoOperacaoSalaServidor.IniciouPartida);

            _salasAbertas.Remove(idSala);

            return mensagensCriacaoPartida;
        }

        private static IEnumerable<MensagemSalaServidor> _criarMensagensServidor(
            Guid idJogador,
            Guid idSala,
            Guid idPartida,
            TipoOperacaoSalaServidor tipoOperacaoSalaServidor)
        {
            var mensagensSalaServidor = new List<MensagemSalaServidor>();

            List<Guid> jogadoresSala = _salasAbertas[idSala];

            foreach (Guid id in jogadoresSala)
            {
                var mensagemSaidaSala = new MensagemSalaServidor(
                    idSala,
                    id,
                    idJogador,
                    idPartida,
                    tipoOperacaoSalaServidor);

                mensagensSalaServidor.Add(mensagemSaidaSala);
            }

            return mensagensSalaServidor;
        }

        private static bool _estaNumaSala(Guid idJogador) => _salasAbertas.Values.Any(s => s.Contains(idJogador));
    }
}
