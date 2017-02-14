﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DScheduler.Framework.Helpers
{
    public class ReadOnlyStoredProcedureSection : ConfigurationElement
    {
        [ConfigurationProperty("storedProcedureName", IsRequired = true)]
        public string Name
        {
            get { return (string)this["storedProcedureName"]; }
        }
    }
}
