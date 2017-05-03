using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuKit.Solvers.LGP.AlgorithmModels.Crossover
{
    using System.Xml;
    using SimuKit.Solvers.LGP.ComponentModels;
    public abstract class LGPCrossoverInstruction
    {
        public LGPCrossoverInstruction()
        {

        }

        public LGPCrossoverInstruction(XmlElement xml_level1)
        {

        }

        public abstract void Crossover(LGPPop pop, LGPProgram child1, LGPProgram child2);
        public abstract LGPCrossoverInstruction Clone();
    }
}
