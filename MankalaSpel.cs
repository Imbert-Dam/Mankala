public abstract class MankalaSpel
{
    public State state;
    public Bord Speelbord;
    public int current_player; //stores huidige speler maar ook winnaar als afgelopen
    public int n_kuiltjes_player;
    public int n_rows_player;


    public MankalaSpel()
    {
        Speelbord = GetBord();
        state = State.getInstance();
    }
    protected abstract Bord GetBord();
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
     protected override Bord GetBord()
     {
         return new MankalaV1Bord();
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