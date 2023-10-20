using System.Runtime.Intrinsics.X86;

public abstract class Kuiltje
{
    public int Speler;
    public int Steentjes; // Je zei hier Getter, maar miss beter gwn zo? We moeten hem ook kunnen setten namelijk
    public Kuiltje(int steentjes, int speler)
    {
        Steentjes = steentjes;
        Speler = speler;
    }
    public void VerwijderSteentjes()
    {
        Steentjes = 0;
    }
    public abstract void AddSteentje();
}

public class ThuisKuiltje : Kuiltje
{
    public ThuisKuiltje(int steentjes, int speler) : base(steentjes, speler)
    {
    }

    public override void AddSteentje()
    {
        Steentjes += 1;
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
    public BordKuiltje(int steentjes, int speler) : base(steentjes, speler)
    {
    }
    public override void AddSteentje()
    {
        Steentjes += 1;
    }
    
}