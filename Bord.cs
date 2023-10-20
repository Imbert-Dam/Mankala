using System.Text;

public abstract class Bord
{
    public Kuiltje[,] Kuiltjes;
    public List<ThuisKuiltje> ThuisKuiltjes;
    protected int Rijen;
    protected int KuiltjesPerRij;
    public int AantalThuiskuiltjes; // Ongebruikt
}

public class MankalaV1Bord : Bord
{
    public MankalaV1Bord(int rijen, int kuiltjesPerRij, int aantalThuiskuiltjes = 2)
    {
        // Maak bord met bepaalde hvh kuiltjes -> MankalaV1 heeft dus deze opties
        this.Rijen = rijen;
        this.KuiltjesPerRij = kuiltjesPerRij;
        this.AantalThuiskuiltjes = aantalThuiskuiltjes;
        Kuiltjes = new Kuiltje[rijen, kuiltjesPerRij];
        ThuisKuiltjes = new List<ThuisKuiltje>();
        VulBord(4);
    }
    private void VulBord(int aantalSteentjes)
    { 
        // V1 heeft in elk Kuiltje 4 Steentjes
        // Kan niet niet-uniform verdeeld worden -> kan miss later met een 2D int array van te voren te definieren
        for (int rij = 0; rij < Rijen; rij++)
        {
            for (int kolom = 0; kolom < KuiltjesPerRij - 1; kolom++)
            {
                Kuiltjes[rij, kolom] = new BordKuiltje(aantalSteentjes, rij + 1);
            }
            
            ThuisKuiltje tempThuis = new ThuisKuiltje(0, rij + 1);
            Kuiltjes[rij, KuiltjesPerRij - 1] = tempThuis;
            ThuisKuiltjes.Add(tempThuis);
            
            // ThuisKuiltjes zullen zich per definitie aan de achterkant bevinden van de 2D array, dus op 0,6 en 1,6 in dit geval.
            // Mocht dit veranderen dan kan MankalaV2Bord een andere definitie voor VulBord krijgen (dan zou deze ook abstract in Bord worden gezet)
        }
    }

    public override string ToString()
    {
        // Wordt gebruikt in GameManager.Display() voor de console standard output,
        // later alleen handig voor debuggen, als we UI gaan doen met andere klassen
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
