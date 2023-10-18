public class State
{//singleton???
    public int speler;
    // public bool spelGaande; kan alleen met int spelerGewonnen
    public int spelerGewonnen;
    private static State _instance;
    private static readonly object _instanceLock = new();

    private State()
    {
        this.speler = 1;
        // this.spelGaande = true;
        this.spelerGewonnen = 0;
    }
    public static State getInstance()
    {
        if(_instance == null)
        {
            lock (_instanceLock) 
            { // prevents multiple threads from creating an instance of Printer
                if (_instance == null) 
                    { // it will be only used by the first thread
                        _instance = new State();
                    }
            }
        }
        return _instance;
    }
    public void nextPlayer()
    {
        if (speler == 1)
        {
            this.speler = 2;
        }
        else
        {
            this.speler = 1;
        }
    }
}