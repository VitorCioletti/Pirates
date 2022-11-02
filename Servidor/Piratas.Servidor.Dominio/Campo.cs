namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Cartas.Duelo;
    using Cartas.Embarcacao;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Excecoes.Campo;

    public class Campo
    {
        private readonly int _danoEmbarcacao = 1;

        private readonly int _tripulacaoMaxima = 2;

        public List<Canhao> Canhoes { get; private set; }

        public List<DueloSurpresa> DuelosSurpresa { get; private set; }

        public List<Carta> Protegidas { get; private set; }

        public List<BaseTripulante> Tripulacao { get; private set; }

        public BaseEmbarcacao BaseEmbarcacao { get; private set; }

        public event Action<Carta> AoAdicionar;

        public event Action<Carta> AoRemover;

        public Campo()
        {
            Canhoes = new List<Canhao>();
            DuelosSurpresa = new List<DueloSurpresa>();
            Protegidas = new List<Carta>();
            Tripulacao = new List<BaseTripulante>();

            BaseEmbarcacao = null;
        }

        public int CalcularPontosDuelo()
        {
            var pontosDuelo = 0;

            pontosDuelo += _calcularTirosCanhoes();
            pontosDuelo += _calcularTirosDueloSurpresa();
            pontosDuelo += _calcularTirosTripulacao();
            pontosDuelo += _calcularTirosEmbarcacao(pontosDuelo);

            return pontosDuelo;
        }

        public void DanificarEmbarcacao()
        {
            if (BaseEmbarcacao == null)
                return;

            BaseEmbarcacao.Danificar(_danoEmbarcacao);

            if (BaseEmbarcacao.Vida == 0)
                _removerEmbarcacao();
        }

        public void Adicionar(BaseTripulante baseTripulante)
        {
            if (Tripulacao.Count >= _tripulacaoMaxima)
                throw new TripulacaoCheiaExcecao();

            Tripulacao.Add(baseTripulante);

            AoAdicionar?.Invoke(baseTripulante);
        }

        public void Adicionar(BaseEmbarcacao baseEmbarcacao)
        {
            if (BaseEmbarcacao != null)
                throw new ExisteEmbarcacaoExcecao();

            BaseEmbarcacao = baseEmbarcacao;

            AoAdicionar?.Invoke(baseEmbarcacao);
        }

        public void Adicionar(List<Canhao> canhoes) => canhoes.ForEach(Adicionar);

        public void Adicionar(Canhao canhao)
        {
            Canhoes.Add(canhao);

            AoAdicionar?.Invoke(canhao);
        }

        public void Remover(Canhao canhao)
        {
            Canhoes.Remove(canhao);

            AoRemover?.Invoke(canhao);
        }

        public void Adicionar(List<DueloSurpresa> duelosSurpresa) => duelosSurpresa.ForEach(Adicionar);

        public void Adicionar(DueloSurpresa dueloSurpresa)
        {
            DuelosSurpresa.Add(dueloSurpresa);

            AoAdicionar?.Invoke(dueloSurpresa);
        }

        public void Remover(BaseTripulante baseTripulante)
        {
            if (Tripulacao.Count == 0)
                throw new TripulacaoVaziaExcecao();

            if (Tripulacao.FirstOrDefault(t => t == baseTripulante) == null)
                throw new TripulanteNaoEncontradoExcecao();

            Tripulacao.Remove(baseTripulante);

            AoRemover?.Invoke(baseTripulante);
        }

        public void AfogarTripulacao()
        {
            foreach (BaseTripulante tripulante in Tripulacao)
            {
                if (tripulante.Afogavel)
                    Remover(tripulante);
            }
        }

        public void RemoverCartasDuelo()
        {
            Canhoes.Clear();
            DuelosSurpresa.Clear();
        }

        public bool TripulacaoCheia() => Tripulacao.Count == _tripulacaoMaxima;

        public void TrocarEmbarcacao(BaseEmbarcacao baseEmbarcacao)
        {
            _removerEmbarcacao();
            Adicionar(baseEmbarcacao);
        }

        public void AdicionarProtegida(Carta carta) => Protegidas.Add(carta);

        public List<Carta> ObterTodasProtegidas() => Protegidas.ToList();

        private void _removerEmbarcacao()
        {
            if (BaseEmbarcacao == null)
                throw new SemEmbarcacaoExcecao();

            _removerTodasProtegidas();

            AoRemover?.Invoke(BaseEmbarcacao);

            BaseEmbarcacao = null;
        }

        private void _removerTodasProtegidas()
        {
            var protegidas = ObterTodasProtegidas();

            Protegidas = null;

            foreach (Carta protegida in protegidas)
                AoRemover?.Invoke(protegida);
        }

        private int _calcularTirosCanhoes() => Canhoes.Sum(c => c.Tiros);

        private int _calcularTirosDueloSurpresa() => DuelosSurpresa.Sum(d => d.Tiros);

        private int _calcularTirosTripulacao() => Tripulacao.Sum(t => t.Tiros);

        private int _calcularTirosEmbarcacao(int tiros)
        {
            if (BaseEmbarcacao is GuerrilhaNaval guerrilhaNaval)
                tiros += guerrilhaNaval.TirosAdicionais * Canhoes.Count;

            else if (BaseEmbarcacao is OuricoInfernal ouricoInfernal)
            {
                if (tiros != 0)
                    tiros += ouricoInfernal.Tiros;
            }

            return tiros;
        }
    }
}
