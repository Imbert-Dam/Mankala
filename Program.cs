namespace Lab2;

static class Program
{
    static void Main()
    {
        GameManager.GameLoop();
    }
}

public static class GameManager
{
    //singleton strategy??
    private static MankalaSpel _mankalaSpel = new MankalaV1(); // Enige plek waar we V1 kiezen
    
    public static void GameLoop()
    {
        // Als GetWinnendeSpeler nog 0 is = geen winnaar, 1 en 2 is winnaar, 3 is gelijkspel 
        while(_mankalaSpel.GetWinnendeSpeler() == 0)
        {
            Console.WriteLine(Display());
            int input = InputLezer();
            _mankalaSpel.ZetResultaat(input);

        }

        if (_mankalaSpel.GetWinnendeSpeler() == 3)
        {
            Console.WriteLine("Gelijkspel!");
        }
        else
        {
            Console.WriteLine($"Speler {_mankalaSpel.GetWinnendeSpeler()} heeft gewonnen!");
        }
        
    }

    static string Display()
    {
        // Interact met Forms later
        return _mankalaSpel.BordNaarString(); 
    }

    static int InputLezer()
    {
        Console.WriteLine($"Wat is Speler's {_mankalaSpel.HuildigeSpeler} zet?");
        string? input = Console.ReadLine();
        if(input != null && int.TryParse(input, out int nummer))
        {
            if(BestaandKuiltje(nummer) && _mankalaSpel.KuiltjeNietLeeg(nummer))
            {
                return nummer;
            }
        }
        Console.WriteLine("Incorrecte input...");
        return InputLezer();   
    }

    static bool BestaandKuiltje(int kuiltje)
    {
        return kuiltje > 0 && kuiltje < _mankalaSpel.AantalKuiltjesSpeler;
    }
}