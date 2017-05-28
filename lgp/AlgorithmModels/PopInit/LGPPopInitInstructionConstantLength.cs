using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.PopInit
{
    
    using LGP.ComponentModels;

    class LgpPopInitInstructionConstantLength : LGPPopInitInstruction
    {
        private int mConstantProgramLength=100;

        public LgpPopInitInstructionConstantLength()
        {

        }

        public LgpPopInitInstructionConstantLength(LGPSchema schema)
        {
            mConstantProgramLength = schema.PopInitConstantProgramLength;
        }

        public override void Initialize(LGPPop pop)
        {
            // CSChen says:
	        // specified here is a variable length initialization that selects initial program
	        // lengths from a uniform distribution within a specified range of m_iInitialMinProgLength - m_iIinitialMaxProgLength
	        // the method is recorded in chapter 7 section 7.6 page 164 of Linear Genetic Programming 2004
	        int iPopulationSize=pop.PopulationSize;

	        // CSChen says:
	        // the program generated in this way will have program length as small as 
	        // iMinProgLength and as large as iMaxProgLength
	        // the program length is distributed uniformly between iMinProgLength and iMaxProgLength
	        for(int i=0; i<iPopulationSize; i++)
	        {
		        LGPProgram lgp=pop.CreateProgram(mConstantProgramLength, pop.Environment);
		        pop.AddProgram(lgp);
	        }
        }

        public override LGPPopInitInstruction Clone()
        {
            LgpPopInitInstructionConstantLength clone = new LgpPopInitInstructionConstantLength();
            clone.mConstantProgramLength = mConstantProgramLength;
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(">> Name: LGPPopInstruction_MaximumInitialization\n");
	        sb.AppendFormat(">> Constant Initial Program Length: {0}", mConstantProgramLength);

            return sb.ToString();
        }
    }
}
