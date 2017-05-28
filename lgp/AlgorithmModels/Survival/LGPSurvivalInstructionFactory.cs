using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Survival
{
    
    using LGP.ComponentModels;

    public class LGPSurvivalInstructionFactory
    {
        private LGPSchema _schema;
        private LGPSurvivalInstruction mCurrentInstruction;

        public LGPSurvivalInstructionFactory(LGPSchema schema)
        {
            _schema = schema;

            LGPSchema.SurvivalType strategy = schema.Survival;
            if (strategy == LGPSchema.SurvivalType.complete)
            {
                mCurrentInstruction = new LgpSurvivalInstructionCompete(schema);
            }
            else
            {
                mCurrentInstruction = new LgpSurvivalInstructionProbablistic(schema);
            }
            
        }

        public virtual LGPSurvivalInstructionFactory Clone()
        {
            LGPSurvivalInstructionFactory clone = new LGPSurvivalInstructionFactory(_schema);
            return clone;
        }

        public virtual LGPProgram Compete(LGPPop pop, LGPProgram weak_program_in_current_pop, LGPProgram child_program)
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.Compete(pop, weak_program_in_current_pop, child_program);
            }
            else
            {
                throw new ArgumentNullException();
            }
            
        }


        public override string ToString()
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.ToString();
            }
            return "LGP Survival Instruction Factory";
        }
    }
}
