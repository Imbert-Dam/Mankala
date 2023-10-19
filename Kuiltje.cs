public abstract class Kuiltje
{
    public Kuiltje(int steentjes, int speler)
    {
        this.steentjes = steentjes;
        this.speler = speler;

    }
    public int speler;
    public int steentjes; // Je zei hier Getter, maar miss beter gwn zo? We moeten hem ook kunnen setten namelijk

    public abstract void AddSteentje();
}

public class ThuisKuiltje : Kuiltje
{
    public ThuisKuiltje(int steentjes, int speler) : base(steentjes, speler)
    {
    }

    public override void AddSteentje()
    {
        steentjes += 1;
        // TODO nog meer logic i presume
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
        steentjes += 1;
        // TODO nog meer logic i presume
    }

    public int VerwijderSteentjes()
    {
        int oudeHoeveelheid = steentjes;
        steentjes = 0;
        return oudeHoeveelheid;
    }
}