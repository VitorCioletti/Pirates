namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Cartas.Duelo;
    using Cartas.Embarcacao;
    using Cartas.Tipos;
    using Excecoes.Campo;

    public class Campo
    {
        private readonly int _danoEmbarcacao = 1;

        private readonly int _tripulacaoMaxima = 2;

        public List<Canhao> Canhoes { get; private set; }

        public List<DueloSurpresa> DuelosSurpresa { get; private set; }

        public List<Carta> Protegidas { get; private set; }

        public List<Tripulante> Tripulacao { get; private set; }

        public Embarcacao Embarcacao { get; private set; }
        public event Action<List<Carta>> AoRemoverProtegidas;

        public Campo()
        {
            Canhoes = new List<Canhao>();
            DuelosSurpresa = new List<DueloSurpresa>();
            Protegidas = new List<Carta>();
            Tripulacao = new List<Tripulante>();

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

        public void Adicionar(Tripulante tripulante)
        {
            if (Tripulacao.Count >= _tripulacaoMaxima)
                throw new TripulacaoCheiaException();

            Tripulacao.Add(tripulante);
        }

        public void Adicionar(Embarcacao embarcacao)
        {
            if (Embarcacao != null)
                throw new ExisteEmbarcacaoException();

            Embarcacao = embarcacao;
        }

        public void Adicionar(List<Canhao> canhoes) => canhoes.ForEach(Adicionar);

        public void Adicionar(Canhao canhao) => Canhoes.Add(canhao);

        public void Adicionar(List<DueloSurpresa> duelosSurpresa) => duelosSurpresa.ForEach(Adicionar);

        public void Adicionar(DueloSurpresa dueloSurpresa) => DuelosSurpresa.Add(dueloSurpresa);

        public void Remover(Tripulante tripulante)
        {
            if (Tripulacao.Count == 0)
                throw new TripulacaoVaziaException();

            if (Tripulacao.FirstOrDefault(t => t == tripulante) == null)
                throw new TripulanteNaoEncontradoException();

            Tripulacao.Remove(tripulante);
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

        public void AdicionarProtegida(Carta carta) => Protegidas.Add(carta);

        public List<Carta> ObterTodasProtegidas() => Protegidas.ToList();

        private void _removerEmbarcacao()
        {
            if (Embarcacao == null)
                throw new SemEmbarcacaoException();

            Embarcacao = null;

            _removerTodasProtegidas();
        }

        private List<Carta> _removerTodasProtegidas()
        {
            var protegidas = ObterTodasProtegidas();

            Protegidas = null;

            AoRemoverProtegidas?.Invoke(protegidas);

            return protegidas;
        }

        private int _calcularTirosCanhoes() => Canhoes.Sum(c => c.Tiros);

        private int _calcularTirosDueloSurpresa() => DuelosSurpresa.Sum(d => d.Tiros);

        private int _calcularTirosTripulacao() => Tripulacao.Sum(t => t.Tiros);

        private int _calcularTirosEmbarcacao()
        {
            var tiros = 0;

            if (Embarcacao is GuerrilhaNaval guerrilhaNaval)
                tiros += guerrilhaNaval.TirosAdicionais * Canhoes.Count;

            else if (Embarcacao is OuricoInfernal ouricoInfernal)
            {
                if (tiros != 0)
                    tiros += ouricoInfernal.Tiros;
            }

            return tiros;
        }
    }
}
