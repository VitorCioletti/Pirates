namespace Piratas.Cliente.Controller.Bootstrapper
{
    using Cliente.Bootstrapper;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class InicializacaoControlador : MonoBehaviour
    {
        private void Start()
        {
            Inicializacao.Inicializar();

            SceneManager.LoadScene("Home");
        }
    }
}
