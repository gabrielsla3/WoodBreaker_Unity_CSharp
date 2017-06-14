using UnityEngine;

public class Bola : MonoBehaviour
{
    public Vector3 Direcao;
    public float Velocidade;
    public GameObject ParticulasBloco;
    public ParticleSystem ParticulaFolha;
    
	// Use this for initialization
	void Start ()
    {
		Direcao.Normalize(); //Equivalente a fazer Direcao = Direcao.normalized
    }
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += Direcao * Velocidade * Time.deltaTime;
	}

    //Função chamada quando este elemento colidir com outro. O outro elemento, junto com informações da colisão, são encontrados no parâmetro 'colisor'
    void OnCollisionEnter2D(Collision2D colisor)
    {
        var colisaoInvalida = false;
        var normal = colisor.contacts[0].normal;
        var plataforma = colisor.transform.GetComponent<Plataforma>();
        var geradorArestas = colisor.transform.GetComponent<GeradorArestas>();

        //Se existir o componente Plataforma no game object com o qual a bolinha colidiu. Realiza o controle do "Game over" conforme o vetor normal dos componentes
        if (plataforma != null)
        {
            if (normal != Vector2.up)
            {
                colisaoInvalida = true;
            }
            else
            {
                ParticulaFolha.transform.position = colisor.transform.position;
                ParticulaFolha.Play();
            }
        }
        else if (geradorArestas != null)
        {
            if (normal == Vector2.up)
            {
                colisaoInvalida = true;
            }
        }
        else //Caso cairmos no else, estamos colidindo com um bloco, pois é o que sobre de colisão
        {
            //Obtém as bordas do colisor
            var bordasColisor = colisor.transform.GetComponent<SpriteRenderer>().bounds;
            //Recupera a posição de criação e joga o foco para o centro do item, para que o efeito inicie no centro do bloco destruído
            var posicaoCriacao = new Vector3(colisor.transform.position.x + bordasColisor.extents.x, colisor.transform.position.y - bordasColisor.extents.y, colisor.transform.position.z);
            //Instância a partícula no objeto
            var particulas = Instantiate(ParticulasBloco, posicaoCriacao, Quaternion.identity);
            //Obtém o componente de partículas
            var componenteParticulas = particulas.GetComponent<ParticleSystem>();
            //Destrói as partículas após 2.3 segundos (tempo de vida de cada partícula + tempo de geração das partículas). Assim, respeita a vida da última partícula gerada.
            Destroy(particulas, componenteParticulas.main.duration + componenteParticulas.main.startLifetimeMultiplier);
            //Destrói o objeto
            Destroy(colisor.gameObject);

            //Incrementa a cada bloco destruído
            GerenciadorGame.NumeroBlocosDestruidos++;
        }

        //Se a colisão estiver "Ok", inverte a colisão
        if (!colisaoInvalida)
        {
            Direcao = Vector2.Reflect(Direcao, normal);
            Direcao.Normalize();
        }
        else
        {
            //Game over
            GerenciadorGame.Instancia.FinalizarJogo();

        }
    }
}
