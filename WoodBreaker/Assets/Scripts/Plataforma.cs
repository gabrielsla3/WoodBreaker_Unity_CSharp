using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float velocidadeMovimento;
    public float limiteX;

	// Use this for initialization
	void Start ()
	{
	    limiteX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<SpriteRenderer>().bounds.extents.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float direcaoHorizontalMouse = Input.GetAxis("Mouse X"); // -1 = para esquerda; 0 = parado; 1 = para direita;

        //Velocidade de movimento = Velocidade padrão para controlar o movimento.
        //Time.deltaTime = Multiplica pelo tempo do último frame, para regular a velocidade do jogo, independente da potência do computador.
        GetComponent<Transform>().position += Vector3.right * direcaoHorizontalMouse * velocidadeMovimento * Time.deltaTime;

        //Controla o limite da plataforma na tela
        var xAtual = transform.position.x;
	    xAtual = Mathf.Clamp(xAtual, -limiteX, limiteX);
        transform.position = new Vector3(xAtual, transform.position.y);
	}
}
