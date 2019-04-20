using UnityEngine;

public class Bala : MonoBehaviour {

    [SerializeField] float velocidade = 2; // velocidade da bala
    public int dano = 1; // dano a causar em inimigos
	
	void Update () {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime); // desloca para a direita
        if (transform.position.x > 2.5f) Destroy(gameObject); // destroi de sair da tela
	}
}
