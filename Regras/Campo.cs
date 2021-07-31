namespace ServidorPiratas.Regras
{
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

        public Canhao Canhao { get ; set; }

        public List<Tripulacao> Tripulacao { get; private set; } // TODO: Privado?

        public Embarcacao Embarcacao { get; private set; } // TODO: Privado?

        public List<Carta> Protegidas { get; private set; }

        public int CalcularPontosDuelo()
        {
            var pontosDuelo = Tripulacao.Sum(t => t.Tiros);

            pontosDuelo += Canhao != null ? Canhao.Tiros : 0;

            if (Embarcacao.GetType() == typeof(GuerrilhaNaval))
                pontosDuelo += ((GuerrilhaNaval)Embarcacao).TirosAdicionais;

            if (Embarcacao.GetType() == typeof(OuricoInfernal))
                pontosDuelo += ((OuricoInfernal)Embarcacao).Tiros;

            return pontosDuelo;
        }

        public void DanificarEmbarcacao()
        {
            Embarcacao.Danificar(_danoEmbarcacao);

            if (Embarcacao.Vida == 0)
               RemoverEmbarcacao(); 
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

        public void AfogarTripulacao() => Tripulacao.RemoveAll(t => t.Afogavel);

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

        public void AdicionarProtegida(Carta carta)
        {
            Protegidas.Add(carta);
        }

        public List<Carta> ObterTodasProtegidas() => Protegidas.ToList();

        public List<Carta> RemoverTodasProtegidas()
        {
            var protegidas = ObterTodasProtegidas(); 

            Protegidas = null;

            return protegidas;
        }
    }
}