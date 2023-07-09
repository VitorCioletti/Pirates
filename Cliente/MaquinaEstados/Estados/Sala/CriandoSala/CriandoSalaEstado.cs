namespace Piratas.Cliente.MaquinaEstados.Estados.Sala.CriandoSala
{
    using System;
    using Servicos;

    public class CriandoSalaEstado : BaseEstado
    {
        public CriandoSalaEstado(MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
        }

        public override void Inicializar()
        {
            Console.WriteLine("Criando sala...");

            SalaServico.CriarSala();
        }

        public override BaseResultadoEstado Limpar()
        {
            Console.Clear();

            return new CriandoSalaResultadoEstado(null);
        }
    }
}
