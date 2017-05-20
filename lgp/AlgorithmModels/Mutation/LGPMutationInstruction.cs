﻿namespace LGP.AlgorithmModels.Mutation
{
    using ComponentModels;

    public abstract class LGPMutationInstruction
    {
        public LGPMutationInstruction()
        {

        }

        public LGPMutationInstruction(XmlElement xml_level1)
        {

        }

        public virtual void Mutate(LGPPop pop, LGPProgram child1, LGPProgram child2)
        {
            Mutate(pop, child1);
            Mutate(pop, child2);
        }

        public abstract void Mutate(LGPPop lgpPop, LGPProgram child);

        public abstract LGPMutationInstruction Clone();
    }
}
