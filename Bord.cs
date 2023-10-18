public abstract class Bord
{
    protected Kuiltje[,] Kuiltjes;
    protected List<ThuisKuiltje> ThuisKuiltjes; // Moet miss Kuiltjes type zijn
}

public class MankalaV1Bord : Bord
{

    public MankalaV1Bord()
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.Kuiltjes = new Kuiltje[2, 3];
    }
}