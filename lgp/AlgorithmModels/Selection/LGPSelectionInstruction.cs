﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Selection
{
    
    using LGP.ComponentModels;

    public abstract class LGPSelectionInstruction
    {
        public LGPSelectionInstruction()
        {

        }

        public LGPSelectionInstruction(LGPSchema schema)
        {

        }

        public abstract LGPProgram Select(LGPPop pop);
        public abstract void Select(LGPPop lgpPop, ref KeyValuePair<LGPProgram, LGPProgram> best_pair, ref KeyValuePair<LGPProgram, LGPProgram> worst_pair);
        public abstract LGPSelectionInstruction Clone();
    }
}
