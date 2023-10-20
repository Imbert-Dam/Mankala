using System.ComponentModel.DataAnnotations.Schema;

public abstract class MankalaSpel
{
    private State _state;
    private Bord _speelbord;
    private WinChecker _winChecker;
    public int HuildigeSpeler; // Stores huidige Speler maar ook winnaar als afgelopen
    public int AantalKuiltjesSpeler;
    protected int AantalRijenSpeler;
    protected List<Regel> Regels; // Content is voor elke variant anders

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
        for (int i = 0; i < aantalSteentjes; i++)
        {
            huildigKuiltje += 1;
            if (BuitenArray(huildigKuiltje) || TegenstanderThuisKuiltje(_speelbord.Kuiltjes[huildigeRij , huildigKuiltje]))
            {
                huildigKuiltje = 0;
                huildigeRij = _state.GetAndereSpeler(huildigeRij + 1) - 1;
            }
            _speelbord.Kuiltjes[huildigeRij, huildigKuiltje].AddSteentje();
        } 
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
        int huildigeRij = HuildigeSpeler - 1;
        int huildigKuiltje = kuiltje - 1;
        bool checkRegels = true;
        bool swapSpeler = true;
        while (checkRegels)
        {
            (int rij, int kolom) = Zet(huildigeRij, huildigKuiltje);
            foreach (Regel regel in Regels)
            {
                (checkRegels, swapSpeler) = regel.StartRegelProcedure(_speelbord, _state, rij, kolom);
                if (!checkRegels) break;
            }
            huildigeRij = rij;
            huildigKuiltje = kolom;
        }
        if (swapSpeler) UpdateSpeler();
        _state.SpelerGewonnen = _winChecker.spelWinstSpeler(_speelbord, _state.Speler);
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
    }

    protected override void SetBordSettings()
    {
        AantalRijenSpeler = 1;
        AantalKuiltjesSpeler = 7;
    }

    protected override Bord GetBord()
    {
        int aantalSpelers = 2; // Wordt nooit echt veranderd but cant hurt
        return new MankalaV1Bord(AantalRijenSpeler * aantalSpelers , AantalKuiltjesSpeler);
    }

    protected override WinChecker GetWinChecker()
    {
        return new WinCheckerV1(AantalKuiltjesSpeler);
    }
}