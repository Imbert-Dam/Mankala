public class State
{ // Singleton
    public int Speler;
    public int SpelerGewonnen;
    private static State _instance;
    private static readonly object _instanceLock = new();

    private State()
    {
        Speler = 1;
        SpelerGewonnen = 0;
    }
    public static State GetInstance()
    {
        if(_instance == null)
        {
            lock (_instanceLock) 
            { // een enkele thread kan maar State maken
                if (_instance == null) 
                { // nm de eerste
                    _instance = new State();
                }
            }
        }
        return _instance;
    }
    public void VolgendeSpeler()
    {
        Speler = GetAndereSpeler(Speler);
    }
    public int GetAndereSpeler(int p1)
    {
        return (p1 == 1) ? 2 : 1; // ah ternary : als p1==1 dan 2 anders 1
    }
}