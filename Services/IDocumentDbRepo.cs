using System.Collections.Generic;

namespace Keyur.AspNet.Sample
{
    public interface IDocumentDbRepo
    {
        IEnumerable<Account> GetAccounts();
    }
}