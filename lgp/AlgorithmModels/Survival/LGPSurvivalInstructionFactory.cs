using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGP.AlgorithmModels.Survival
{
    
    using LGP.ComponentModels;

    public class LGPSurvivalInstructionFactory
    {
        private string mFilename;
        private LGPSurvivalInstruction mCurrentInstruction;

        public LGPSurvivalInstructionFactory(LGPSchema schema)
        {
            mFilename = filename;
            XmlDocument doc = new XmlDocument();
            doc.Load(mschema);
            XmlElement doc_root = doc.DocumentElement;
            string selected_strategy = doc_root.Attributes["strategy"].Value;
            foreach (LGPSchema schema in doc_root.ChildNodes)
            {
                if (xml_level1.Name == "strategy")
                {
                    string attrname = xml_level1.Attributes["name"].Value;
                    if (attrname == selected_strategy)
                    {
                        if (attrname == "compete")
                        {
                            mCurrentInstruction = new LgpSurvivalInstructionCompete(xml_level1);
                        }
                        else if (attrname == "probablistic")
                        {
                            mCurrentInstruction = new LgpSurvivalInstructionProbablistic(xml_level1);
                        }
                    }
                }
            }
        }

        public virtual LGPSurvivalInstructionFactory Clone()
        {
            LGPSurvivalInstructionFactory clone = new LGPSurvivalInstructionFactory(mschema);
            return clone;
        }

        public virtual LGPProgram Compete(LGPPop pop, LGPProgram weak_program_in_current_pop, LGPProgram child_program)
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.Compete(pop, weak_program_in_current_pop, child_program);
            }
            else
            {
                throw new ArgumentNullException();
            }
            
        }


        public override string ToString()
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.ToString();
            }
            return "LGP Survival Instruction Factory";
        }
    }
}
