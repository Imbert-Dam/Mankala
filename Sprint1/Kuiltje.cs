public abstract class Kuiltje
{
    // Elk kuiltje heeft een Speler van wie het kuiltje is, en een hoeveelheid steentjes
    public int Speler;
    public int Steentjes;
    public Kuiltje(int steentjes, int speler)
    {
        Steentjes = steentjes;
        Speler = speler;
    }
    public void VerwijderSteentjes()
    // Op dit moment wordt dit nergens gebruikt voor ThuisKuiltje, zou met andere regels wellicht kunnen
    {
        Steentjes = 0;
    }

    public void AddSteentje()
    {
        Steentjes += 1;
    }
}

public class ThuisKuiltje : Kuiltje
{
    public ThuisKuiltje(int steentjes, int speler) : base(steentjes, speler)
    {
    }
    
    public void AddSteentjes(int amount)
    // Liever forloop hier dan daar -> mocht er in de toekomst een regel zijn waar Bordkuiltjes ook meerdere
    // steentjes in een keer kunnen krijgen kan deze methode abstract of gewoon universeel (deel van superclass) worden
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
    // ThuisKuiltje en BordKuiltje lijken vrij weinig te verschillen, maar type is toch handig voor regels
    // En voor toekomst kan het ook geen kwaad
    
}