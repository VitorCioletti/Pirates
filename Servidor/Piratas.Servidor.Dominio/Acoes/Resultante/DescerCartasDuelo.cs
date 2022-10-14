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

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            var acoesResultantes = new List<Acao> { };

            CartasRespostaDuelo.ForEach(c => c.AplicarEfeito(this, mesa));

            var realizadorPossuiDueloSurpresa = Realizador.Mao.Possui<DueloSurpresa>();
            var alvoPossuiDueloSurpresa = Alvo.Mao.Possui<DueloSurpresa>();

            var calcularResultadoDuelo = new CalcularResultadoDuelo(this, Realizador, Alvo);

            if (!realizadorPossuiDueloSurpresa && !alvoPossuiDueloSurpresa)
                acoesResultantes.Add(calcularResultadoDuelo);

            else
            {
                mesa.RegistrarImediataAposResultantes(calcularResultadoDuelo);

                if (realizadorPossuiDueloSurpresa)
                {
                    var descerCartasDueloSurpresa = new DescerCartasDueloSurpresa(this, Realizador);

                    acoesResultantes.Add(descerCartasDueloSurpresa);
                }

                if (alvoPossuiDueloSurpresa)
                {
                    var descerCartasDueloSurpresa = new DescerCartasDueloSurpresa(this, Alvo);

                    acoesResultantes.Add(descerCartasDueloSurpresa);
                }
            }

            return acoesResultantes;
        }
    }
}
