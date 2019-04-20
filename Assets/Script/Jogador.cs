using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]

public class Jogador : MonoBehaviour {

    public int vida = 3; // numero de vidas
    public int Vida { get { return vida; } }
    float tempoInvencivel = 0; // tempo invencivel restante

    // pulo
    float forcaPulo = 6; // força do pulo
    bool pulouNoAr = false; // ja pulou a segunda vez?
    bool noChao = false; // se o personagem esta atualmente no chao
    // arma
    public bool powerupArma = false; // se a arma esta mais forte (2 de dano)
    [SerializeField] GameObject balaPrefab; // objeto da bala para ser instanciado

    // deslizamento
    private bool estaDeslizando = false; // se o jogador esta atualmente deslizando
    float tempoDeslizamento = 1.35f; // duraçao do deslizamento
    private Temporizador temporizadorDeslize = new Temporizador(); // variavel de controle do tempo de deslizamento

    Texture2D[] iconeCoracao = new Texture2D[2]; // icone da vida
    //Texture2D[] iconePowerup = new Texture2D[2]; // 
    AudioClip clip_atirar, clip_pular; // efeitos sonoros
    private AudioSource audios;

    void Start () {
        iconeCoracao[0] = Resources.Load<Texture2D>("spr_coracao_vazio");
        iconeCoracao[1] = Resources.Load<Texture2D>("spr_coracao_cheio");
        Random.InitState(Random.Range(-1000000, 1000000));
        clip_atirar = Resources.Load<AudioClip>("fx/clip_atirar");
        clip_pular = Resources.Load<AudioClip>("fx/clip_pular");
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        tempoInvencivel -= Time.deltaTime; // diminui o tempo restante de invencibilidade
        if (tempoInvencivel > 0) {
            transform.transform.GetChild(0).gameObject.SetActive(((int)(tempoInvencivel * 10) % 3) != 0); // pisca quando invencivel
        } else transform.GetChild(0).gameObject.SetActive(true);

        if (estaDeslizando) {
            if (temporizadorDeslize[tempoDeslizamento]) { // ao passar certo tempo deslizando
                estaDeslizando = false; // para de deslizar
                GetComponent<BoxCollider2D>().size = new Vector2(1, 2); // aumenta novamente a caixa de colisao
                transform.localScale = new Vector3(1, 1, 1);
            }
        } else { // se nao estiver deslizando pode pular deslizar e atirar
            if (Input.GetKeyDown(KeyCode.W)) {// pular na tecla W
                if (noChao) { // se esta tocando o chao
                    pulouNoAr = false; // reseta pulo no ar
                    noChao = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * forcaPulo;
                    audios.PlayOneShot(clip_pular);
                } else if (!pulouNoAr) { // se nao tiver pulado no ar
                    pulouNoAr = true; // gasta pulo no ar
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * forcaPulo / 2;
                    audios.PlayOneShot(clip_pular);
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && noChao) { // deslizar na tecla S, se estiver no chao
                estaDeslizando = true;
                GetComponent<BoxCollider2D>().size = new Vector2(1, 1); // diminui a caixa de colisao
                transform.localScale = new Vector3(1, .5f, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space)) { // atirar na tecla espaço
                audios.PlayOneShot(clip_atirar,.6f);
                GameObject go = Instantiate(balaPrefab, transform.GetChild(0).FindChild("arma").position, Quaternion.identity) as GameObject;
                go.GetComponent<Bala>().dano = powerupArma ? 2 : 1; // bala causa 1 de dano sem powerup, 2 caso contrario
            }
        }
    }

    public bool SofrerDano() {
        if (tempoInvencivel < 0) { // se nao estiver invencivel
            vida -= 1; // diminui vida
            tempoInvencivel = 2; // 2 segundos invencivel apos ser atingido
            Controle.pontuacao -= 1; // diminui pontuaçao
            if (vida < 1) GameOver(); // sem mais vidas, game over
            return true; // jogador sofreu dano
        }
        return false; // nao sofreu dano
    }

    private void OnCollisionEnter2D(Collision2D coll) { 
        if (coll.gameObject.tag == "chao") noChao = true; // checha se esta no chao
    }

    private void OnGUI()
    {
        GUI.skin.box.fontSize = 24;
        GUI.skin.button.fontSize = 32;

        var r = new Rect(0, 0, 32, 32);
        for (var i = 0; i < 3; i++) { 
            GUI.DrawTexture(r, iconeCoracao[i < vida ? 1 : 0]); // desenha os coraçoes na tela
            r.x += 32;
        }
        // desenha powerups
        GUI.Box(new Rect(Screen.width-256, 0, 256, 32), Controle.pontuacao.ToString()); // desenha a pontuaçao
    }

    private void GameOver() {
        PlayerPrefs.SetInt("pontuaçao maxima", Mathf.Max(Controle.pontuacao, PlayerPrefs.GetInt("pontuaçao maxima")));
        PlayerPrefs.SetInt("ultima pontuaçao", Controle.pontuacao); // grava a ultima pontuaçao
        Controle.pontuacao = 0;
        Controle.velocidadeJogo = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene("cena_gameover"); // vai para cena de game over
    }

}
