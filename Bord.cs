using System.Text;

public abstract class Bord
{
    public Kuiltje[,] Kuiltjes;
    public List<ThuisKuiltje> ThuisKuiltjes; // Moet miss Kuiltjes type zijn
    public int rijen;
    public int kuiltjes_per_rij;
    public int aantal_thuiskuiltjes;
}

public class MankalaV1Bord : Bord
{

    public MankalaV1Bord(int rijen, int kuiltjes_per_rij, int aantal_thuiskuiltjes = 2)
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.rijen = rijen;
        this.kuiltjes_per_rij = kuiltjes_per_rij;
        this.aantal_thuiskuiltjes = aantal_thuiskuiltjes;
        Kuiltjes = new Kuiltje[rijen, kuiltjes_per_rij];
        ThuisKuiltjes = new List<ThuisKuiltje>();
        fillBord(4);
    }
    private void fillBord(int aantal_steentjes)
    { // V1 heeft in elk Kuiltje 4 steentjes
        
        for (int rij = 0; rij < rijen; rij++)
        {
            for (int kolom = 0; kolom < kuiltjes_per_rij - 1; kolom++)
            {
                Kuiltjes[rij, kolom] = new BordKuiltje(aantal_steentjes, rij + 1);
            }
            
            ThuisKuiltje tempThuis = new ThuisKuiltje(0, rij + 1);
            Kuiltjes[rij, kuiltjes_per_rij - 1] = tempThuis;
            ThuisKuiltjes.Append(tempThuis);

        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        
        for (int rij = 0; rij < rijen; rij++)
        {
            for (int kolom = 0; kolom < kuiltjes_per_rij; kolom++)
            {
                sb.Append("|");
                sb.Append(Kuiltjes[rij, kolom].steentjes);
                
            }

            sb.Append("|\n");
        }
        
        return sb.ToString();
    }
}
