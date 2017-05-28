using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.ProblemModels
{
    using LGP.ComponentModels;

    public class LGPEnvironment
    {
        public delegate int GetFitnessCaseCountHandle();
        public delegate LGPFitnessCase CreateFitnessCaseHandle(int i);

        public event GetFitnessCaseCountHandle GetFitnessCaseCountTriggered;
        public event CreateFitnessCaseHandle CreateFitnessCaseTriggered;

        public int GetFitnessCaseCount()
        {
            if (GetFitnessCaseCountTriggered != null)
            {
                return GetFitnessCaseCountTriggered();
            }
            return 0;
        }

        public LGPFitnessCase CreateFitnessCase(int index)
        {
            if (CreateFitnessCaseTriggered != null)
            {
                return CreateFitnessCaseTriggered(index);
            }
            return null;
        }

        public object Data = null;
        public LGPSchema Config = null;

        public LGPEnvironment(LGPSchema config) { Config = config; }
    }
}
