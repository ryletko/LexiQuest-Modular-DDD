namespace LexiQuest.Framework.Domain;

public abstract class BusinessRule
{
    protected abstract bool Rule();
    public abstract string ErrorMessage { get; }

    public void CheckRule()
    {
        if (!Rule())
        {
            throw new BusinessRuleValidationException(this);
        }
    }

    public static void Check(BusinessRule rule)
    {
        rule.CheckRule();
    } 
}

