using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSChen.LGP.AlgorithmModels.PopInit
{
    using System.Xml;
    using CSChen.LGP.ComponentModels;

    public abstract class LGPPopInitInstruction
    {
        public LGPPopInitInstruction()
        {

        }

        public LGPPopInitInstruction(XmlElement xml_level1)
        {

        }

        public abstract void Initialize(LGPPop pop);
        public abstract LGPPopInitInstruction Clone();
    }
}
