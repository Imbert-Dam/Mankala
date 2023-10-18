public abstract class Kuiltje
{
    private int speler;
    public int steentjes; // Je zei hier Getter, maar miss beter gwn zo? We moeten hem ook kunnen setten namelijk

    public abstract void AddSteentje();
}

public class ThuisKuiltje : Kuiltje
{
    public ThuisKuiltje() : base()
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
    public BordKuiltje() : base()
    {
    }

    public override void AddSteentje()
    {
        steentjes += 1;
        // TODO nog meer logic i presume
    }
    //TODO deze ook een AddSteentjes? Hebben we eigk wel nodig als we het bord initializeren
}