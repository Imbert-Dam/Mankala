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
        while(!isWinst())
        {
            int input = inputLezer();
            Console.WriteLine(input);
            break;

        }
        Console.WriteLine($"Speler {MankalaSpel.state.spelerGewonnen} heeft gewonnen!");
    }

    static bool isWinst()
    {
        if (MankalaSpel.state.spelGaande)
        {
            return false;   
        }
        return true;
        
    }

    static void naZet()
    {
        
    }

    static void Display()
    {
        // Interact met Forms later, nu even console.log
    }

    static int inputLezer()
    {
        Display();
        Console.WriteLine($"Wat is speler's {MankalaSpel.state.speler} zet?");
        string input = Console.ReadLine();
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