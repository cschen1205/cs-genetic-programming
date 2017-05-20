﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace maths.SpecialFunctions
{
    public class LogitFunction
    {
        public static double GetLogit(double p)
        {
            return System.Math.Log(p / (1 - p));
        }
    }
}
