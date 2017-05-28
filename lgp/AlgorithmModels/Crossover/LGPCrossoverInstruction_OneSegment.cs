using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Crossover
{
    using System.Xml;
    using LGP.ComponentModels;
    using maths.Distribution;

    /* CSChen says:
       This operator is derived from Algorithm 5.3 in Section 5.7.3 of Linear Genetic Programming
    */

    /* CSChen says: (From Section 5.7.3 of Linear Genetic Programming
       Crossover requires, by definition, that information is exchanged between individual programs.
       However, an exchange always includes two operations on an individual, the deletion and
       the insertion of a subprogram. The imperative program representation allows instructions to be 
       deleted without replacement since instructon operands, e.g. register pointers, are always defined.
       Instructions may also be inserted at any position without a preceding deletion, at least if the maximum 
       program length is not exceeded. 

       If we want linear crossover to be less disruptive it may be a good idea to execute only one operation per
       individual. this consideration motivates a one-segment or one-way recombination of linear genetic
       programs as described by Algorithm 5.3.

       Standard linear crossover may also be refered to as two-segment recombinations, in these terms.
    */
    public class LGPCrossoverInstruction_OneSegment : LGPCrossoverInstruction
    {
        private int mMaxProgramLength;
        private int mMinProgramLength;
        private int mMaxSegmentLength;
        private double mInsertionProbability;

        public LGPCrossoverInstruction_OneSegment()
            : base()
        {
            mMaxProgramLength = 200;
            mMinProgramLength = 1;
            mMaxSegmentLength = 10;
            mInsertionProbability = 0.5;
        }

	    public LGPCrossoverInstruction_OneSegment(LGPSchema lgp)
	    {
		    mMaxProgramLength = lgp.MaxProgramLength;
		    mMinProgramLength = lgp.MinProgramLength;
		    mMaxSegmentLength = lgp.MaxSegmentLength;
		    mInsertionProbability = lgp.InsertionProbabilityInCrossover;
	    }

        public override LGPCrossoverInstruction Clone()
        {
            LGPCrossoverInstruction_OneSegment clone = new LGPCrossoverInstruction_OneSegment();
            clone.mMaxSegmentLength = mMaxSegmentLength;
            clone.mInsertionProbability = mInsertionProbability;
            clone.mMaxProgramLength = mMaxProgramLength;
            clone.mMinProgramLength = mMinProgramLength;
            return clone;
        }

        public override void Crossover(LGPPop pop, LGPProgram child1, LGPProgram child2)
        {
            CrossoverOneSegment(child1, child2);
            CrossoverOneSegment(child2, child1);
        }

        private void CrossoverOneSegment(LGPProgram gp1, LGPProgram gp2)
        {
            double prob_r = DistributionModel.GetUniform();
            if((gp1.InstructionCount < mMaxProgramLength) && ((prob_r <= mInsertionProbability || gp1.InstructionCount==mMinProgramLength)))
            {
                int i1=DistributionModel.NextInt(gp1.InstructionCount);
                int max_segment_length=gp2.InstructionCount < mMaxSegmentLength ? gp2.InstructionCount : mMaxSegmentLength;
                int ls2=1+DistributionModel.NextInt(max_segment_length);
                if(gp1.InstructionCount+ls2 > mMaxProgramLength)
                {
                    ls2=mMaxProgramLength-gp1.InstructionCount;
                }
                int i2=DistributionModel.NextInt(gp2.InstructionCount-ls2);
                
                List<LGPInstruction> instructions1=gp1.Instructions;
                List<LGPInstruction> instructions2=gp2.Instructions;

                List<LGPInstruction> s=new List<LGPInstruction>();
		        for(int i=i2; i != (i2+ls2); ++i)
		        {
                    LGPInstruction instruction=instructions2[i];
			        LGPInstruction instruction_cloned=instruction.Clone();
			        instruction_cloned.Program=gp1;
			        s.Add(instruction_cloned);
		        }

		        instructions1.InsertRange(i1, s.AsEnumerable());
            }

            if((gp1.InstructionCount > mMinProgramLength) && ((prob_r > mInsertionProbability) || gp1.InstructionCount == mMaxProgramLength))
	        {
		        int max_segment_length=(gp2.InstructionCount < mMaxSegmentLength) ? gp2.InstructionCount : mMaxSegmentLength;
		        int ls1=1+DistributionModel.NextInt(max_segment_length);

		        if(gp1.InstructionCount < ls1)
		        {
			        ls1=gp1.InstructionCount - mMinProgramLength;
		        }
		        else if(gp1.InstructionCount - ls1 < mMinProgramLength)
		        {
			        ls1=gp1.InstructionCount - mMinProgramLength;
		        }
		        int i1=DistributionModel.NextInt(gp1.InstructionCount-ls1);
		        List<LGPInstruction> instructions1=gp1.Instructions;

                instructions1.RemoveRange(i1, ls1);
	        }

	        gp1.TrashFitness();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(">> Name: LGPCrossoverInstruction_Linear\n");
            sb.AppendFormat(">> Max Program Length: {0}\n", mMaxProgramLength);
            sb.AppendFormat(">> Min Program Length: {0}\n", mMinProgramLength);
            sb.AppendFormat(">> Max Segment Length: {0}\n", mMaxSegmentLength);
            sb.AppendFormat(">> Insertion Probability: {0}", mInsertionProbability);

            return sb.ToString();
        }
    }
}
