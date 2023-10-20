public abstract class WinChecker
{
    protected int NumKuiltjesPP;
    protected Bord Speelbord;
    protected int HuidigeSpeler;
    protected WinChecker (int aantalKuiltjes)
    {
        NumKuiltjesPP = aantalKuiltjes;
    }
    public abstract int SpelWinstSpeler(Bord speelbord, int huidigeSpeler);
}

public class WinCheckerV1 : WinChecker
{
    public WinCheckerV1 (int aantalKuiltjes) : base(aantalKuiltjes)
    {
        NumKuiltjesPP = aantalKuiltjes;
    }
    public override int SpelWinstSpeler(Bord speelbord, int huidigeSpeler)
    {
        this.Speelbord = speelbord;
        this.HuidigeSpeler = huidigeSpeler;
        if(NietLegeKuiltjes()) return 0; // Cruciale condition
        return BesteSpeler();
    }
    private bool NietLegeKuiltjes()
    {
        for (int i = 0; i < NumKuiltjesPP - 1; i++) //-1 gezien aantal kuiltjes per persoon thuiskuiltjes meerekend.
        {
            if(Speelbord.Kuiltjes[HuidigeSpeler - 1 , i].Steentjes != 0)
            {
                return true;
            }
        }
        return false;
    }

    private int BesteSpeler()
    {   /* BesteSpeler controleert alle thuiskuiltjes en telt de hoeveelheid steentjes bij elkaar op,
        vervolgens wordt de speler met de meeste aantal steentjes gereturned of 3 wat betekent dat er
        een draw is.
        */
        int speler1Score = 0;
        int speler2Score = 0;
        foreach (ThuisKuiltje tk in Speelbord.ThuisKuiltjes)
        {
            if(tk.Speler == 1)
            {
                speler1Score += tk.Steentjes;
            }
            else
            {
                speler2Score += tk.Steentjes;
            }
        }
        if (speler1Score > speler2Score) return 1; //speler 1 heeft gewonnen
        if (speler1Score < speler2Score) return 2; //speler 2 heeft gewonnen
                                         return 3; //gelijkspel
    }
    
}