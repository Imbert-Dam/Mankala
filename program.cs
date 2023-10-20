namespace Lab2;

static class Program
{
    static void Main()
    {
        Console.WriteLine("helloworld");
        GameManager.gameLoop();
    }
}

public static class GameManager
{
    //singleton strategy??
    private static MankalaSpel MankalaSpel = new MankalaV1(); // Enige plek waar we V1 kiezen
    

    public static void gameLoop()
    {
        while(MankalaSpel.winnendeSpeler() == 0)
        {
            Console.WriteLine(Display());
            int input = inputLezer();
            MankalaSpel.ZetResultaat(input);

        }
        Console.WriteLine($"Speler {MankalaSpel.winnendeSpeler()} heeft gewonnen!");
    }

    // static bool isWinst() kunnen we in MankalaSpel doen
    // {
    //     if (MankalaSpel.state.spelGaande)
    //     {
    //         return false;   
    //     }
    //     return true;
        
    // }

    static void naZet()
    {
        
    }

    static string Display()
    {
        // Interact met Forms later, nu even console.log
        return MankalaSpel.bordNaarString(); // Zo doen? En dan ToString een definitie geven in Bord
    }

    static int inputLezer()
    {
        Console.WriteLine($"Wat is speler's {MankalaSpel.current_player} zet?");
        string? input = Console.ReadLine();
        if(input != null && int.TryParse(input, out int nummer))
        {
            if(bestaandKuiltje(nummer) && MankalaSpel.nietLeeg(nummer))
            {
                return nummer;
            }
        }
        Console.WriteLine("incorrect input....");
        return inputLezer();   
    }

    static bool bestaandKuiltje(int kuiltje)
    {
        return kuiltje>0 && kuiltje <MankalaSpel.n_kuiltjes_player;
    }
}