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
        while(!MankalaSpel.isWinst())
        {
            int input = inputLezer();
            Console.WriteLine(input);
            break;

        }
        Console.WriteLine($"Speler {MankalaSpel.state.spelerGewonnen} heeft gewonnen!");
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

    static void Display()
    {
        // Interact met Forms later, nu even console.log
        MankalaSpel.Speelbord.ToString(); // Zo doen? En dan ToString een definitie geven in Bord
    }

    static int inputLezer()
    {
        Display();
        Console.WriteLine($"Wat is speler's {MankalaSpel.state.speler} zet?");
        string input = Console.ReadLine();
        // if (input == null){
        //     return inputLezer(); // Ik ben niet bekend met returnen van methodes
        // }
        char k = input[0]; //uitkijken voor null reference
        if(Char.IsNumber(k))
        {
            int nummer = int.Parse(k.ToString());
            if(nummer - ((MankalaSpel.Speelbord.Kuiltjes.Length - MankalaSpel.Speelbord.ThuisKuiltjes.Count)/2)<=0)
            {
                return nummer;
            }
        }
        Console.WriteLine("incorrect input....");
        return inputLezer();

        
    }
}