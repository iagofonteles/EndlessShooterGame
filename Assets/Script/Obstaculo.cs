using UnityEngine;

public class Obstaculo : MonoBehaviour {

    private float velocidade = 2;

    void Update() {
        transform.Translate(Vector2.left * velocidade * Controle.velocidadeJogo * Time.deltaTime); // desloca para a esquerda
        if (transform.position.x < -15) {
            SairDaTela(); // se sair da tela
            Destroy(gameObject); // destroi essa instancia
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var jogador = collision.gameObject.GetComponent<Jogador>();
        if (jogador != null) { // se colidir com o jogador
            ColidirJogador(jogador.SofrerDano()); // tenta causar dano no jogador, e executa evento de colisao
            gameObject.layer = LayerMask.NameToLayer("inimigo_colidiu"); // muda de layer... pra n arrastar o jogador
            GetComponentInChildren<Renderer>().material.color = Color.gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="bala") { // ao colidir com uma bala
            SofrerDano(collision.GetComponent<Bala>().dano); // sofre o dano da bala
            Destroy(collision.gameObject); // destroi a bala
        }

    }

    public virtual void SofrerDano(int dano) {} // executar as ser atingido por uma bala
    public virtual void SairDaTela() {} // executar ao sair do campo de visao
    public virtual void ColidirJogador(bool causouDano) {} // executar ao colidir com o jogador
    
}
