//IGTMarkovMath.h
////////////////////////////////////////////
//IGT Markov Math Library				////
//Original Author: Dustin Stewart		////
//Version 1.3							////
//Release Date 7/28/2009				////
////////////////////////////////////////////
///////////////////////////////////////////////////////
//History:
//Version	Date		Author		Changes
//1.0		2/26/2009	D. Stewart	*Original Release
//1.1		3/10/2009	D. Stewart	*Updated to work with older compilers 
//									(removed abstract keyword, and updated some ">>" to "> >").
//									*Made BaseGraph constructor and destructor protected to 
//									emphasize its use as a base class.
//									*Added use of auto_ptr to addVertex to avoid memory leaks.
//									*Changed methods that pass a _NameType to use const reference passing 
//									(makes it more efficient when _NameType is a class).
//									*Added makeshift defn for _ASSERTE for older compilers.
//									*Added _ASSERTE error for arcs with invalid vertices
//1.2		5/7/2009	D. Stewart	*Changed print functions to pass strings by constant reference for efficiency.
//									*Added IGT_ARC_CHECK_ON definition to toggle arc checking during debug of addArc 
//									(it was causing some performance issuse to run map.find for every arc in large graphs).  
//1.3		7/28/2009	D. Stewart	*Removed function markovOptimizationTA and related functions.
///////////////////////////////////////////////////////

namespace MarkovLibraryCSharp
{
    //Arc class that also includes a pay variable 
    public class PayArc <_VertexType>
	{
		public double _probability;
		public _VertexType _neighbor;
		public double _pay;
		//Constructor 
		public PayArc(_VertexType neighbor, double probability =0, double pay =0) 
		{
		
		//CHECK PROBABILITY IS >=0 and <=1
			if (probability >= 0 && probability <= 1)
			{
				_probability = probability;
				_pay = pay;
				_neighbor = neighbor;
			}
			else
            {
				System.Console.WriteLine($"Probability not in range: {probability}");
            }
		}
	}
}
