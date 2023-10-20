using System.Text;

public abstract class Bord
{
    public Kuiltje[,] Kuiltjes;
    public List<ThuisKuiltje> ThuisKuiltjes;
    protected int Rijen;
    protected int KuiltjesPerRij;
    public int AantalThuiskuiltjes; // unused
}

public class MankalaV1Bord : Bord
{
    public MankalaV1Bord(int rijen, int kuiltjesPerRij, int aantalThuiskuiltjes = 2)
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.Rijen = rijen;
        this.KuiltjesPerRij = kuiltjesPerRij;
        this.AantalThuiskuiltjes = aantalThuiskuiltjes;
        Kuiltjes = new Kuiltje[rijen, kuiltjesPerRij];
        ThuisKuiltjes = new List<ThuisKuiltje>();
        VulBord(1);
    }
    private void VulBord(int aantalSteentjes)
    { // V1 heeft in elk Kuiltje 4 Steentjes
        
        for (int rij = 0; rij < Rijen; rij++)
        {
            for (int kolom = 0; kolom < KuiltjesPerRij - 1; kolom++)
            {
                Kuiltjes[rij, kolom] = new BordKuiltje(aantalSteentjes, rij + 1);
            }
            
            ThuisKuiltje tempThuis = new ThuisKuiltje(0, rij + 1);
            Kuiltjes[rij, KuiltjesPerRij - 1] = tempThuis;
            ThuisKuiltjes.Add(tempThuis);
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        for (int rij = 0; rij < Rijen; rij++)
        {
            for (int kolom = 0; kolom < KuiltjesPerRij; kolom++)
            {
                stringBuilder.Append('|');
                stringBuilder.Append(Kuiltjes[rij, kolom].Steentjes);
                
            }

            stringBuilder.Append("|\n");
        }
        
        return stringBuilder.ToString();
    }
}
