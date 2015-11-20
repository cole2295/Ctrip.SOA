using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Interface;
using Ctrip.SOA.Infratructure.IOCFactory.Util;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.InstCreator
{
    public class DISingletonInstCreator : IInstCreator
    {
        public Ctrip.SOA.Infratructure.IOCFactoryModel.RegistCheckResult Check(Ctrip.SOA.Infratructure.IOCFactoryModel.RegistObjectContext context)
        {
            var diCreator = InstCreatorFactory.Create(Ctrip.SOA.Infratructure.IOCFactoryModel.InstType.DI);
            return diCreator.Check(context);
        }

        public object CreateInst(Ctrip.SOA.Infratructure.IOCFactoryModel.RegistObjectContext context, params object[] param)
        {
            if (context.Obj == null)
            {
                lock (context)
                {
                    var diCreator = InstCreatorFactory.Create(Ctrip.SOA.Infratructure.IOCFactoryModel.InstType.DI);
                    context.Obj = diCreator.CreateInst(context, param);
                }
            }
            return context.Obj;
        }
    }
}
