public abstract class Rule
{
    public abstract (bool,bool) startRuleProcedure(Bord bord, State state, int row, int column);
    // BOOL, BOOL -> check_rules, swap_player -> of de verdere regels nog gecheckt moeten worden, of de beurt door moet gaan
    protected abstract void ruleResultaat(Bord bord, State state, int row, int column);
}

class ThuiskuiltjeSpeler : Rule
{
    /* De laatste steen komt terecht in het thuiskuiltje van de speler. De speler mag nu
    een nieuwe zet doen. Er is geen maximum aan het aantal keer dat een speler achter
    elkaar aan de beurt is
    */
    public override (bool,bool) startRuleProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        if ((eindKuiltje.speler == state.speler) && (eindKuiltje.GetType()==typeof(ThuisKuiltje)))
        {
            ruleResultaat(bord, state, row, column);
            return (false,false);
        }

        return (true,false);
    }

    protected override void ruleResultaat(Bord bord, State state, int row, int column)
    {
        // Gebeurt niks
    }
    
}

class NietLeegKuiltje : Rule
{
    /*De laatste steen komt in een ander kuiltje dan het thuiskuiltje van de speler, en dat
    kuiltje was niet leeg. De speler pakt alle stenen in het kuiltje op, en gaat verder
    met de beurt. Er is geen maximum aan het aantal keren dat in een beurt stenen
    opgepakt kunnen worden.*/
    
    public override (bool,bool) startRuleProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindkuiltje = bord.Kuiltjes[row,column];
        if (eindkuiltje.GetType()==typeof(BordKuiltje) && eindkuiltje.steentjes > 1)
        {
            ruleResultaat(bord, state, row, column);
        }
        return (true,false);
    }

    protected override void ruleResultaat(Bord bord, State state, int row, int column)
    {
        // Staat al in de while loop      
    }
    
}

class LeegTegenstander : Rule
{
    /*De laatste steen komt in een leeg kuiltje van de tegenspeler. De zet is over, en de
    beurt is over: de tegenspeler is aan de beurt.
    */
    public override (bool, bool) startRuleProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindkuiltje = bord.Kuiltjes[row,column];
        if (eindkuiltje.speler != state.speler && eindkuiltje.steentjes == 1)
        {
            ruleResultaat(bord, state, row, column);
            return (false,true);
        }
        return (true,false);
    }

    protected override void ruleResultaat(Bord bord, State state, int row, int column)
    {
        // Gebeurt niks
    }
}

class TegenoverLeeg : Rule
{
    /*
     * De laatste steen komt in een leeg kuiltje van de speler. Het kuiltje van de tegenspeler daar tegenover is leeg.
     * De zet is over, de beurt is over: de tegenspeler is aan de beurt.
     */
    public override (bool, bool) startRuleProcedure(Bord bord, State state, int row, int column)
    {
        throw new NotImplementedException();
    }

    protected override void ruleResultaat(Bord bord, State state, int row, int column)
    {
        throw new NotImplementedException();
    }
}

class TegenoverNietLeeg : Rule
{
    /* De laatste steen komt in een leeg kuiltje van de speler. Het kuiltje van de tegenspeler daartegenover is niet leeg.
     De speler pakt de laatst uitgestrooide steen plus de stenen in het kuiltje van de tegenspeler ertegenover, en voegt 
     ze toe aan zijn thuiskuiltje. De zet is over, de beurt is over, de tegenspeler is aan de beurt.
    */
    public override (bool, bool) startRuleProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        if (eindKuiltje.speler != state.speler || eindKuiltje.GetType()==typeof(ThuisKuiltje))// Eindkuiltje is van oppo of is een thuiskuiltje
        {
            return (true, false);
        }
        // Dus eindkuiltje is van speler EN is type Bordkuiltje
        if (eindKuiltje.steentjes != 1)
        {
            return (true, false);
        }
        // Was dus voor de zet leeg
        Kuiltje tegenoverKuiltje = bord.Kuiltjes[state.getOtherPlayer(state.speler) - 1, column];// Kuiltje tegenover
        // Dit is de happy case
        if (tegenoverKuiltje.steentjes == 0)
        {
            ruleResultaat(bord, state, row, column);
            return (false, true);
        }
        // Als niet happy case: return default
        return (true, false);
    }

    protected override void ruleResultaat(Bord bord, State state, int row, int column)
    {
        Kuiltje tegenoverKuiltje = bord.Kuiltjes[state.getOtherPlayer(state.speler) - 1, column];// Kuiltje tegenover
        Kuiltje eindKuiltje = bord.Kuiltjes[row,column];
        
        int gestolenSteentjes = 0;
        
        gestolenSteentjes += eindKuiltje.steentjes; // 1 als het goed is
        eindKuiltje.steentjes = 0; 
        
        gestolenSteentjes += tegenoverKuiltje.steentjes;
        tegenoverKuiltje.steentjes = 0;

        Kuiltje thuisKuiltjeSpeler = bord.ThuisKuiltjes[state.speler - 1];
        thuisKuiltjeSpeler.steentjes += gestolenSteentjes;
        
    }
}