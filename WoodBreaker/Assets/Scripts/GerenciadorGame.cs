using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorGame : MonoBehaviour
{
    public static int NumeroTotalBlocos;
    public static int NumeroBlocosDestruidos;
    public Image Estrelas;
    public GameObject Canvas;
    public static GerenciadorGame Instancia;
    public GameObject Bola;
    public Plataforma Plataforma;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        //Chama somente na cena do jogo (1)
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Canvas.SetActive(false);
            NumeroBlocosDestruidos = 0;
        }
    }

    /// <summary>
    /// Finaliza o jogo, mostrando o resultado.
    /// </summary>
    public void FinalizarJogo()
    {
        Canvas.SetActive(true);

        //Cast para float para que os valores decimais sejam considerados, pois retornar 0 ou 1 não iria alterar o preenchimento das estrelas.
        Estrelas.fillAmount = (float)NumeroBlocosDestruidos / (float)NumeroTotalBlocos;
        //Desabilita a movimentação da plataforma
        Plataforma.enabled = false;
        //Destrói a maça (bola)
        Destroy(Bola.gameObject);
    }

    /// <summary>
    /// Jogar novamente o jogo
    /// </summary>
    public void JogarNovamente()
    {
        SceneManager.LoadScene("main");
    }

    /// <summary>
    /// Método genérico para chamar qualquer cena do jogo
    /// </summary>
    public void AlterarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    /// <summary>
    /// Fecha o aplicativo
    /// </summary>
    public void FecharAplicativo()
    {
        Application.Quit();
    }
}
