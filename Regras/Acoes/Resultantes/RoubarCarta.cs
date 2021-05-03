namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System;
    using Tipos;

    public class RoubarCarta: Resultante
    {
        public string IdAcaoOrigem { get; private set; }

        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            if (Alvo.Mao.Remove(CartaRoubada))
                Realizador.Mao.Add(CartaRoubada);
            else
                throw new Exception(
                    $"Carta \"{CartaRoubada.Nome}\" não está na mão do jogador \"{Alvo.Id}\".");
        }
    }
}