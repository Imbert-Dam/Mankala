public abstract class MankalaSpel
{
    private State state;
    public Bord Speelbord;

    public MankalaSpel()
    {
        Speelbord = GetBord();
    }
    protected abstract Bord GetBord();
    public abstract void Zet();
    public abstract void ZetResultaat();
    public abstract bool CheckWin();
}

public class MankalaV1 : MankalaSpel
{
     protected override Bord GetBord()
     {
         return new MankalaV1Bord();
     }

    public override void Zet()
    {
        
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