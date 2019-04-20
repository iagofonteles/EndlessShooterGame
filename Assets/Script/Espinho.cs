
public class Espinho : Obstaculo {

    private bool foiTocado = false;

    public override void SairDaTela() {
        if(!foiTocado) Controle.pontuacao += 3; // adiciona pontos por passar por um obstaculo sem toca-lo
        base.SairDaTela();
    }

    public override void ColidirJogador(bool causouDano) {
        if (causouDano) foiTocado = true;
    }
}
