public abstract class MankalaSpel
{
    public State state;
    public Bord Speelbord;
    public int current_player; //stores huidige speler maar ook winnaar als afgelopen
    public int n_kuiltjes_player;
    public int n_rows_player;
    public List<Rule> regels; // Is voor elke variant anders

    public MankalaSpel()
    {
        bordSettings();
        Speelbord = GetBord();
        state = State.getInstance();
        current_player = state.speler;
    }
    protected abstract Bord GetBord();
    protected abstract void bordSettings();
    public virtual void Zet(int kuiltje)
    {
        int current_row = current_player-1;
        int current_kuiltje = kuiltje-1;
        int aantalsteentjes = Speelbord.Kuiltjes[current_row , current_kuiltje].steentjes;
        Speelbord.Kuiltjes[current_row , current_kuiltje].VerwijderSteentjes();
        for (int i = 0; i < aantalsteentjes; i++)
        {
            current_kuiltje+=1;
            if(buitenArray(current_kuiltje) || tegenstanderThuisKuiltje(Speelbord.Kuiltjes[current_row , current_kuiltje]))
            {
                current_kuiltje = 0;
                current_row = state.getOtherPlayer(current_row+1)-1;
            }
            Speelbord.Kuiltjes[current_row , current_kuiltje].AddSteentje();

        } // misschien methode - sowieso ff comments
        ZetResultaat();
        updatePlayer();
        CheckWin();
        
    }
    protected virtual bool buitenArray(int kuiltje)
    {
        return kuiltje >= n_kuiltjes_player;
    }
    protected virtual bool tegenstanderThuisKuiltje(Kuiltje k)
    {
        return k.speler != current_player && k.GetType()==typeof(ThuisKuiltje);
    }
    public virtual void updatePlayer()
    {
        state.nextPlayer();
        current_player = state.speler;
    }

    public virtual void ZetResultaat() // Hoeft niet echt virtual te zijn eigk -> tenzij we later sjit moeten doen
    {
        bool nieuweZet = true;
        while (nieuweZet)
        {
            foreach (Rule regel in regels)
            {
                bool nieuweZetRegel;
                nieuweZetRegel = regel.startRuleProcedure(Speelbord, state);
                nieuweZet = nieuweZetRegel;
                if (nieuweZetRegel) break;
            }
        }
    }
    public int winnendeSpeler()
    {
        return state.spelerGewonnen;
    }
    public abstract void CheckWin();

    public virtual string bordNaarString()
    {
        return Speelbord.ToString();
    }
    public virtual bool nietLeeg(int input)
    {
        return Speelbord.Kuiltjes[current_player-1 , input-1].steentjes != 0;
    }
}

public class MankalaV1 : MankalaSpel
{
    public MankalaV1() : base()
    {
        regels = new List<Rule>
        {
            new ThuiskuiltjeSpeler(),
            new NietLeegKuiltje()
        };
    }

    protected override void bordSettings()
    {
        n_rows_player = 1;
        n_kuiltjes_player = 7;
    }

    protected override Bord GetBord()
    {
        int aantal_spelers = 2;
        return new MankalaV1Bord(n_rows_player * aantal_spelers , n_kuiltjes_player);
    }

    public override void CheckWin() //naam aanpassen naar update winner
    {
        for (int i = 0; i < n_kuiltjes_player-1; i++) //TODO aantal thuis kuiltjes = 1
        {
            if(Speelbord.Kuiltjes[current_player-1 , i].steentjes != 0)
            {
                return;
            }
        }
        int speler_1_score = 0;
        int speler_2_score = 0;
        foreach (ThuisKuiltje tk in Speelbord.ThuisKuiltjes)
        {
            if(tk.speler == 1)
            {
                speler_1_score += tk.steentjes;
            }
            else
            {
                speler_2_score += tk.steentjes;
            }
        }
        if(speler_1_score>speler_2_score)
        {
            state.spelerGewonnen = 1;
        }
        else state.spelerGewonnen = 2;
        

    }
}