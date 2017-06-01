using System;
using System.Collections;
using System.Collections.Generic;
using lgp;
using LGP.ComponentModels;
using LGP.ComponentModels.Operators;
using LGP.ProblemModels;
using Xunit;

namespace lgp_unit_test
{
    public class MexicanHatFitnessCase : LGPFitnessCase
    {
        private double mX1;
        private double mX2;
        private double mY;
        private double mComputedY;

        public double ComputedY
        {
            get { return mComputedY; }
        }

        public double X1
        {
            get { return mX1; }
            set { mX1 = value; }
        }

        public double X2
        {
            get { return mX2; }
            set { mX2 = value; }
        }

        public double Y
        {
            get { return mY; }
            set { mY = value; }
        }

        public override void RunLGPProgramCompleted(double[] result)
        {
            mComputedY = result[0];
        }

        public override bool QueryInput(int index, out double input)
        {
            input = 0;
            if (index == 0)
            {
                input = mX1;
                return true;
            }
            else if (index == 1)
            {
                input = mX2;
                return true;
            }
            
            return false;
        }


        public override int GetInputCount()
        {
            return 1;
        }


    }
    
    public class MexicanHatUnitTest
    {
        static double FunctionXY(double x1, double x2)
        {
            return (1 - x1 * x1 / 4 - x2 * x2 / 4) * System.Math.Exp(- x1 * x2 / 8 - x2 * x2 / 8);
        }

        static List<LGPFitnessCase> LoadData()
        {
            
            List<LGPFitnessCase> result = new List<LGPFitnessCase>();

            double lower_bound=-4;
            double upper_bound=4;
            int period=16;

            double interval=(upper_bound - lower_bound) / period;

            for(int i=0; i<period; i++)
            {
                double x1=lower_bound + interval * i;
                for(int j=0; j<period; j++)
                {
                    double x2=lower_bound + interval * j;
                    var fc = new MexicanHatFitnessCase();
                    fc.X1 = x1;
                    fc.X2 = x2;
                    fc.Y = FunctionXY(x1, x2);
                    result.Add(fc);
                }
            }


            return result;
        }
        
        [Fact]
        public void TestSymbolicRegression()
        {
            List<LGPFitnessCase> table = new List<LGPFitnessCase>();
            LGPSchema config = new LGPSchema();

            LGPPop pop = new LGPPop(config);
            pop.OperatorSet.AddOperator(new LGPOperator_Plus());
            pop.OperatorSet.AddOperator(new LGPOperator_Minus());
            pop.OperatorSet.AddOperator(new LGPOperator_Division());
            pop.OperatorSet.AddOperator(new LGPOperator_Multiplication());
            pop.OperatorSet.AddOperator(new LGPOperator_Power());
            pop.OperatorSet.AddIfltOperator();

            pop.CreateFitnessCase += (index) => table[index];

            pop.GetFitnessCaseCount += () => table.Count;

            pop.EvaluateFitnessFromAllCases += (fitness_cases) =>
            {
                double fitness = 0;
                for (int i = 0; i < fitness_cases.Count; i++)
                {
                    MexicanHatFitnessCase fitness_case = (MexicanHatFitnessCase)fitness_cases[i];
                    double correct_y = fitness_case.Y;
                    double computed_y = fitness_case.ComputedY;
                    fitness += (correct_y - computed_y) * (correct_y - computed_y);
                }

                return fitness;
            };


            pop.BreedInitialPopulation();


            while (!pop.IsTerminated)
            {
                pop.Evolve();
                Console.WriteLine("Mexican Hat Symbolic Regression Generation: {0}", pop.CurrentGeneration);
                Console.WriteLine("Global Fitness: {0}\tCurrent Fitness: {1}", pop.GlobalBestProgram.Fitness.ToString("0.000"), pop.FindFittestProgramInCurrentGeneration().Fitness.ToString("0.000"));
            }

        }
    }
}