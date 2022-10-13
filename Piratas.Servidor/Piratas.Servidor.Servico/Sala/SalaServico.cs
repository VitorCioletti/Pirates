namespace Piratas.Servidor.Servico.Sala
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Excecoes;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;

    public static class SalaServico
    {
        private static Dictionary<Guid, List<Guid>> _salasAbertas { get; }

        private static object _lockSala { get; }

        static SalaServico()
        {
            _salasAbertas = new Dictionary<Guid, List<Guid>>();
            _lockSala = new Object();
        }

        public static List<MensagemSalaServidor> ProcessarMensagemCliente(MensagemSalaCliente mensagemSalaCliente)
        {
            lock (_lockSala)
                return _processarMensagemCliente(mensagemSalaCliente);
        }

        private static List<MensagemSalaServidor> _processarMensagemCliente(MensagemSalaCliente mensagemSalaCliente)
        {
            var mensagensSalaServidor = new List<MensagemSalaServidor>();

            switch (mensagemSalaCliente.TipoAcaoSala)
            {
                case TipoAcaoSala.Criar:
                    MensagemSalaServidor mensagemCriacaoSala = _criarSala(mensagemSalaCliente.IdJogadorRealizouAcao);

                    mensagensSalaServidor.Add(mensagemCriacaoSala);
                    break;

                case TipoAcaoSala.Sair:
                    List<MensagemSalaServidor> mensagensSaidaSala = _sairSala(
                        mensagemSalaCliente.IdJogadorRealizouAcao);

                    mensagensSalaServidor.AddRange(mensagensSaidaSala);
                    break;

                case TipoAcaoSala.Entrar:
                    List<MensagemSalaServidor> mensagensEntradaSala = _entrarSala(
                        mensagemSalaCliente.IdJogadorRealizouAcao,
                        mensagemSalaCliente.IdSala);

                    mensagensSalaServidor.AddRange(mensagensEntradaSala);
                    break;

                case TipoAcaoSala.IniciarPartida:
                    break;

                default:
                    throw new TipoAcaoSalaNaoEncontrado((int)mensagemSalaCliente.TipoAcaoSala);
            }


            return mensagensSalaServidor;
        }

        private static MensagemSalaServidor _criarSala(Guid idJogadorCriador)
        {
            bool estaNumaSala = _estaNumaSala(idJogadorCriador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaException(idJogadorCriador);

            Guid idNovaSala = Guid.NewGuid();

            _salasAbertas[idNovaSala] = new List<Guid> { idJogadorCriador };

            return new MensagemSalaServidor(
                idNovaSala,
                idJogadorCriador,
                idJogadorCriador,
                TipoAcaoSalaServidor.Criou);
        }

        private static List<MensagemSalaServidor> _sairSala(Guid idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idSala == Guid.Empty)
                throw new JogadorNaoEstaEmNenhumaSala(idJogador);

            List<Guid> sala = _salasAbertas[idSala];

            var mensagensSaidaSala = _criarMensagensServidor(idJogador, idSala, TipoAcaoSalaServidor.JogadorSaiu);

            sala.Remove(idJogador);

            return mensagensSaidaSala;
        }

        private static List<MensagemSalaServidor> _entrarSala(Guid idJogador, Guid idSala)
        {
            bool estaNumaSala = _estaNumaSala(idJogador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaException(idJogador);

            bool salaNaoExiste = !_salasAbertas.ContainsKey(idSala);

            if (salaNaoExiste)
                throw new SalaNaoEncontradaException(idSala);

            var mensagensEntradaSala = _criarMensagensServidor(idJogador, idSala, TipoAcaoSalaServidor.JogadorEntrou);

            _salasAbertas[idSala].Add(idJogador);

            return mensagensEntradaSala;
        }

        private static List<MensagemSalaServidor> _criarMensagensServidor(
            Guid idJogador,
            Guid idSala,
            TipoAcaoSalaServidor tipoAcaoSalaServidor)
        {
            var mensagensSalaServidor = new List<MensagemSalaServidor>();

            List<Guid> jogadoresSala = _salasAbertas[idSala];

            foreach (Guid id in jogadoresSala)
            {
                var mensagemSaidaSala = new MensagemSalaServidor(idSala, id, idJogador, tipoAcaoSalaServidor);

                mensagensSalaServidor.Add(mensagemSaidaSala);
            }

            return mensagensSalaServidor;
        }

        private static bool _estaNumaSala(Guid idJogador) => _salasAbertas.Values.Any(s => s.Contains(idJogador));
    }
}
