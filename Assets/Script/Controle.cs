using UnityEngine;

public static class Controle {

    public static int pontuacao = 0; // pontuaçao atual do jogador
    public static float velocidadeJogo = 1; // controla a velocidade em que o jogador corre e os obstaculos surgem
}

public class Temporizador
{
    float tempo = 0;
    public bool this[float t] {
        get {
            if ((tempo += Time.deltaTime) >= t) {
                tempo -= t;
                return true;
            } else return false;
        }
    }
}