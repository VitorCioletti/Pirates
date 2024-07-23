namespace Piratas.Protocolo.Partida
{
    public abstract class BaseEscolha
    {
        public TipoEscolha Tipo { get; private set; }

        protected BaseEscolha(TipoEscolha tipo)
        {
            Tipo = tipo;
        }
    }
}
