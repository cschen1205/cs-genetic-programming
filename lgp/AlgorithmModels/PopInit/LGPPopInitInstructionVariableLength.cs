using System;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.PopInit
{
    using LGP.ComponentModels;
    using maths.Distribution;

    class LgpPopInitInstructionVariableLength : LGPPopInitInstruction
    {
        private int m_iInitialMaxProgLength;
        private int m_iInitialMinProgLength;

        public LgpPopInitInstructionVariableLength()
        {

        }

        public LgpPopInitInstructionVariableLength(LGPSchema schema)
        {
            m_iInitialMaxProgLength = schema.MaxProgramLength;
            m_iInitialMinProgLength = schema.MinProgramLength;
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
		        int iProgLength=m_iInitialMinProgLength + DistributionModel.NextInt(m_iInitialMaxProgLength - m_iInitialMinProgLength + 1);
                //Console.WriteLine("Prog Length: {0}", iProgLength);
		        LGPProgram lgp=pop.CreateProgram(iProgLength, pop.Environment);
		        pop.AddProgram(lgp);

                //Console.WriteLine("Min Length: {0}", m_iInitialMinProgLength);
                //Console.WriteLine("LGP: {0}", lgp.InstructionCount);

                if (lgp.InstructionCount < m_iInitialMinProgLength)
                {
                    throw new ArgumentNullException();
                }
                if (lgp.InstructionCount > m_iInitialMaxProgLength)
                {
                    throw new ArgumentNullException();
                }
	        }
        }

        public override LGPPopInitInstruction Clone()
        {
            LgpPopInitInstructionVariableLength clone = new LgpPopInitInstructionVariableLength();
            clone.m_iInitialMaxProgLength = m_iInitialMaxProgLength;
            clone.m_iInitialMinProgLength = m_iInitialMinProgLength;
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(">> Name: LGPPopInstruction_MaximumInitialization\n");
            sb.AppendFormat(">> Min Initial Program Length: {0}\n", m_iInitialMinProgLength);
            sb.AppendFormat(">> Max Initial Program Length: {0}", m_iInitialMaxProgLength);

            return sb.ToString();
        }
    }
}
