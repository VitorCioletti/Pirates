namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using System;

    public abstract class BaseEstado
    {
        protected MaquinaEstados MaquinaEstados { get; set; }

        protected BaseEstado(MaquinaEstados maquinaEstados)
        {
            MaquinaEstados = maquinaEstados;
        }

        public virtual void Inicializar()
        {
        }

        public virtual BaseResultadoEstado Limpar()
        {
            return null;
        }

        public virtual void AoVoltarNoTopo(BaseResultadoEstado resultadoEstado)
        {
        }

        public virtual void AoReceberTexto(string texto)
        {
        }

        protected void Remover()
        {
            MaquinaEstados.Remover(this);
        }
    }
}
