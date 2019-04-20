using UnityEngine;

public class GeradorObstaculos : MonoBehaviour {

    [SerializeField] float tempoOcioso = 3f;
    Temporizador temporizadorObstaculo = new Temporizador();
    //Temporizador temporizadorPowerup = new Temporizador();

    [SerializeField] GameObject[] inimigoPrefab;

	// Update is called once per frame
	void Update () {
        if (temporizadorObstaculo[tempoOcioso/Controle.velocidadeJogo])
            Instantiate(inimigoPrefab[Random.Range(0,inimigoPrefab.Length)], transform.position, Quaternion.identity, transform);

        //if (temporizadorPowerup[tempoOcioso * 5]) return; // cria powerapu

        // aumenta a velocidade do jogo ao passar do tempo
        Controle.velocidadeJogo += Time.deltaTime * 0.01f;
    }
}
