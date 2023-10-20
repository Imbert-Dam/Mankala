public abstract class WinChecker
{
    protected int NumKuiltjesPP;
    protected Bord Speelbord;
    protected int HuidigeSpeler;
    public WinChecker (int nKuiltjes)
    {
        NumKuiltjesPP = nKuiltjes;
    }
    public abstract int SpelWinstSpeler(Bord speelbord, int huidigeSpeler);
}

public class WinCheckerV1 : WinChecker
{
    public WinCheckerV1 (int nKuiltjes):base(nKuiltjes)
    {
        NumKuiltjesPP = nKuiltjes;
    }
    public override int SpelWinstSpeler(Bord speelbord, int huidigeSpeler)
    {
        this.Speelbord = speelbord;
        this.HuidigeSpeler = huidigeSpeler;
        if(NietLegeKuiltjes()) return 0;
        return BesteSpeler();
    }
    private bool NietLegeKuiltjes()
    {
        for (int i = 0; i < NumKuiltjesPP-1; i++) //-1 gezien aantal kuiltjes per persoon thuiskuiltjes meerekend.
        {
            if(Speelbord.Kuiltjes[HuidigeSpeler-1 , i].steentjes != 0)
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
                speler1Score += tk.steentjes;
            }
            else
            {
                speler2Score += tk.steentjes;
            }
        }
        if          (speler1Score>speler2Score)       return 1; //speler 1 heeft gewonnen
        else if     (speler1Score<speler2Score)       return 2; //speler 2 heeft gewonnen
        return 3;                                               //gelijkspel
    }
    
}