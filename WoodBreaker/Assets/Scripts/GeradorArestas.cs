using UnityEngine;

public class GeradorArestas : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        //Câmera principal do jogo
	    var cam = Camera.main;
        //Recupera o colisor no mesmo objeto anexado
	    var colisor = GetComponent<EdgeCollider2D>();

        //Recupera os cantos da tela
	    var cantoInferiorEsquerdo = cam.ScreenToWorldPoint(new Vector3(0, 0));
	    var cantoSuperiorEsquerdo = cam.ScreenToWorldPoint(new Vector3(0, Screen.height));
	    var cantoSuperiorDireito = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
	    var cantoInferiorDireito = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0));

        //"Percorre" a tela para delimitar os cantos
        colisor.points = new Vector2[]
        {
            cantoInferiorEsquerdo,
            cantoSuperiorEsquerdo,
            cantoSuperiorDireito,
            cantoInferiorDireito,
            cantoInferiorEsquerdo
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
