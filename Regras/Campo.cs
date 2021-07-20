namespace ServidorPiratas.Regras
{
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class Campo
    {
        private int _tripulacaoMaxima = 2;

        public Duelo Canhao { get ; set; }

        public List<Tripulacao> Tripulacao { get; private set; }

        public Embarcacao Embarcacao { get; private set; }

        public int CalcularPontosDuelo()
        {
            var pontosDuelo = Tripulacao.Sum(t => t.Tiros);

            pontosDuelo += Canhao != null ? Canhao.Tiros : 0;

            return pontosDuelo;
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

        public void Remover(Tripulacao tripulacao)
        {
            if (Tripulacao.Count == 0)
                throw new Exception("Tripulação vazia.");
 
            if (Tripulacao.FirstOrDefault(t => t == tripulacao) == null)
                throw new Exception("Tripulação não encontrada.");

            Tripulacao.Remove(tripulacao);
        }

        public void AfogarTripulacao() => Tripulacao.RemoveAll(t => t.PermiteAfogamento);

        public void RemoverEmbarcacao()
        {
            if (Embarcacao == null)
                throw new Exception("Não há embarcação no campo.");

           Embarcacao = null; 
        }

        public void TrocarEmbarcacao(Embarcacao embarcacao)
        {
            RemoverEmbarcacao();
            Adicionar(embarcacao);
        }
    }
}