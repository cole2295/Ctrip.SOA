﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.RegistContextReader.UnityMappingFileReader.UnityModel
{
    [XmlRoot(ElementName = "containers", Namespace = "")]
    public class Containers
    {
        [XmlElement(ElementName = "container")]
        public Container[] Objects { get; set; }
    }
}
