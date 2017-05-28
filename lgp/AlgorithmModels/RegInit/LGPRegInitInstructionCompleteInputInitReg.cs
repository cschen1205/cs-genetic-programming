using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.RegInit
{
    
    using LGP.ComponentModels;
    using LGP.ProblemModels;

    class LgpRegInitInstructionCompleteInputInitReg : LGPRegInitInstruction
    {

        public LgpRegInitInstructionCompleteInputInitReg()
        {

        }

        public LgpRegInitInstructionCompleteInputInitReg(LGPSchema schema)
        {
            
        }

        public override void InitializeRegisters(LGPRegisterSet reg_set, LGPConstantSet constant_set, LGPFitnessCase fitness_case)
        {
            int iRegisterCount=reg_set.RegisterCount;
	        int iInputCount=fitness_case.GetInputCount();


	        int iRegisterIndex=0;
	        while(iRegisterIndex < iRegisterCount)
	        {
		        for(int j=0; j<iInputCount; ++j, ++iRegisterIndex)
		        {
			        if(iRegisterIndex >= iRegisterCount)
			        {
				        break;
			        }

			        double value;
			        fitness_case.QueryInput(j, out value);
			        reg_set.FindRegisterByIndex(iRegisterIndex).Value=value;
		        }
	        }

        }

        public override LGPRegInitInstruction Clone()
        {
            LgpRegInitInstructionCompleteInputInitReg clone = new LgpRegInitInstructionCompleteInputInitReg();
            return clone;
        }

        public override string ToString()
        {
            return ">> Name: LGPRegInitInstruction_CompleteInputInitReg";
        }
    }
}
