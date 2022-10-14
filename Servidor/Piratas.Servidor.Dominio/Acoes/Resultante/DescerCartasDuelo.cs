namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Cartas.Tipos;
    using Imediata;
    using Tipos;

    public class DescerCartasDuelo : Resultante
    {
        public List<Duelo> CartasRespostaDuelo { get; private set; }

        public DescerCartasDuelo(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
        }

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            CartasRespostaDuelo.ForEach(c => c.AplicarEfeito(this, mesa));

            var realizadorPossuiDueloSurpresa = Realizador.Mao.Possui<DueloSurpresa>();
            var alvoPossuiDueloSurpresa = Alvo.Mao.Possui<DueloSurpresa>();

            var calcularResultadoDuelo = new CalcularResultadoDuelo(this, Realizador, Alvo);

            if (!realizadorPossuiDueloSurpresa && !alvoPossuiDueloSurpresa)
                yield return calcularResultadoDuelo;

            else
            {
                mesa.RegistrarImediataAposResultantes(calcularResultadoDuelo);

                if (realizadorPossuiDueloSurpresa)
                    yield return new DescerCartasDueloSurpresa(this, Realizador);

                if (alvoPossuiDueloSurpresa)
                    yield return new DescerCartasDueloSurpresa(this, Alvo);
            }
        }
    }
}
