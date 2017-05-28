using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;
using LGP.AlgorithmModels.Survival;

namespace LGP.AlgorithmModels.Crossover
{
    
    using LGP.ComponentModels;
    public abstract class LGPCrossoverInstruction
    {
        public LGPCrossoverInstruction()
        {

        }

        public LGPCrossoverInstruction(LGPSchema schema)
        {

        }

        public abstract void Crossover(LGPPop pop, LGPProgram child1, LGPProgram child2);
        public abstract LGPCrossoverInstruction Clone();
    }
}
