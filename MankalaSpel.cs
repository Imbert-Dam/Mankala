public abstract class MankalaSpel
{
    public State state;
    public Bord Speelbord;
    public int currentPlayer; //stores huidige speler maar ook winnaar als afgelopen

    public MankalaSpel()
    {
        Speelbord = GetBord();
        state = State.getInstance();
    }
    protected abstract Bord GetBord();
    public virtual void Zet(int kuiltje)
    {
        currentPlayer= state.speler;
        int aantalsteentjes = Speelbord.Kuiltjes[currentPlayer,kuiltje-1].steentjes;
    }
    public abstract void ZetResultaat();
    public bool isWinst()
    {
        int winnaar = state.spelerGewonnen;
        if (winnaar!=0)
        {
            currentPlayer = winnaar; //store winnaar in currentPlayer
            return true;
        }
        return false;
    }
    public abstract bool CheckWin();
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