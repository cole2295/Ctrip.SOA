using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ctrip.SOA.Infratructure.IOCFactoryModel.Interface
{
    public interface IRegistContextReader
    {
        RegistMappingContextCollection GetMappingContexts(string fileUrl);

        RegistMappingContextCollection GetMappingContexts(Stream stream);
    }
}
