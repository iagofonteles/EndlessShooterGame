using UnityEngine;

public class Inimigo : Obstaculo {

    private int vida = 1;

    public override void SofrerDano(int dano) {
        vida -= dano;
        if (vida < 1) { // se acabar a vida do inimigo
            Destroy(gameObject); // destroi o inimigo
            Controle.pontuacao += 5; // aumenta pontuaçao
        }
    }

    public override void ColidirJogador(bool causouDano) {
        if(causouDano) GetComponentInChildren<Animator>().Play("anim_inimigo_ataque");
    }
}
