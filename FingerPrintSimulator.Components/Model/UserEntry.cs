using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FingerPrintSimulator.Components.Model
{
    public class UserEntry
    {
        public string Name { get; set; }

        [RepositoryPrimaryKey]
        public string FingerPrint { get; set; }
    }
}
