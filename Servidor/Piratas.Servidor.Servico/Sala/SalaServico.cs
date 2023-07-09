namespace Piratas.Servidor.Servico.Sala
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Excecoes.Sala;
    using Partida;

    public static class SalaServico
    {
        private static Dictionary<Guid, List<string>> _salasAbertas { get; }

        static SalaServico()
        {
            _salasAbertas = new Dictionary<Guid, List<string>>();
        }

        public static Guid Criar(string idJogadorCriador)
        {
            bool estaNumaSala = _estaNumaSala(idJogadorCriador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaExcecao(idJogadorCriador);

            var idNovaSala = Guid.NewGuid();

            return idNovaSala;
        }

        public static Guid Sair(string idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idSala == Guid.Empty)
                throw new JogadorNaoEstaEmNenhumaSala(idJogador);

            List<string> sala = _salasAbertas[idSala];

            sala.Remove(idJogador);

            return idSala;
        }

        public static void Entrar(string idJogador, Guid idSala)
        {
            bool estaNumaSala = _estaNumaSala(idJogador);

            if (estaNumaSala)
                throw new JogadorJaEstaNumaSalaExcecao(idJogador);

            bool salaNaoExiste = !_salasAbertas.ContainsKey(idSala);

            if (salaNaoExiste)
                throw new SalaNaoEncontradaExcecao(idSala);

            _salasAbertas[idSala].Add(idJogador);
        }

        public static Guid IniciarPartida(string idJogador)
        {
            Guid idSala = _salasAbertas.FirstOrDefault(s => s.Value.Contains(idJogador)).Key;

            if (idSala == Guid.Empty)
                throw new JogadorNaoEstaEmNenhumaSala(idJogador);

            List<string> idsJogadores = _salasAbertas[idSala];

            Guid idPartida = GerenciadorPartidaServico.CriarPartida(idsJogadores);

            _salasAbertas.Remove(idSala);

            return idPartida;
        }

        private static bool _estaNumaSala(string idJogador) => _salasAbertas.Values.Any(s => s.Contains(idJogador));
    }
}
