namespace Piratas.Servidor.Regras
{
    using Cartas.Duelo;
    using Cartas.Embarcacao;
    using Cartas.Tipos;
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class Campo
    {
        private int _danoEmbarcacao = 1;
 
        private int _tripulacaoMaxima = 2;

        public List<Canhao> Canhoes { get; private set; } // TODO: Privado?

        public List<DueloSurpresa> DuelosSurpresa { get; private set; } // TODO: Privado?

        public List<Carta> Protegidas { get; private set; } // TODO: Privado?

        public List<Tripulacao> Tripulacao { get; private set; } // TODO: Privado?

        public Embarcacao Embarcacao { get; private set; } // TODO: Privado?


        public event Action<List<Carta>> AoRemoverProtegidas;

        public Campo()
        {
            Canhoes = new List<Canhao>();
            DuelosSurpresa = new List<DueloSurpresa>();
            Protegidas = new List<Carta>();
            Tripulacao = new List<Tripulacao>();

            Embarcacao = null;
        }

        public int CalcularPontosDuelo()
        {
            var pontosDuelo = 0;

            pontosDuelo += _calcularTirosCanhoes();
            pontosDuelo += _calcularTirosDueloSurpresa();
            pontosDuelo += _calcularTirosEmbarcacao();
            pontosDuelo += _calcularTirosTripulacao();

            return pontosDuelo;
        }

        public void DanificarEmbarcacao()
        {
            if (Embarcacao == null)
                return;

            Embarcacao.Danificar(_danoEmbarcacao);

            if (Embarcacao.Vida == 0)
               _removerEmbarcacao(); 
        }

        public void Adicionar(Tripulacao tripulacao)
        {
            if (Tripulacao.Count >= _tripulacaoMaxima) 
                throw new Exception("Tripulação do jogador está cheia.");

            Tripulacao.Add(tripulacao);
        }

        public void Adicionar(Embarcacao embarcacao)
        {
            if (Embarcacao != null)
                throw new Exception("Jogador já possui uma embarcação.");

            Embarcacao = embarcacao;
        }

        public void Adicionar(List<Canhao> canhoes) => canhoes.ForEach(c => Adicionar(c));

        public void Adicionar(Canhao canhao) => Canhoes.Add(canhao);

        public void Adicionar(List<DueloSurpresa> duelosSurpresa) => duelosSurpresa.ForEach(d => Adicionar(d));

        public void Adicionar(DueloSurpresa dueloSurpresa) => DuelosSurpresa.Add(dueloSurpresa);

        public void Remover(Tripulacao tripulacao)
        {
            if (Tripulacao.Count == 0)
                throw new Exception("Tripulação vazia.");
 
            if (Tripulacao.FirstOrDefault(t => t == tripulacao) == null)
                throw new Exception("Tripulação não encontrada.");

            Tripulacao.Remove(tripulacao);
        }

        public void AfogarTripulacao() => Tripulacao.RemoveAll(t => t.Afogavel);

        public void RemoverCartasDuelo()
        {
            Canhoes.Clear();
            DuelosSurpresa.Clear();
        }

        public bool TripulacaoCheia() => Tripulacao.Count == _tripulacaoMaxima;

        public void TrocarEmbarcacao(Embarcacao embarcacao)
        {
            _removerEmbarcacao();
            Adicionar(embarcacao);
        }

        public void AdicionarProtegida(Carta carta)
        {
            Protegidas.Add(carta);
        }

        public List<Carta> ObterTodasProtegidas() => Protegidas.ToList();

        private void _removerEmbarcacao()
        {
            if (Embarcacao == null)
                throw new Exception("Não há embarcação no campo.");
 
            Embarcacao = null; 

            _removerTodasProtegidas();
        }

        private List<Carta> _removerTodasProtegidas()
        {
            var protegidas = ObterTodasProtegidas(); 

            Protegidas = null;

            AoRemoverProtegidas(protegidas);

            return protegidas;
        }

        private int _calcularTirosCanhoes() => Canhoes.Sum(c => c.Tiros);

        private int _calcularTirosDueloSurpresa() => DuelosSurpresa.Sum(d => d.Tiros);

        private int _calcularTirosTripulacao() =>  Tripulacao.Sum(t => t.Tiros);

        private int _calcularTirosEmbarcacao()
        {
            var tiros = 0;

            if (Embarcacao is GuerrilhaNaval)
                tiros += ((GuerrilhaNaval)Embarcacao).TirosAdicionais * Canhoes.Count;

            else if (Embarcacao is OuricoInfernal)
            {
                if (tiros > 0)
                    tiros += ((OuricoInfernal)Embarcacao).Tiros;
            }

            return tiros;
        }
    }
}