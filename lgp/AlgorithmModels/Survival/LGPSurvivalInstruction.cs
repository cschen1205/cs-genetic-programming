using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGP.AlgorithmModels.Survival
{
    
    using LGP.ComponentModels;

    public abstract class LGPSurvivalInstruction
    {
        public LGPSurvivalInstruction()
        {

        }

        public LGPSurvivalInstruction(LGPSchema schema)
        {

        }

        // CSChen says:
        // this method return the pointer of the program that is to be deleted (loser in the competition for survival)
        public abstract LGPProgram Compete(LGPPop pop, LGPProgram weak_program_in_current_pop, LGPProgram child_program);
        public abstract LGPSurvivalInstruction Clone();
    }
}
