# cs-genetic-programming

Genetic programming framework implemented in C-sharp and .NET Core



Add the following dependency to your POM file:

```bash
Install-Package lgp
```

# Features

* Linear Genetic Programming

    - Initialization
    
       + Full Register Array 
       + Fixed-length Register Array
   
    - Crossover
     
        + Linear
        + One-Point
        + One-Segment
    
    - Mutation
   
        + Micro-Mutation
        + Effective-Macro-Mutation
        + Macro-Mutation
    
    - Replacement
   
        + Tournament
        + Direct-Compete
    
    - Default-Operators
   
        + Most of the math operators
        + if-less, if-greater
        + Support operator extension
        


    
Future Works

* Tree Genetic Programming
* Grammar-based Genetic Programming
* Gene Expression Programming



# Usage of Linear Genetic Programming

### Create training data

The sample code below shows how to generate data from the "Mexican Hat" regression problem:

```cs
 public static List<LGPFitnessCase> mexican_hat()
{
    List<LGPFitnessCase> result = new List<LGPFitnessCase>();

    Func<Double, Double, Double> mexican_hat_func = (x1, x2) => (1 - x1 * x1 / 4 - x2 * x2 / 4) * Math.Exp(-x1 * x2 / 8 - x2 * x2 / 8);

    double lower_bound = -4;
    double upper_bound = 4;
    int period = 16;

    double interval = (upper_bound - lower_bound) / period;

    for (int i = 0; i < period; i++)
    {
        double x1 = lower_bound + interval * i;
        for (int j = 0; j < period; j++)
        {
            double x2 = lower_bound + interval * j;

            MexicanHatFitnessCase observation = new MexicanHatFitnessCase();

            observation.X1 = x1;
            observation.X2 = x2;
            observation.Y = mexican_hat_func(x1, x2);

            result.Add(observation);
        }
    }

    return result;
}
```


 
The sample code below shows how the LGP can be created and trained:

```cs
List<LGPFitnessCase> table = mexican_hat();
LGPSchema config = new LGPSchema();

LGPPop pop = new LGPPop(config);
pop.OperatorSet.AddOperator(new LGPOperator_Plus());
pop.OperatorSet.AddOperator(new LGPOperator_Minus());
pop.OperatorSet.AddOperator(new LGPOperator_Division());
pop.OperatorSet.AddOperator(new LGPOperator_Multiplication());
pop.OperatorSet.AddOperator(new LGPOperator_Power());
pop.OperatorSet.AddIfltOperator();

config.ConstantRegisters.Add(new KeyValuePair<double, double>(1, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(2, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(3, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(4, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(5, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(6, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(7, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(8, 1));
config.ConstantRegisters.Add(new KeyValuePair<double, double>(9, 1));

config.RegisterCount = 6;


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

```



