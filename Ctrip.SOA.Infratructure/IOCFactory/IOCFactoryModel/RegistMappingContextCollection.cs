﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.IOCFactoryModel
{
    public class RegistMappingContextCollection
    {
        public List<RegistMappingContext> Contexts { get; set; }

        public RegistMappingContextCollection()
        {
            Contexts = new List<RegistMappingContext>();
        }
    }
}
