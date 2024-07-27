namespace Business.Helpers;

public class PriorityHelper
{
    public static (string Text, string CssClass) GetPriorityString(int priority)
    {
        return priority switch
        {
            1 => ("Low", "priority-low"),
            2 => ("Medium", "priority-medium"),
            3 => ("High", "priority-high"),
            _ => ("Unknown", "priority-unknown")
        };
    }
}