public abstract class MankalaSpel
{
    public State state;
    public Bord Speelbord;
    public int current_player; //stores huidige speler maar ook winnaar als afgelopen
    public int n_kuiltjes_player;
    public int n_rows_player;


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
        current_player= state.speler;
        int aantalsteentjes = Speelbord.Kuiltjes[current_player,kuiltje-1].steentjes;
    }
    public abstract void ZetResultaat();
    public bool isWinst()
    {
        int winnaar = state.spelerGewonnen;
        if (winnaar != 0)
        {
            current_player = winnaar; //store winnaar in currentPlayer
            return true;
        }
        return false;
    }
    public abstract bool CheckWin();

    public virtual string bordNaarString()
    {
        return Speelbord.ToString();
    }
}

public class MankalaV1 : MankalaSpel
{
    protected override void bordSettings()
    {
        n_rows_player = 1;
        n_kuiltjes_player = 7;
    }

    protected override Bord GetBord()
    {
        int aantal_spelers = 2;
        return new MankalaV1Bord(n_rows_player*aantal_spelers , n_kuiltjes_player);
    }

    public override void ZetResultaat()
    {
        throw new NotImplementedException();
    }

    public override bool CheckWin()
    {
        throw new NotImplementedException();
    }
}