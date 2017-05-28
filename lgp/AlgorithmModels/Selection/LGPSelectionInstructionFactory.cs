using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Selection
{
    
    using LGP.ComponentModels;

    public class LGPSelectionInstructionFactory
    {
        private LGPSchema _schema;
        private LGPSelectionInstruction mCurrentInstruction;

        public LGPSelectionInstructionFactory(LGPSchema schema)
        {
            _schema = schema;
            mCurrentInstruction = new LgpSelectionInstructionTournament(schema);
        }

        public virtual LGPSelectionInstructionFactory Clone()
        {
            LGPSelectionInstructionFactory clone = new LGPSelectionInstructionFactory(_schema);
            return clone;
        }

        public virtual void Select(LGPPop pop, ref KeyValuePair<LGPProgram, LGPProgram> best_pair, ref KeyValuePair<LGPProgram, LGPProgram> worst_pair)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.Select(pop, ref best_pair, ref worst_pair);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public virtual LGPProgram Select(LGPPop pop)
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.Select(pop);
            }
            return null;
        }

        public override string ToString()
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.ToString();
            }
            return "LGP Selection Instruction Factory";
        }
    }
}
