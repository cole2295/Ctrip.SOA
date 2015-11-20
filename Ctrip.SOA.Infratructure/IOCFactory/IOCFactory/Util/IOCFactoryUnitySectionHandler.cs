using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.RegistContextReader.UnityMappingFileReader.UnityModel;
using System.Xml.Serialization;
using System.IO;

namespace Ctrip.SOA.Infratructure.IOCFactory.Util
{
    public class IOCFactoryUnitySectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(section.OuterXml);
                    var reader = InstCreatorFactory.GetReader(Ctrip.SOA.Infratructure.IOCFactoryModel.Enum.FactoryMappingFilePattern.Unity);
                    sw.Flush();
                    ms.Position = 0;
                    return reader.GetMappingContexts(ms);
                }
            }

        }
    }
}
