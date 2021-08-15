namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Passivo;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa _) => 
            _aplicarEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal Resultante _aplicarEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            // TODO: Como avisar o cliente que foi um Bau Armadilha?
            (Mao maoSaqueador, Mao maoSaqueado) = maoAlvo.Possui<BauArmadilha>() ?
                (maoAlvo, maoRealizador) : (maoRealizador, maoAlvo);

            _saquear(maoSaqueador, maoAlvo);

            return null;
        }

        private void _saquear(Mao maoSaqueador, Mao maoSaqueado)
        {
            var cartaSaqueada = maoSaqueado.ObterQualquer();
            maoSaqueado.Remover(cartaSaqueada);

            maoSaqueador.Adicionar(cartaSaqueada);
        }
    }
}