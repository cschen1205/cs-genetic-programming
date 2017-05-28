using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.RegInit
{
    
    using LGP.ComponentModels;
    using LGP.ProblemModels;

    public class LGPRegInitInstructionFactory
    {
        private LGPRegInitInstruction mCurrentInstruction;
        private LGPSchema schema;

        public LGPRegInitInstructionFactory(LGPSchema schema)
        {
            this.schema = schema;
            LGPSchema.RegInitType strategy = schema.RegInit;

            if (strategy == LGPSchema.RegInitType.complete)
            {
                mCurrentInstruction = new LgpRegInitInstructionCompleteInputInitReg(schema);
            } else if (strategy == LGPSchema.RegInitType.standard)
            {
                mCurrentInstruction = new LgpRegInitInstructionStandard(schema);
            }
        }

        public virtual LGPRegInitInstructionFactory Clone()
        {
            LGPRegInitInstructionFactory clone = new LGPRegInitInstructionFactory(schema);
            return clone;
        }

        public virtual void InitializeRegisters(LGPRegisterSet reg_set, LGPConstantSet constant_set, LGPFitnessCase fitness_case)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.InitializeRegisters(reg_set, constant_set, fitness_case);
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
            return "LGP Reg Init Instruction Factory";
        }
    }
}
