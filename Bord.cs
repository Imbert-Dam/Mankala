public abstract class Bord
{
    public Kuiltje[,] Kuiltjes;
    public List<ThuisKuiltje> ThuisKuiltjes; // Moet miss Kuiltjes type zijn
}

public class MankalaV1Bord : Bord
{

    public MankalaV1Bord()
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.Kuiltjes = new Kuiltje[2, 3];
        this.ThuisKuiltjes = new List<ThuisKuiltje>();
    }
}