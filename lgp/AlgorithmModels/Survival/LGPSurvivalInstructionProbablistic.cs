using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Survival
{
    
    using ComponentModels;
    using maths.Distribution;

    class LgpSurvivalInstructionProbablistic : LGPSurvivalInstruction
    {
        private double m_reproduction_probability = 1;

        public LgpSurvivalInstructionProbablistic()
        {

        }

        public LgpSurvivalInstructionProbablistic(LGPSchema schema)
        {
            m_reproduction_probability = schema.ReproductionProbability;
        }

        public override LGPProgram Compete(LGPPop pop, LGPProgram weak_program_in_current_pop, LGPProgram child_program)
        {
            double r = DistributionModel.GetUniform();

            if (r < m_reproduction_probability)
            {
                //Console.WriteLine("replacing...");
                pop.Replace(weak_program_in_current_pop, child_program);
                return weak_program_in_current_pop;
            }

            return child_program;
        }

        public override LGPSurvivalInstruction Clone()
        {
            LgpSurvivalInstructionProbablistic clone = new LgpSurvivalInstructionProbablistic();
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(">> Name: LGPSurvivalInstruction_Probablistic");
            sb.AppendFormat(">> Reproduction Probability: {0}", m_reproduction_probability);

            return sb.ToString();
        }
    }
}
