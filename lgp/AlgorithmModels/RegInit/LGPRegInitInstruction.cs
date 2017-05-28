﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGP.AlgorithmModels.RegInit
{
    
    using LGP.ComponentModels;
    using LGP.ProblemModels;

    public abstract class LGPRegInitInstruction
    {
        public LGPRegInitInstruction()
        {

        }

        public LGPRegInitInstruction(XmlElement xml_level1)
        {

        }

        public abstract void InitializeRegisters(LGPRegisterSet reg_set, LGPConstantSet constant_set, LGPFitnessCase fitness_case);
        public abstract LGPRegInitInstruction Clone();
    }
}
