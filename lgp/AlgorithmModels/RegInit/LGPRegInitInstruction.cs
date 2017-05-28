using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.RegInit
{
    
    using LGP.ComponentModels;
    using LGP.ProblemModels;

    public abstract class LGPRegInitInstruction
    {
        public LGPRegInitInstruction()
        {

        }

        public LGPRegInitInstruction(LGPSchema schema)
        {

        }

        public abstract void InitializeRegisters(LGPRegisterSet reg_set, LGPConstantSet constant_set, LGPFitnessCase fitness_case);
        public abstract LGPRegInitInstruction Clone();
    }
}
