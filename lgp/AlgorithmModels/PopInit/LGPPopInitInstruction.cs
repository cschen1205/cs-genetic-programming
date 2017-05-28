using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.PopInit
{
    
    using LGP.ComponentModels;

    public abstract class LGPPopInitInstruction
    {
        public LGPPopInitInstruction()
        {

        }

        public LGPPopInitInstruction(LGPSchema schema)
        {

        }

        public abstract void Initialize(LGPPop pop);
        public abstract LGPPopInitInstruction Clone();
    }
}
