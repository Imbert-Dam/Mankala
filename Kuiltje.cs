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