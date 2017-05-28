using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.PopInit
{
    
    using LGP.ComponentModels;

    public class LGPPopInitInstructionFactory
    {
        private string mFilename;
        private LGPPopInitInstruction mCurrentInstruction;
        private LGPSchema schema;

        public LGPPopInitInstructionFactory(LGPSchema lgp)
        {
            LGPSchema.PopInitType attrname = lgp.PopInit;

            switch (attrname)
            {
                case LGPSchema.PopInitType.variable_length:
                    mCurrentInstruction = new LgpPopInitInstructionVariableLength(lgp);
                    break;
                case LGPSchema.PopInitType.constant_length:
                    mCurrentInstruction = new LgpPopInitInstructionConstantLength(lgp);
                    break;
            }
        }

        public virtual LGPPopInitInstructionFactory Clone()
        {
            LGPPopInitInstructionFactory clone = new LGPPopInitInstructionFactory(schema);
            return clone;
        }

        public virtual void Initialize(LGPPop pop)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.Initialize(pop);
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
            return "LGP Pop Init Instruction Factory";
        }
    }
}
