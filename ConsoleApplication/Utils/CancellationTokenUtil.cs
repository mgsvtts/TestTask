using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
