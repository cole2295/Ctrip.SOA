using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.IOCFactoryModel;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Interface;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.InstCreator
{
    public class NormalInstCreator : IInstCreator
    {
        internal NormalInstCreator()
        {
        }

        public RegistCheckResult Check(RegistObjectContext context)
        {
            return true;
        }


        public object CreateInst(RegistObjectContext context, params object[] param)
        {
            return Activator.CreateInstance(context.ObjType, param);
        }
    }
}
