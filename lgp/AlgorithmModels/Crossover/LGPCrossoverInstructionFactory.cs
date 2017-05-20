using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace CSChen.LGP.AlgorithmModels.Crossover
{
    using ComponentModels;

    public class LGPCrossoverInstructionFactory
    {
        private LGPCrossoverInstruction worker;
        private readonly LGPSchema schema;

        public LGPCrossoverInstructionFactory(LGPSchema schema)
        {
            this.schema = schema;
        }

        public virtual LGPCrossoverInstructionFactory Clone()
        {
            var clone = new LGPCrossoverInstructionFactory(schema);
            return clone;
        }

        public void Crossover(LGPPop pop, LGPProgram child1, LGPProgram child2)
        {
            if (worker == null)
            {
                var attrname = schema.Crossover;
                switch (attrname)
                {
                    case LGPSchema.CrossoverType.linear:
                        worker = new LGPCrossoverInstruction_Linear(schema);
                        break;
                    case LGPSchema.CrossoverType.one_point:
                        worker = new LGPCrossoverInstruction_OnePoint(schema);
                        break;
                    case LGPSchema.CrossoverType.one_seg:
                        worker = new LGPCrossoverInstruction_OneSegment(schema);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            worker.Crossover(pop, child1, child2);
        }

        public override string ToString()
        {
            return worker.ToString();
        }
    }
}
