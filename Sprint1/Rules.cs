public abstract class Regel
{
    public abstract (bool,bool) StartRegelProcedure(Bord bord, State state, int row, int column);
    // BOOL, BOOL -> check_rules, swap_player -> of de verdere Regels nog gecheckt moeten worden, of de beurt door moet gaan
    protected abstract void RegelResultaat(Bord bord, State state, int row, int column);
}

class ThuiskuiltjeSpeler : Regel
{
    /* De laatste steen komt terecht in het thuiskuiltje van de Speler. De Speler mag nu
    een nieuwe zet doen. Er is geen maximum aan het aantal keer dat een Speler achter
    elkaar aan de beurt is
    */
    public override (bool,bool) StartRegelProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindKuiltje = bord.Kuiltjes[row, column];
        if ((eindKuiltje.Speler == state.Speler) && (eindKuiltje.GetType() == typeof(ThuisKuiltje)))
        {
            // Console.WriteLine("R1");
            RegelResultaat(bord, state, row, column);
            return (false,false);
        }

        return (true,true);
    }

    protected override void RegelResultaat(Bord bord, State state, int row, int column)
    {
        // Gebeurt niks
    }
    
}

class NietLeegKuiltje : Regel
{
    /*De laatste steen komt in een ander kuiltje dan het thuiskuiltje van de Speler, en dat
    kuiltje was niet leeg. De Speler pakt alle stenen in het kuiltje op, en gaat verder
    met de beurt. Er is geen maximum aan het aantal keren dat in een beurt stenen
    opgepakt kunnen worden.*/
    
    public override (bool,bool) StartRegelProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindkuiltje = bord.Kuiltjes[row, column];
        if (eindkuiltje.GetType() == typeof(BordKuiltje) && eindkuiltje.Steentjes > 1)
        {
            // Console.WriteLine("R2");
            RegelResultaat(bord, state, row, column);
            return(true,false);
        }
        return (true,true);
    }

    protected override void RegelResultaat(Bord bord, State state, int row, int column)
    {
        // Staat al in de while loop      
    }
    
}

class LeegTegenstander : Regel
{
    /*De laatste steen komt in een leeg kuiltje van de tegenspeler. De zet is over, en de
    beurt is over: de tegenspeler is aan de beurt.
    */
    public override (bool, bool) StartRegelProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindkuiltje = bord.Kuiltjes[row, column];
        if (eindkuiltje.Speler != state.Speler && eindkuiltje.Steentjes == 1)
        {
            // Console.WriteLine("R3");
            RegelResultaat(bord, state, row, column);
            return (false,true);
        }
        return (true,true);
    }

    protected override void RegelResultaat(Bord bord, State state, int row, int column)
    {
        // Gebeurt niks
    }
}

class TegenoverLeeg : Regel
{
    /* De laatste steen komt in een leeg kuiltje van de Speler. Het kuiltje van de tegenspeler daar tegenover is leeg.
     * De zet is over, de beurt is over: de tegenspeler is aan de beurt. */
    public override (bool, bool) StartRegelProcedure(Bord bord, State state, int row, int column)
    {
        // Nogal veel conditions, eerste 2 zijn daarom guard clauses
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        if (eindKuiltje.Speler != state.Speler || eindKuiltje.GetType() == typeof(ThuisKuiltje)) // Eindkuiltje is van oppo of is een thuiskuiltje
        {
            return (true, true);
        }
        // Dus eindkuiltje is van Speler EN is type Bordkuiltje
        if (eindKuiltje.Steentjes > 1)
        {
            return (true, true);
        }
        // Was dus voor de zet leeg
        Kuiltje tegenoverKuiltje = bord.Kuiltjes[state.GetAndereSpeler(state.Speler) - 1, column];// Kuiltje tegenover
        if (tegenoverKuiltje.Steentjes == 0) // happy flow
        {
            // Console.WriteLine("R4");
            RegelResultaat(bord, state, row, column);
            return (false, true);
        }
        // als niet happy flow
        return (true, true);
    }

    protected override void RegelResultaat(Bord bord, State state, int row, int column)
    {
        // Niets...
    }
}

class TegenoverNietLeeg : Regel
{
    /* De laatste steen komt in een leeg kuiltje van de Speler. Het kuiltje van de tegenspeler daartegenover is niet leeg.
     De Speler pakt de laatst uitgestrooide steen plus de stenen in het kuiltje van de tegenspeler ertegenover, en voegt 
     ze toe aan zijn thuiskuiltje. De zet is over, de beurt is over, de tegenspeler is aan de beurt.
    */
    public override (bool, bool) StartRegelProcedure(Bord bord, State state, int row, int column)
    {
        // Nogal veel conditions, eerste 2 zijn daarom guard clauses
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        if (eindKuiltje.Speler != state.Speler || eindKuiltje.GetType() == typeof(ThuisKuiltje))// Eindkuiltje is van oppo of is een thuiskuiltje
        {
            return (true, true);
        }
        // Dus eindkuiltje is van Speler EN is type Bordkuiltje
        if (eindKuiltje.Steentjes > 1)
        {
            return (true, true);
        }
        // Was dus voor de zet leeg
        Kuiltje tegenoverKuiltje = bord.Kuiltjes[state.GetAndereSpeler(state.Speler) - 1, column];// Kuiltje van andere speler tegenover
        // Dit is de happy case
        if (tegenoverKuiltje.Steentjes > 0)
        {
            // Console.WriteLine("R5");
            RegelResultaat(bord, state, row, column);
            return (false, true);
        }
        return (true, true);
    }

    protected override void RegelResultaat(Bord bord, State state, int row, int column)
    {
        Kuiltje tegenoverKuiltje = bord.Kuiltjes[state.GetAndereSpeler(state.Speler) - 1, column];// Kuiltje van andere speler tegenover
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        
        int gestolenSteentjes = 0;
        
        gestolenSteentjes += eindKuiltje.Steentjes; // 1 als het goed is
        eindKuiltje.VerwijderSteentjes();
        
        gestolenSteentjes += tegenoverKuiltje.Steentjes;
        tegenoverKuiltje.VerwijderSteentjes();

        ThuisKuiltje thuisKuiltjeSpeler = bord.ThuisKuiltjes[state.Speler - 1];
        thuisKuiltjeSpeler.AddSteentjes(gestolenSteentjes);
        
    }
}