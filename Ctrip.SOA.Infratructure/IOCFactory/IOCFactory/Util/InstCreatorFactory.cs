using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.IOCFactoryModel;
using Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.InstCreator;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Interface;
using Ctrip.SOA.Infratructure.IOCFactoryModel.Enum;
using Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.RegistContextReader;

namespace Ctrip.SOA.Infratructure.IOCFactory.Util
{
    internal class InstCreatorFactory
    {
        private Dictionary<InstType, IInstCreator> dic;

        private Dictionary<FactoryMappingFilePattern, IRegistContextReader> readerDic;

        private static InstCreatorFactory factory;

        private static InstCreatorFactory GetInst()
        {
            if (factory == null)
            {
                factory = new InstCreatorFactory();
            }
            return factory;
        }

        public static IInstCreator Create(InstType instType)
        {
            var factory = GetInst();
            return factory._Create(instType);
        }

        public static IRegistContextReader GetReader(FactoryMappingFilePattern pattern)
        {
            return InstCreatorFactory.GetInst()._GetReader(pattern);
        }

        private IRegistContextReader _GetReader(FactoryMappingFilePattern pattern)
        {
            return readerDic[pattern];
        }

        private InstCreatorFactory()
        {
            dic = new Dictionary<InstType, IInstCreator>();
            readerDic = new Dictionary<FactoryMappingFilePattern, IRegistContextReader>();
            Init();
        }

        private void Init()
        {
            dic.Add(InstType.Normal, new Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.LambdaInstCreator.NormalInstCreator());
            dic.Add(InstType.Singleton, new SingletonInstCreator());
            dic.Add(InstType.DI, new DIInstCreator());
            dic.Add(InstType.Decorate, new DecorateInstCreator());
            dic.Add(InstType.DISingleton, new DISingletonInstCreator());

            readerDic.Add(FactoryMappingFilePattern.Normal, new NormalReader());
            readerDic.Add(FactoryMappingFilePattern.Unity, new Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.RegistContextReader.UnityMappingFileReader.Reader());
        }


        private IInstCreator _Create(InstType instType)
        {
            return dic[instType];
        }        

    }
}
