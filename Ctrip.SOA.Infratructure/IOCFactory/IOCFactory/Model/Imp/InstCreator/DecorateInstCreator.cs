using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.IOCFactoryModel;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Enum;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Interface;
using System.Reflection;
using Ctrip.SOA.Infratructure.IOCFactory.Util;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.InstCreator
{
    public class DecorateInstCreator : IInstCreator
    {
        internal DecorateInstCreator()
        {
        }



        public RegistCheckResult Check(RegistObjectContext context)
        {
            var type = context.ObjType;
            var constructs = type.GetConstructors();
            var returnValue = new RegistCheckResult();
            returnValue.IsPass = false;
            if (constructs.Length != 1)
            {
                returnValue.Message = string.Format("type regist as decorate must and only have 1 construct method");
                return returnValue;
            }
            var contruct = constructs[0];

            var parameters = contruct.GetParameters();

            if (parameters.Length != 1)
            {
                returnValue.Message = string.Format("type regist as decorate the construct method must and only have 1 param ");
                return returnValue;
            }

            var parameter = parameters[0];

            if (parameter.ParameterType != context.PType)
            {
                returnValue.Message = string.Format("type regist as decorate the construct method's param must be {0}", context.PType.Name);
                return returnValue;
            }

            returnValue.IsPass = true;
            return returnValue;

        }


        public object CreateInst(RegistObjectContext context, params object[] param)
        {
            object returnValue = null;
            if (!context.Params.ContainsKey(ContextParamNameEnum.TODECORATENAME))
            {
                throw new Exception("Error InstType");
            }
            Factory factory = Factory.GetInst();
            var toDecorateName = context.Params[ContextParamNameEnum.TODECORATENAME].ToString();
            var toDecorateObj = factory.Get(context.PType, toDecorateName);
            var instList = (List<Type>)context.Params[ContextParamNameEnum.INSTCHAIN];

            var diCreator = InstCreatorFactory.Create(Ctrip.SOA.Infratructure.IOCFactoryModel.InstType.Normal);

            foreach (var type in instList)
            {
                toDecorateObj = diCreator.CreateInst(new RegistObjectContext() { ObjType = type }, toDecorateObj);
            }
            returnValue = toDecorateObj;
            return returnValue;
        }
    }
}
