using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Ctrip.SOA.Infratructure.Logging;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Transactions;

namespace Ctrip.SOA.Infratructure.Aop
{
    public class TrancationHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn retValue = null;
            var option = new TransactionOptions
            {
                 IsolationLevel = IsolationLevel.ReadCommitted,
                 Timeout = TimeSpan.FromSeconds(60)
            };
            using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                retValue = getNext()(input, getNext);
                if (retValue.Exception == null)
                    scope.Complete();
                else
                    Logging.LogHelper.WriteError(string.Empty, input.MethodBase.Name, retValue.Exception);
            }

            return retValue;
        }

        public int Order { get; set; }
    }
}
