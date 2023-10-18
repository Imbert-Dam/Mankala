namespace Lab2;

static class Program
{
    static void Main()
    {
        Console.WriteLine("helloworld");
    }
}

public static class GameManager
{
    private static MankalaSpel MankalaSpel = new MankalaV1(); // Enige plek waar we V1 kiezen
    
    static void gameLoop()
    {
        while(checkWinst())
        {
            
        }
    }

    static bool checkWinst()
    {
        return true;
    }

    static void naZet()
    {
        
    }

    static void Display()
    {
        // Interact met Forms later, nu even console.log
    }
}