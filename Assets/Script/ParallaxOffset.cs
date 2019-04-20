using UnityEngine;

public class ParallaxOffset : MonoBehaviour {

    new Renderer renderer;
    [Range(0,.5f)][SerializeField] float velocidade; // velocidade de translaçao
	
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	void Update () {
        // a velocidade depende da velocidade do jogo dada pela classe de controle
        renderer.material.mainTextureOffset += Vector2.left * Time.deltaTime * Controle.velocidadeJogo * velocidade;
	}
}
