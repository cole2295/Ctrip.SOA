﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.IOCFactory.Model.Imp.RegistContextReader.UnityMappingFileReader.UnityModel
{
    [XmlRoot(ElementName = "unity", Namespace = "")]
    public class Unity
    {        
        [XmlElement(ElementName = "containers")]
        public Containers Containers { get; set; }

        [XmlElement(ElementName = "typeAliases")]
        public TypeAliases TypeAliases { get; set; }
    }
}
