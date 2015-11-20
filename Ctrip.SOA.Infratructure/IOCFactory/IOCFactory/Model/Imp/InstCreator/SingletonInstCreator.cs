using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.IOCFactoryModel;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Interface;
using Ctrip.SOA.Infratructure.IOCFactory.Util;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.InstCreator
{
    public class SingletonInstCreator : IInstCreator
    {
        internal SingletonInstCreator()
        {
        }

        public RegistCheckResult Check(RegistObjectContext context)
        {
            return true;
        }


        public object CreateInst(RegistObjectContext context, params object[] param)
        {
            if (context.Obj == null)
            {
                lock (context)
                {
                    var diCreator = InstCreatorFactory.Create(Ctrip.SOA.Infratructure.IOCFactoryModel.InstType.Normal);
                    context.Obj = diCreator.CreateInst(context, param);
                }
            }
            return context.Obj;
        }
    }
}
