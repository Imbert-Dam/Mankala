public abstract class Rule
{
    public abstract bool startRuleProcedure(Bord bord, State state);
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
    public override bool startRuleProcedure(Bord bord, State state)
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
        // Doe dingen met bord
    }
    
    
}

class NietLeegKuiltje : Rule
{
    /*De laatste steen komt in een ander kuiltje dan het thuiskuiltje van de speler, en dat
    kuiltje was niet leeg. De speler pakt alle stenen in het kuiltje op, en gaat verder
    met de beurt. Er is geen maximum aan het aantal keren dat in een beurt stenen
    opgepakt kunnen worden.*/
    
    public override bool startRuleProcedure(Bord bord, State state)
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