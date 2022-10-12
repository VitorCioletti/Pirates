namespace Piratas.Servidor.Servico
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;

    // TODO: Responsável por criar partidas. As salas abertas e seus jogadores devem ficar aqui.
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
                    break;

                case TipoAcaoSala.IniciarPartida:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }


            return mensagensSalaServidor;
        }

        private static MensagemSalaServidor _criarSala(Guid idJogadorCriador)
        {
            bool estaNumaSala = _estaNumaSala(idJogadorCriador);

            if (estaNumaSala)
                throw new Exception();

            Guid idNovaSala = Guid.NewGuid();

            _salasAbertas[idNovaSala] = new List<Guid> {idJogadorCriador};

            return new MensagemSalaServidor(
                idNovaSala,
                idJogadorCriador,
                idJogadorCriador,
                TipoAcaoSalaServidor.Criou);
        }

        private static List<MensagemSalaServidor> _sairSala(Guid idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idJogador == null)
                throw new Exception();

            List<Guid> sala = _salasAbertas[idSala];

            bool naoEstaNaSala = sala.All(i => i != idJogador);

            if (naoEstaNaSala)
                throw new Exception();

            var mensagensSaidaSala = new List<MensagemSalaServidor>();

            foreach (Guid id in sala)
            {
                var mensagemSaidaSala = new MensagemSalaServidor(
                    idSala,
                    id,
                    idJogador,
                    TipoAcaoSalaServidor.JogadorSaiu);

                mensagensSaidaSala.Add(mensagemSaidaSala);
            }

            sala.Remove(idJogador);

            return mensagensSaidaSala;
        }

        private static bool _estaNumaSala(Guid idJogador) => _salasAbertas.Values.Any(s => s.Contains(idJogador));
    }
}
