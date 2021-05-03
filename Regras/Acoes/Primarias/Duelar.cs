namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using Regras;
    using System;
    using Tipos;

    public class Duelar: Primaria
    {
        public Jogador Vitorioso { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            mesa.EmDuelo = true;
            mesa.Duelistas = new Tuple<Jogador, Jogador>(Realizador, Alvo);
        }
    }
}