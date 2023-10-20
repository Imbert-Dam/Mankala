public abstract class WinChecker
{
    protected int n_kuiltjes_player;
    protected Bord Speelbord;
    protected int current_player;
    public WinChecker (int n_kuiltjes)
    {
        n_kuiltjes_player = n_kuiltjes;
    }
    public abstract int spelWinstSpeler(Bord Speelbord, int current_player);
}

public class WinCheckerV1 : WinChecker
{
    public WinCheckerV1 (int n_kuiltjes):base(n_kuiltjes)
    {
        n_kuiltjes_player = n_kuiltjes;
    }
    public override int spelWinstSpeler(Bord Speelbord, int current_player)//naam aanpassen naar update winner
    {
        this.Speelbord = Speelbord;
        this.current_player = current_player;
        if(nietLegeKuiltjes()) return 0;
        return besteSpeler();
    }
    private bool nietLegeKuiltjes()
    {
        for (int i = 0; i < n_kuiltjes_player-1; i++) //TODO aantal thuis kuiltjes = 1
        {
            if(Speelbord.Kuiltjes[current_player-1 , i].Steentjes != 0)
            {
                return true;
            }
        }
        return false;
    }

    private int besteSpeler()
    {
        int speler_1_score = 0;
        int speler_2_score = 0;
        foreach (ThuisKuiltje tk in Speelbord.ThuisKuiltjes)
        {
            if(tk.Speler == 1)
            {
                speler_1_score += tk.Steentjes;
            }
            else
            {
                speler_2_score += tk.Steentjes;
            }
        }
        if      (speler_1_score>speler_2_score)       return 1;
        else if (speler_1_score<speler_2_score)       return 2;
        return 3; //draw
    }
    
}