using System.ComponentModel.DataAnnotations.Schema;

public abstract class MankalaSpel
{
    private State _state;
    private Bord _speelbord;
    private WinChecker _winChecker;
    public int HuildigeSpeler; // Stores huidige Speler maar ook winnaar als afgelopen
    public int AantalKuiltjesSpeler;
    protected int AantalRijenSpeler;
    protected List<Regel> Regels; // Regels zijn voor elke variant anders

    protected MankalaSpel()
    {
        SetBordSettings();
        _speelbord = GetBord(); 
        _state = State.GetInstance();
        _winChecker = GetWinChecker();
        HuildigeSpeler = _state.Speler;
    }
    protected abstract Bord GetBord();
    protected abstract void SetBordSettings();
    private (int,int) Zet(int huildigeRij, int huildigKuiltje)
    {
        int aantalSteentjes = _speelbord.Kuiltjes[huildigeRij , huildigKuiltje].Steentjes;
        _speelbord.Kuiltjes[huildigeRij, huildigKuiltje].VerwijderSteentjes();
        // Standaard behaviour: ze stuk voor stuk, tegen de klok in, met een tegelijk in de kuiltjes van de Speler zelf en van
        // de tegenstander te strooien
        // In 2D array dus als volgt: van 0,6 -> 1,0, en van 1,6 -> 0,1
        for (int i = 0; i < aantalSteentjes; i++)
        {
            huildigKuiltje += 1;
            // Skip tegenstandars thuis kuiltje, en behaviour voor door 2d array itereren
            if (BuitenArray(huildigKuiltje) || TegenstanderThuisKuiltje(_speelbord.Kuiltjes[huildigeRij , huildigKuiltje]))
            {
                huildigKuiltje = 0;
                huildigeRij = _state.GetAndereSpeler(huildigeRij + 1) - 1;
            }
            _speelbord.Kuiltjes[huildigeRij, huildigKuiltje].AddSteentje();
        } 
        // Return het eind kuiltje, hiervandaan worden regels toegepast
        return (huildigeRij, huildigKuiltje);
    }
    private bool BuitenArray(int kuiltje)
    {
        return kuiltje >= AantalKuiltjesSpeler;
    }
    private bool TegenstanderThuisKuiltje(Kuiltje k)
    {
        return k.Speler != HuildigeSpeler && k.GetType() == typeof(ThuisKuiltje);
    }
    private void UpdateSpeler()
    {
        _state.VolgendeSpeler();
        HuildigeSpeler = _state.Speler;
    }

    public void ZetResultaat(int kuiltje)
    {
        // Overarching methode van Zet. Zet doet een enkele verplaatsing van steentjes, terwijl ZetResultaat ook 
        // regels bekijkt en dus ook kijkt of er nog een zet voor die speler komt etc.
        int huildigeRij = HuildigeSpeler - 1;
        int huildigKuiltje = kuiltje - 1;
        // Zolang checkRegels true is blijft de loop checken, maar als een regel is gevonden waarbij de zet over is wordt deze false, dit zal ooit gebeuren
        bool checkRegels = true;
        bool swapSpeler = true;
        while (checkRegels)
        {
            // Geeft ons standaard logica van steentjes strooien + het eindkuiltje, waarop we regels moeten checken
            (int rij, int kolom) = Zet(huildigeRij, huildigKuiltje);
            foreach (Regel regel in Regels)
            {
                // Polymorfe aanroep van regel logic
                (checkRegels, swapSpeler) = regel.StartRegelProcedure(_speelbord, _state, rij, kolom);
                // Returnt of we door moeten gaan met het checken van regels, en of de beurt switched of niet
                if (!checkRegels) break;
            }
            huildigeRij = rij;
            huildigKuiltje = kolom;
        }
        if (swapSpeler) UpdateSpeler();
        _state.SpelerGewonnen = _winChecker.SpelWinstSpeler(_speelbord, _state.Speler); // Check na zet altijd voor winst
    }
    public int GetWinnendeSpeler()
    {
        return _state.SpelerGewonnen;
    }
    protected abstract WinChecker GetWinChecker();

    public string BordNaarString()
    {
        return _speelbord.ToString(); // possible NPE aaaaaaa
    }
    public bool KuiltjeNietLeeg(int input)
    {
        return _speelbord.Kuiltjes[HuildigeSpeler-1, input - 1].Steentjes != 0;
    }
}

public class MankalaV1 : MankalaSpel
{
    public MankalaV1()
    {   
        // Voor andere varianten van V1 kunnen deel van deze rules gebruikt worden, of nieuwe worden gemaakt
        Regels = new List<Regel>
        {
            new ThuiskuiltjeSpeler(),
            new NietLeegKuiltje(),
            new LeegTegenstander(),
            new TegenoverLeeg(),
            new TegenoverNietLeeg()
        };
        // Factory method-esque, aangezien we een aantal regels 'aanzetten' voor deze variant
    }

    protected override void SetBordSettings()
    {
        AantalRijenSpeler = 1;
        AantalKuiltjesSpeler = 7;
        // kan worden uitgebreid met standaard hoeveelheid steentjes (wel uniform)
    }

    protected override Bord GetBord()
    {
        // Factory method
        int aantalSpelers = 2; // Wordt nooit echt veranderd but cant hurt
        int steentjesPerKuiltje = 4; // Kan ook member variabele van klasse worden zoals AantalTijenSpeler en AantalKuiltjesSpeler
        return new MankalaV1Bord(AantalRijenSpeler * aantalSpelers , AantalKuiltjesSpeler, steentjesPerKuiltje);
    }

    protected override WinChecker GetWinChecker()
    {
        // Factory method
        return new WinCheckerV1(AantalKuiltjesSpeler);
    }
}