namespace ConsoleApplication.Utils;

public static class CancellationTokenUtil
{
    public static CancellationToken GetToken()
    {
        var source = new CancellationTokenSource();

        source.CancelAfter(TimeSpan.FromSeconds(5));

        return source.Token;
    }
}