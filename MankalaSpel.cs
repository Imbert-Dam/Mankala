using System.ComponentModel.DataAnnotations.Schema;

public abstract class MankalaSpel
{
    public State state;
    public Bord Speelbord;
    protected WinChecker win_check;
    public int current_player; //stores huidige speler maar ook winnaar als afgelopen
    public int n_kuiltjes_player;
    public int n_rows_player;
    public List<Rule> regels; // Is voor elke variant anders

    public MankalaSpel()
    {
        bordSettings();
        Speelbord = GetBord();
        state = State.getInstance();
        win_check = getWinChecker();
        current_player = state.speler;
    }
    protected abstract Bord GetBord();
    protected abstract void bordSettings();
    public (int,int) Zet(int current_row, int current_kuiltje)
    {
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
        return(current_row,current_kuiltje);
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
        current_player = state.speler; // current_player is eigk heel irritant; miss gwn overal state.speler gebruiken?
    }

    public virtual void ZetResultaat(int kuiltje) // Hoeft niet echt virtual te zijn eigk -> tenzij we later sjit moeten doen
    {
        int current_row = current_player-1;
        int current_kuiltje = kuiltje-1;
        bool check_rules = true;
        bool change_player = true;
        while (check_rules)
        {
            (int row,int column) = Zet(current_row,current_kuiltje);
            foreach (Rule regel in regels)
            {
                (check_rules,change_player) = regel.startRuleProcedure(Speelbord, state ,row, column);
                if (!check_rules) break;
            }
            current_row=row;
            current_kuiltje=column;
        }
        if (change_player) updatePlayer();
        state.spelerGewonnen = win_check.SpelWinstSpeler(Speelbord,state.speler);
    }
    public int winnendeSpeler()
    {
        return state.spelerGewonnen;
    }
    public abstract WinChecker getWinChecker();

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
    public MankalaV1()
    {   
        // Voor andere varianten van V1 kunnen deel van deze rules gebruikt worden, of nieuwe worden gemaakt
        regels = new List<Rule>
        {
            new ThuiskuiltjeSpeler(),
            new NietLeegKuiltje(),
            new LeegTegenstander(),
            new TegenoverLeeg(),
            new TegenoverNietLeeg()
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

    public override WinChecker getWinChecker()
    {
        return new WinCheckerV1(n_kuiltjes_player);
    }
}