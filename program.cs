namespace Lab2;

static class Program
{
    static void Main()
    {
        Console.WriteLine("helloworld");
    }
}

public class GameManager
{
    private MankalaSpel MankalaSpel = new MankalaV1(); // Enige plek waar we V1 kiezen
    
    void checkWinst()
    {
        
    }

    void naZet()
    {
        
    }

    void Display()
    {
        // Interact met Forms later, nu even console.log
    }
}

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

public class State
{
    private int speler;
    private bool spelGaande;
    private int spelerGewonnen;
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

public class MankalaV1Bord : Bord
{

    public MankalaV1Bord()
    {
        // Maak bord met bepaalde hvh kuiltjes
        this.Kuiltjes = new Kuiltje[2, 3];
    }
}

public abstract class Bord
{
    protected Kuiltje[,] Kuiltjes;
    protected List<ThuisKuiltje> ThuisKuiltjes; // Moet miss Kuiltjes type zijn
}
public abstract class Kuiltje
{
    private int speler;
    private int steentjes;

    public abstract void AddSteentje();
}

public class ThuisKuiltje : Kuiltje
{
    public ThuisKuiltje() : base()
    {
    }

    public override void AddSteentje()
    {
        throw new NotImplementedException();
    }

    public void AddSteentjes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.AddSteentje();
        }
    }
}

public class BordKuiltje : Kuiltje
{
    public BordKuiltje() : base()
    {
    }

    public override void AddSteentje()
    {
        throw new NotImplementedException();
    }
}