using System;

namespace lgp
{
    public class LGPSchema
    {
        public enum CrossoverType
        {
            linear,
            one_point,
            one_seg
        }

        public enum PopInitType
        {
            variable_length,
            constant_length
        }

        public enum RegInitType
        {
            complete,
            standard
        }


        public CrossoverType Crossover = CrossoverType.linear;
        public PopInitType PopInit { get; set; } = PopInitType.variable_length;
        public RegInitType RegInit { get; set; } = RegInitType.complete;

        public int MaxDifferenceOfSegmentLength { get; set; } = 10;
        public int MaxProgramLength { get; set; } = 100;
        public int MinProgramLength { get; set; } = 20;
        public int MaxSegmentLength { get; set; } = 10;
        public int MaxDistanceOfCrossoverPoints { get; set; } = 5;
        public double InsertionProbabilityInCrossover { get; set; } = 0.5;
        public double MacroMutateInsertionRate { get; set; } = 0.5;
        public double MacroMutationDeletionRate { get; set; } = 0.5;
        public bool EffectiveMutation { get; set; } = false;

        public int PopInitConstantProgramLength { get; set; } = 100;

        public int TournamentSize { get; set; } = 5;

        public int RegInitInputCopyCount { get; set; } = 1;
        public double RegInitDefaultRegisterValue { get; set; } = 1;


    }
}
