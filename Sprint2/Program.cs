using Sprint2;

namespace Lab2;

static class Program
{
    static void Main()
    {
        
        Console.WriteLine("Kies welke versie van de Mankala-applicatie gestart moet worden.\n" +
        "Typ 'c' voor console of 'f' voor WinForms.");
        string input = Console.ReadLine();
        if (input == null || input.Equals(""))
        {
            Console.WriteLine("Geef input!");
            // Kan in while loopje voor completeness
        }
        if (input.Equals("c")) {
            GameManager.KiesVariant();
            GameManager.GameLoop();
        }
        if (input.Equals("f"))
        {
            Application.Run(new UI());
        }

    }
}

public static class GameManager
{

    private static MankalaSpel _mankalaSpel;

    public static void KiesVariant()
    {
        Console.WriteLine("Kies een variant.\n1: Mankala Standaard\n2: Wari\n3: LocalSpecial");
        string variant = Console.ReadLine();
        if (variant == null || variant.Equals(""))
        {
            Console.WriteLine("Kies een variant!");
        }
        if (variant.Equals("1"))
        {
            _mankalaSpel = new MankalaV1();
        }
        else if(variant.Equals("2"))
        {
            //_mankalaSpel = new MankalaWari();
        }
        else if(variant.Equals("3"))
        {
            //_mankalaSpel = new MankalaV3();
        }
    }
    public static void GameLoop()
    {
        // Als GetWinnendeSpeler nog 0 is = geen winnaar, 1 en 2 is win van die speler, 3 is gelijkspel 
        while(_mankalaSpel.GetWinnendeSpeler() == 0)
        {
            // Output, lees input, doe die zet
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

    private static string Display()
    {
        // Interact met Forms later ipv dit
        return _mankalaSpel.BordNaarString(); 
    }

    private static int InputLezer()
    {
        Console.WriteLine($"Wat is Speler's {_mankalaSpel.HuildigeSpeler} zet?");
        string? input = Console.ReadLine();
        //Check of input geldig is en of die kuil niet leeg is
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

    private static bool BestaandKuiltje(int kuiltje)
    {
        // Hulpmethode zoals zo veel
        return kuiltje > 0 && kuiltje < _mankalaSpel.AantalKuiltjesSpeler;
    }
}