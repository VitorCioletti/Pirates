namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using Acoes.Resultantes;
    using Regras;
    using System;
    using Tipos;

    public class Duelar: Primaria
    {
        public Duelar(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            mesa.EmDuelo = true;
            mesa.Duelistas = new Tuple<Jogador, Jogador>(Realizador, Alvo);

            return new ResponderDuelo(Alvo, Realizador);
        }
    }
}