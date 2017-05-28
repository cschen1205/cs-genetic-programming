 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.RegInit
{
    
    using LGP.ComponentModels;
    using LGP.ProblemModels;

    class LgpRegInitInstructionStandard : LGPRegInitInstruction
    {
        private int mInputCopyCount=1;
        private double mDefaultRegisterValue=1;

        public LgpRegInitInstructionStandard()
        {

        }

        public LgpRegInitInstructionStandard(LGPSchema schema)
        {
	        mInputCopyCount = schema.RegInitInputCopyCount;
	        mDefaultRegisterValue = schema.RegInitDefaultRegisterValue;
        }

        public override void InitializeRegisters(LGPRegisterSet reg_set, LGPConstantSet constant_set, LGPFitnessCase fitness_case)
        {
            int iRegisterCount=reg_set.RegisterCount;
	        int iInputCount=fitness_case.GetInputCount();

        

	        int iRegisterIndex=0;
	        for(int i=0; i<mInputCopyCount; ++i)
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

		        if(iRegisterIndex >= iRegisterCount)
		        {
			        break;
		        }
	        }

	        while(iRegisterIndex < iRegisterCount)
	        {
		        reg_set.FindRegisterByIndex(iRegisterIndex).Value=mDefaultRegisterValue;
		        iRegisterIndex++;
	        }


        }

        public override LGPRegInitInstruction Clone()
        {
            LgpRegInitInstructionStandard clone = new LgpRegInitInstructionStandard();
            clone.mDefaultRegisterValue = mDefaultRegisterValue;
            clone.mInputCopyCount = mInputCopyCount;
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
            sb.AppendLine(">> Name: LGPRegInitInstruction_Standard");
            sb.AppendFormat(">> input copy count: {0}\n", mInputCopyCount);
            sb.AppendFormat(">> default register value: {0}", mDefaultRegisterValue);
            return sb.ToString();
        }
    }
}
