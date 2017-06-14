using UnityEngine;

public class GeradorBlocos : MonoBehaviour
{
    public GameObject[] blocos;
    public int linhas;

	// Use this for initialization
	void Start () {
		CriarGrupoBlocos();
	}

    void CriarGrupoBlocos()
    {
        var limitesBloco = blocos[0].GetComponent<SpriteRenderer>().bounds;
        var larguraBloco = limitesBloco.size.x;
        var alturaBloco = limitesBloco.size.y;
        float larguraTela, alturaTela, multiplicadorLargura;
        int colunas;

        ColetarInformacoesBloco(larguraBloco, out larguraTela, out alturaTela, out colunas, out multiplicadorLargura);

        //Recupera o número total de blocos criados automaticamente
        GerenciadorGame.NumeroTotalBlocos = linhas * colunas;

        for (int i = 0; i < linhas; i++)
        {
            for (int j = 0; j < colunas; j++)
            {
                var blocoAleatorio = blocos[Random.Range(0, blocos.Length)];
                var blocoInstanciado = Instantiate(blocoAleatorio);
                blocoInstanciado.transform.position = new Vector3(-(larguraTela * 0.5f) + (j * larguraBloco * multiplicadorLargura), (alturaTela * 0.5f) - (i * alturaBloco), 0);
                var novaLarguraBloco = blocoInstanciado.transform.localScale.x * multiplicadorLargura;
                blocoInstanciado.transform.localScale = new Vector3(novaLarguraBloco, blocoInstanciado.transform.localScale.y, 1);
            }
        }
    }

    void ColetarInformacoesBloco(float larguraBloco, out float larguraTela, out float alturaTela, out int colunas, out float multiplicadorLargura)
    {
        //Camera principal
        Camera cam = Camera.main;
        //Largura da tela (canto inferior direito menos o canto inferior esquerdo)
        larguraTela = (cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) - cam.ScreenToWorldPoint(new Vector3(0, 0, 0))).x;
        //Altura da tela (canto superior da tela menos a parte inferior da tela)
        alturaTela = (cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)) - cam.ScreenToWorldPoint(new Vector3(0, 0, 0))).y;
        //Quantidade de colunas de blocos na tela
        colunas = (int) (larguraTela / larguraBloco);

        //multiplicadorLargura * colunas * larguraBloco = larguraTela
        //multiplicadorLargura = larguraTela / (colunas * larguraBloco)
        multiplicadorLargura = larguraTela / (colunas * larguraBloco);
    }
}
