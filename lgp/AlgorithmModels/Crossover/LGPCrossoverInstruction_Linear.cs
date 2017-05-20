using System;
using System.Collections.Generic;
using System.Text;
using lgp;

namespace CSChen.LGP.AlgorithmModels.Crossover
{
    using CSChen.LGP.ComponentModels;
    using CSChen.Math.Distribution;

    // CSChen says:
    // this is derived from Algorithm 5.1 of Section 5.7.1 of Linear Genetic Programming
    // this linear crossover can also be considered as two-point crossover
    public class LGPCrossoverInstruction_Linear : LGPCrossoverInstruction
    {
        private int mMaxProgramLength;
        private int mMinProgramLength;
        private int mMaxSegmentLength;
        private int mMaxDistanceOfCrossoverPoints;
        private int mMaxDifferenceOfSegmentLength;

        public LGPCrossoverInstruction_Linear()
            : base()
        {
            mMaxProgramLength = 100;
            mMinProgramLength = 20;
            mMaxSegmentLength = 10;
            mMaxDistanceOfCrossoverPoints = 10;
            mMaxDifferenceOfSegmentLength = 5;
        }

        public LGPCrossoverInstruction_Linear(LGPSchema schema)
        {
            mMaxDifferenceOfSegmentLength = schema.MaxDifferenceOfSegmentLength;
            mMaxProgramLength = schema.MaxProgramLength;
            mMinProgramLength = schema.MinProgramLength;
            mMaxSegmentLength = schema.MaxSegmentLength;
            mMaxDistanceOfCrossoverPoints = schema.MaxDistanceOfCrossoverPoints;
        }

        public override LGPCrossoverInstruction Clone()
        {
            var clone = new LGPCrossoverInstruction_Linear
            {
                mMaxDifferenceOfSegmentLength = mMaxDifferenceOfSegmentLength,
                mMaxDistanceOfCrossoverPoints = mMaxDistanceOfCrossoverPoints,
                mMaxProgramLength = mMaxProgramLength,
                mMaxSegmentLength = mMaxSegmentLength,
                mMinProgramLength = mMinProgramLength
            };
            return clone;
        }

        public override void Crossover(LGPPop pop, LGPProgram child1, LGPProgram child2)
        {
            // CSChen says:
            // this implementation is derived from Algorithm 5.1 in Section 5.7.1 of Linear
            // Genetic Programming

            var gp1 = child1;
            var gp2 = child2;

            // length(gp1) <= length(gp2)
            if (gp1.InstructionCount > gp2.InstructionCount)
            {
                gp1 = child2;
                gp2 = child1;
            }

            // select i1 from gp1 and i2 from gp2 such that abs(i1-i2) <= max_crossover_point_distance
            // max_crossover_point_distance=min{length(gp1) - 1, m_max_distance_of_crossover_points}
            var i1 = DistributionModel.NextInt(gp1.InstructionCount);
            var i2 = DistributionModel.NextInt(gp2.InstructionCount);
            var cross_point_distance = (i1 > i2) ? (i1 - i2) : (i2 - i1);
            var max_crossover_point_distance = (gp1.InstructionCount - 1 > mMaxDistanceOfCrossoverPoints ? mMaxDistanceOfCrossoverPoints : gp1.InstructionCount - 1);
            while (cross_point_distance > max_crossover_point_distance)
            {
                i1 = DistributionModel.NextInt(gp1.InstructionCount);
                i2 = DistributionModel.NextInt(gp2.InstructionCount);
                cross_point_distance = (i1 > i2) ? (i1 - i2) : (i2 - i1);
            }

            var s1_max = (gp1.InstructionCount - i1) > mMaxDifferenceOfSegmentLength ? mMaxDifferenceOfSegmentLength : (gp1.InstructionCount - i1);
            var s2_max = (gp2.InstructionCount - i2) > mMaxDifferenceOfSegmentLength ? mMaxDifferenceOfSegmentLength : (gp2.InstructionCount - i2);

            // select s1 from gp1 (start at i1) and s2 from gp2 (start at i2)
            // such that length(s1) <= length(s2)
            // and abs(length(s1) - length(s2)) <= m_max_difference_of_segment_length)
            var ls1 = 1 + DistributionModel.NextInt(s1_max);
            var ls2 = 1 + DistributionModel.NextInt(s2_max);
            var lsd = (ls1 > ls2) ? (ls1 - ls2) : (ls2 - ls1);
            while ((ls1 > ls2) && (lsd > mMaxDifferenceOfSegmentLength))
            {
                ls1 = 1 + DistributionModel.NextInt(s1_max);
                ls2 = 1 + DistributionModel.NextInt(s2_max);
                lsd = (ls1 > ls2) ? (ls1 - ls2) : (ls2 - ls1);
            }

            if(((gp2.InstructionCount - (ls2-ls1)) < mMinProgramLength || ((gp1.InstructionCount+(ls2-ls1)) > mMaxProgramLength)))
            {
                if(DistributionModel.GetUniform()<0.5)
                {
                    ls2=ls1;
                }
                else
                {
                    ls1=ls2;
                }
                if((i1+ls1) > gp1.InstructionCount)
                {
                    ls1=ls2=gp1.InstructionCount-1;
                }
            }

            var instructions1=gp1.Instructions;
            var instructions2=gp2.Instructions;

            var instructions1_1=new List<LGPInstruction>();
            var instructions1_2=new List<LGPInstruction>();
            var instructions1_3=new List<LGPInstruction>();

            var instructions2_1=new List<LGPInstruction>();
            var instructions2_2=new List<LGPInstruction>();
            var instructions2_3=new List<LGPInstruction>();

            for(var i=0; i < i1; ++i)
            {
                instructions1_1.Add(instructions1[i]);
            }
            for(var i=i1; i < i1+ls1; ++i)
            {
                instructions1_2.Add(instructions1[i]);
            }
            for(int i=i1+ls1; i < instructions1.Count; ++i)
            {
                 instructions1_3.Add(instructions1[i]);
            }

            for(var i=0; i < i2; ++i)
            {
                instructions2_1.Add(instructions2[i]);
            }
            for(var i=i2; i < i2+ls2; ++i)
            {
                instructions2_2.Add(instructions2[i]);
            }
            for(var i=i2+ls2; i < instructions2.Count; ++i)
            {
                 instructions2_3.Add(instructions2[i]);
            }

            instructions1.Clear();
            instructions2.Clear();

            for(var i=0; i < i1; ++i)
            {
                instructions1.Add(instructions1_1[i]);
            }
            for(var i=0; i < ls2; ++i)
            {
                instructions1.Add(instructions2_2[i]);
                instructions2_2[i].Program=gp1;
            }
            for(var i=0; i < instructions1_3.Count; ++i)
            {
                instructions1.Add(instructions1_3[i]);
            }

            for(var i=0; i < i2; ++i)
            {
                instructions2.Add(instructions2_1[i]);
            }
            for(var i=0; i < ls1; ++i)
            {
                instructions2.Add(instructions1_2[i]);
                instructions1_2[i].Program=gp2;
            }
            for(var i=0; i < instructions2_3.Count; ++i)
            {
                instructions2.Add(instructions2_3[i]);
            }

            gp1.TrashFitness();
            gp2.TrashFitness();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(">> Name: LGPCrossoverInstruction_Linear\n");
            sb.AppendFormat(">> Max Program Length: {0}\n", mMaxProgramLength);
            sb.AppendFormat(">> Min Program Length: {0}\n", mMinProgramLength);
            sb.AppendFormat(">> Max Distance of Crossover Points: {0}\n", mMaxDistanceOfCrossoverPoints);
            sb.AppendFormat(">> Max Difference in Segment Length: {0}", mMaxDifferenceOfSegmentLength);

            return sb.ToString();
        }
    }
}
