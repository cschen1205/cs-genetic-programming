using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Survival
{
    
    using ComponentModels;

    class LgpSurvivalInstructionCompete : LGPSurvivalInstruction
    {
        public LgpSurvivalInstructionCompete()
        {

        }

        public LgpSurvivalInstructionCompete(LGPSchema schema)
        {
            
        }

        public override LGPProgram Compete(LGPPop pop, LGPProgram weak_program_in_current_pop, LGPProgram child_program)
        {
            if (child_program.IsBetterThan(weak_program_in_current_pop))
            {
                pop.Replace(weak_program_in_current_pop, child_program);
                return weak_program_in_current_pop;
            }
            return child_program;
        }

        public override LGPSurvivalInstruction Clone()
        {
            LgpSurvivalInstructionCompete clone = new LgpSurvivalInstructionCompete();
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(">> Name: LGPSurvivalInstruction_Compete");

            return sb.ToString();
        }
    }
}
