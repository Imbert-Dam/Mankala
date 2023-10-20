public abstract class Rule
{
    public abstract (bool,bool) startRuleProcedure(Bord bord, State state, int row, int collumn);
    // Returnen van Bord wellicht niet nodig; bool is voor nieuwe zet -> meestal false
    // Als die true is zal het spel opnieuw regels checken op het nieuwe Bord
    protected abstract void ruleResultaat(Bord bord, State state);
}

class ThuiskuiltjeSpeler : Rule
{
    /* De laatste steen komt terecht in het thuiskuiltje van de speler. De speler mag nu
    een nieuwe zet doen. Er is geen maximum aan het aantal keer dat een speler achter
    elkaar aan de beurt is
    */
    public override (bool,bool) startRuleProcedure(Bord bord, State state, int row, int collumn)
    {
        // Check of regel daadwerkelijk van toepassing is
        Kuiltje eindkuiltje = bord.Kuiltjes[row,column];
        if ((eindkuiltje.speler == state.speler) && (eindkuiltje.GetType()==typeof(ThuisKuiltje)))
        {
            ruleResultaat(bord, state);
            return false;
        }

        return false;
    }

    protected override void ruleResultaat(Bord bord, State state)
    {
        // Doe dingen met bord
    }
    
    
}

class NietLeegKuiltjeSpeler : Rule
{
    /*De laatste steen komt in een ander kuiltje dan het thuiskuiltje van de speler, en dat
    kuiltje was niet leeg. De speler pakt alle stenen in het kuiltje op, en gaat verder
    met de beurt. Er is geen maximum aan het aantal keren dat in een beurt stenen
    opgepakt kunnen worden.*/
    
    public override (bool,bool) startRuleProcedure(Bord bord, State state, int row, int collumn)
    {
        // Check of regel daadwerkelijk van toepassing is
        if (true)
        {
            ruleResultaat(bord, state);
            return false; // ZET DIT OP TRUE ALS REGEL GEIMPLEMENT IS !!!!1!11!!!11 
        }
    }

    protected override void ruleResultaat(Bord bord, State state)
    {
        // Doe dingen met bord        
    }
    
}

class TegenoverNietLeeg : Rule
{
    /* De laatste steen komt in een leeg kuiltje van de speler. Het kuiltje van de tegenspeler daartegenover is niet leeg. De speler pakt de laatst uitgestrooide steen plus
    de stenen in het kuiltje van de tegenspeler ertegenover, en voegt ze toe aan zijn
    thuiskuiltje. De zet is over, de beurt is over, de tegenspeler is aan de beurt.
    */
    public override bool startRuleProcedure(Bord bord, State state, int row, int column)
    {
        // Check of regel daadwerkelijk van toepassing is
        
        if (true)
        {
            ruleResultaat(bord, state);
            return false;
        }
    }

    protected override void ruleResultaat(Bord bord, State state)
    {
        throw new NotImplementedException();
    }
}