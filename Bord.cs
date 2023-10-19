using System.Text;

public abstract class Bord
{
    public Kuiltje[,] Kuiltjes;
    public List<ThuisKuiltje> ThuisKuiltjes; // Moet miss Kuiltjes type zijn
    public int rijen;
    public int kuiltjes_per_rij;
}

public class MankalaV1Bord : Bord
{

    public MankalaV1Bord(int rijen, int kuiltjes_per_rij)
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.rijen = rijen;
        this.kuiltjes_per_rij = kuiltjes_per_rij;
        this.Kuiltjes = new Kuiltje[rijen, kuiltjes_per_rij];
        this.ThuisKuiltjes = new List<ThuisKuiltje>();
        fillBord();
    }
    private void fillBord()
    { // V1 heeft in elk Kuiltje 4 steentjes

        
        for (int kolom = 0; kolom < Kuiltjes.GetLength(0); kolom++)
        {
            for (int rij = 0; rij < Kuiltjes.GetLength(1); rij++)
            {
                if (Kuiltjes[kolom, rij].GetType() == typeof(ThuisKuiltje)) // https://stackoverflow.com/questions/3561202/check-if-instance-is-of-a-type
                {
                    Kuiltjes[kolom, rij] = new ThuisKuiltje(0, kolom + 1);
                    continue;
                }

                Kuiltjes[kolom, rij] = new BordKuiltje(4, kolom + 1);

            }
        }
    }

    public override string ToString() // Heb ik hier toegevoegd
    {
        // even kijken hoe we die 2d array willen representeren, daarnaast kijken of 
        // we het in de volgorde van links naar rechts doen, zoals men leest, of in
        // volgorde van mankala (arab style)
        StringBuilder sb = new StringBuilder();
        
        for (int rij = 0; rij < rijen; rij++)
        {
            for (int kolom = 0; kolom < kuiltjes_per_rij; kolom++)
            {
                sb.Append(Kuiltjes[kolom, rij].steentjes);
            }
        }

        return sb.ToString();
    }
}
