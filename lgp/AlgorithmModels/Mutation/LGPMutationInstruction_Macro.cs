using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lgp;

namespace LGP.AlgorithmModels.Mutation
{
    
    using LGP.ComponentModels;
    using maths.Distribution;

    public class LGPMutationInstruction_Macro : LGPMutationInstruction
    {
	    private double _macroMutateInsertionRate;
	    private double _macroMutateDeletionRate;
	    private int _macroMutateMinProgramLength;
	    private int _macroMutateMaxProgramLength;
	    private bool _effectiveMutation;

        public LGPMutationInstruction_Macro()
        {
            _macroMutateInsertionRate = 0.5;
            _macroMutateDeletionRate=0.5;
            _effectiveMutation=false;
            _macroMutateMaxProgramLength=100;
            _macroMutateMinProgramLength=20;
        }

        public LGPMutationInstruction_Macro(LGPSchema lgp)
        {
	        _macroMutateInsertionRate = lgp.MacroMutateInsertionRate;
	        _macroMutateDeletionRate = lgp.MacroMutationDeletionRate;
	        _effectiveMutation = lgp.EffectiveMutation;
	        _macroMutateMaxProgramLength = lgp.MaxProgramLength;
	        _macroMutateMinProgramLength = lgp.MinProgramLength;

            _macroMutateInsertionRate /= (_macroMutateInsertionRate + _macroMutateDeletionRate);
            _macroMutateDeletionRate = 1 - _macroMutateInsertionRate;
        }

        public override void Mutate(LGPPop lgpPop, LGPProgram child)
        {
            // CSChen says:
	        // This is derived from Algorithm 6.1 (Section 6.2.1) of Linear Genetic Programming
	        // Macro instruction mutations either insert or delete a single instruction.
	        // In doing so, they change absolute program length with minimum step size on the 
	        // level of full instructions, the macro level. On the functional level , a single 
	        // node is inserted in or deleted from the program graph, together with all 
	        // its connecting edges.
	        // Exchanging an instruction or change the position of an existing instruction is not 
	        // regarded as macro mutation. Both of these variants are on average more 
	        // destructive, i.e. they imply a larger variation step size, since they include a deletion
	        // and an insertion at the same time. A further, but important argument against 
	        // substitutios of single instructions is that these do not vary program length. If
	        // single instruction would only be exchanged there would be no code growth.

	        double r=DistributionModel.GetUniform();
	        List<LGPInstruction> instructions=child.Instructions;
	        if(child.InstructionCount < _macroMutateMaxProgramLength && ((r < _macroMutateInsertionRate)  || child.InstructionCount == _macroMutateMinProgramLength))
	        {
		        LGPInstruction inserted_instruction=new LGPInstruction(child);
		        inserted_instruction.Create();
		        int loc=DistributionModel.NextInt(child.InstructionCount);
		        if(loc==child.InstructionCount - 1)
		        {
			        instructions.Add(inserted_instruction);
		        }
		        else
		        {
			        instructions.Insert(loc, inserted_instruction);
		        }

		        if(_effectiveMutation)
		        {
			        while(instructions[loc].IsConditionalConstruct && loc < instructions.Count)
			        {
				        loc++;
			        }
			        if(loc < instructions.Count)
			        {
				        HashSet<int> Reff=new HashSet<int>();
				        child.MarkStructuralIntrons(loc, Reff);
				        if(Reff.Count > 0)
				        {
                            int iRegisterIndex=-1;
					        foreach(int Reff_value in Reff)
					        {
						        if(iRegisterIndex==-1)
						        {
							        iRegisterIndex=Reff_value;
						        }
						        else if(DistributionModel.GetUniform() < 0.5)
						        {
							        iRegisterIndex=Reff_value;
						        }
					        }
					        instructions[loc].DestinationRegister=child.RegisterSet.FindRegisterByIndex(iRegisterIndex);
				        }
			        }
		        }
	        }
	        else if(child.InstructionCount > _macroMutateMinProgramLength && ((r > _macroMutateInsertionRate) || child.InstructionCount == _macroMutateMaxProgramLength))
	        {
		        int loc=DistributionModel.NextInt(instructions.Count);
		        if(_effectiveMutation)
		        {
			        for(int i=0; i<10; i++)
			        {
				        loc=DistributionModel.NextInt(instructions.Count);
				        if(! instructions[loc].IsStructuralIntron)
				        {
					        break;
				        }
			        }
		        }

		       
		        instructions.RemoveAt(loc);
	        }

	        child.TrashFitness();
        }

        public override LGPMutationInstruction Clone()
        {
            LGPMutationInstruction_Macro clone = new LGPMutationInstruction_Macro();
            clone._macroMutateInsertionRate = _macroMutateInsertionRate;
            clone._macroMutateDeletionRate = _macroMutateDeletionRate;
            clone._macroMutateMaxProgramLength = _macroMutateMaxProgramLength;
            clone._macroMutateMinProgramLength = _macroMutateMinProgramLength;
            clone._effectiveMutation = _effectiveMutation;

            return clone;
        }
    }
}
