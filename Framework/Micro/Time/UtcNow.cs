namespace Micro.Time;

public class UtcNow : IClock
{
    public DateTime Current()
        => DateTime.UtcNow;
}
