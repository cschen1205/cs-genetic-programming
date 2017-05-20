using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Mutation
{
    using LGP.ComponentModels;

    public class LGPMutationInstructionFactory
    {
        private string mFilename;
        private LGPMutationInstruction mCurrentMacroMutation;
        private LGPSchema schema;

        public LGPMutationInstructionFactory(LGPSchema lgp)
        {
            schema = lgp;
            mCurrentMacroMutation = new LGPMutationInstruction_Macro(lgp);
        }

        public virtual LGPMutationInstructionFactory Clone()
        {
            LGPMutationInstructionFactory clone = new LGPMutationInstructionFactory(schema);
            return clone;
        }

        public void Mutate(LGPPop pop, LGPProgram child1, LGPProgram child2)
        {
            if (mCurrentMacroMutation != null)
            {
                mCurrentMacroMutation.Mutate(pop, child1, child2);
            }
        }

        public void Mutate(LGPPop pop, LGPProgram child)
        {
            if (mCurrentMacroMutation != null)
            {
                mCurrentMacroMutation.Mutate(pop, child);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override string ToString()
        {
            if (mCurrentMacroMutation != null)
            {
                return mCurrentMacroMutation.ToString();
            }
            return "Mutation Instruction Factory";
        }

    }
}
