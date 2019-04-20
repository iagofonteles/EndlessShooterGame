using UnityEngine;

public class CenaGameOver : MonoBehaviour {

    private void OnGUI() {

        GUI.Box(new Rect(0, 0, 256, 64), "Ultima Pontuaçao: " + PlayerPrefs.GetInt("ultima pontuaçao").ToString()+
            "\nPontuaçao Maxima: "+PlayerPrefs.GetInt("pontuaçao maxima").ToString());

        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), "Jogar Novamente"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("cena_jogo");
    }
}
