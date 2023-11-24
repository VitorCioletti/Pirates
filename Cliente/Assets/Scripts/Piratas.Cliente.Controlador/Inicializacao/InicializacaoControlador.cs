namespace Piratas.Cliente.Controller.Bootstrapper
{
    using Cliente.Bootstrapper;
    using UnityEngine;

    public class InicializacaoControlador : MonoBehaviour
    {
        private void Start()
        {
            _boot();
        }

        private void _boot()
        {
            Inicializacao.Boot();
        }
    }
}
