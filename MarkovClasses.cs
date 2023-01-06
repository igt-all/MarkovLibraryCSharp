using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MarkovLibraryCSharp
{

    //	public class TransitionGraphPlus<_NameType> : BaseGraph<_NameType, double, VertexAP>
    //	{
    //		private void markovProbabilityIteration()
    //		{
    //			std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.ProbInnerProduct<VertexSet_Type.value_type, MarkovFunctors.ArcTimesProb<PayArc<VertexAP>>>());
    //		}
    //		private void markovPayIteration()
    //		{
    //			std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.PayInnerProduct<VertexSet_Type.value_type, MarkovFunctors.ArcExp>());
    //		}
    //		private void updatePays()
    //		{
    //			std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.UpdatePays<VertexSet_Type.value_type>());
    //		}
    //		//add an arc
    //		public void addArc(_NameType tail, _NameType head, double probability, double pay)
    //		{
    //#if ARC_CHECK_ON
    //			Debug.Assert(_vertexMap.find(tail) != _vertexMap.end() && _vertexMap.find(head) != _vertexMap.end());
    //#endif
    //			_vertexMap[head].addArcToNeighborhood(_vertexMap[tail], probability, pay);
    //		}
    //		//perform one iteration of model with arcs that pay
    //		public void markovPay()
    //		{
    //			markovProbabilityIteration(); //sum_{(u,v)}p(u,v)*pv
    //			markovPayIteration(); //sum_{(u,v)}p(u,v)*(xu+pu*x(uv))
    //			updateProbs(); //set pv = v.tempProbability
    //			updatePays(); //set xv = v.tempPay
    //		}
    //		//performs numIterations iterations of the "arcs that pay" model
    //		public void markovPay(int numIterations)
    //		{
    //			Debug.Assert(numIterations >= 0);
    //			for (int i = 0; i != numIterations; ++i)
    //			{
    //				markovProbabilityIteration(); //sum_{(u,v)}p(u,v)*pv
    //				markovPayIteration(); //sum_{(u,v)}p(u,v)*(xu+pu*x(uv))
    //				updateProbs(); //set pv = v.tempProbability
    //				updatePays(); //set xv = v.tempPay
    //			}
    //		}
    //		//Return the current expected value of the graph
    //		public double expectedValue()
    //		{
    //			double temp = 0;
    //			VertexSet_Type.iterator temp_itr = new VertexSet_Type.iterator();
    //			//sum_{v in V(D)} xv
    //			for (temp_itr = _vertexSet.begin(); temp_itr != _vertexSet.end(); ++temp_itr)
    //			{
    //				temp += temp_itr._pay;
    //			}
    //			return temp;
    //		}
    //		//Reduce a multi-graph to a simple graph 
    //		public void simplifyGraph()
    //		{
    //			//objects
    //			VertexSet_Type.iterator vertexItr = new VertexSet_Type.iterator();
    //			VertexSet_Type.iterator endVertex = _vertexSet.end();
    //			List<PayArc<VertexAP>>.Enumerator curArc1;
    //			List<PayArc<VertexAP>>.Enumerator curArc2;
    //			List<PayArc<VertexAP>>.Enumerator endArc;
    //			List<PayArc<VertexAP>> tempVec = new List<PayArc<VertexAP>>();
    //			double tempPay;
    //			double tempProb;
    //			VertexAP tempNeighbor; //create tempNeighborhood
    //			MarkovFunctors.ClearNeighborhood<VertexSet_Type.value_type, PayArc<VertexAP>> CNF = new MarkovFunctors.ClearNeighborhood<VertexSet_Type.value_type, PayArc<VertexAP>>();

    //			//algorithm
    //			for (vertexItr = _vertexSet.begin(); vertexItr != endVertex; ++vertexItr)
    //			{
    //				//sort neighborhood by neighbor
    //				sort(vertexItr._neighborhood.begin(), vertexItr._neighborhood.end(), MarkovFunctors.SortArcByNeighbor<PayArc<VertexAP>>());
    //				curArc1 = curArc2 = vertexItr._neighborhood.begin();
    //				//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
    //				//ORIGINAL LINE: endArc = (*vertexItr)->_neighborhood.end();
    //				endArc.CopyFrom(vertexItr._neighborhood.end());
    //				tempVec.Clear();
    //				//partition arcs into sets S1,S2,...Sk
    //				while (curArc2 != endArc)
    //				{
    //					//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //					while (curArc2 != endArc && (curArc1)._neighbor == (curArc2)._neighbor)
    //					{
    //						//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //						++curArc2; //advance
    //					}
    //					tempPay = tempProb = 0;
    //					//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //					tempNeighbor = (curArc1)._neighbor;
    //					while (curArc1 != curArc2)
    //					{
    //						//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //						tempProb += (curArc1)._probability; //sum_{a in Si} pa
    //															//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //						tempPay += (curArc1)._pay * (curArc1)._probability; //sum_{a in Si} xa
    //																			//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
    //						++curArc1;
    //					}
    //					//add new arc with pa = tempProb and xa = tempPay/tempProb
    //					tempVec.Add(new PayArc<VertexAP>(tempNeighbor, tempProb, (tempPay / tempProb)));
    //				}
    //				CNF(*vertexItr); //delete all old arcs
    //								 //set neighborhood to tempNeighborhood
    //				vertexItr._neighborhood.insert(vertexItr._neighborhood.begin(), tempVec.GetEnumerator(), tempVec.end());
    //			}
    //		}
    //	}

    //	public class OptimizationGraph<_NameType, double> : BaseGraph<_NameType, double, StrategyVertex>
    //	{
    //		private void optimizationIteration()
    //		{
    //			foreach ()
    //				foreach (_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.PayInnerProduct<VertexSet_Type.value_type, MarkovFunctors.ArcTimesPay<StrategyVertex.Arc<Vertex>>>());
    //		}
    //		private void updatePays()
    //		{
    //			std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.OptiPayUpdate<VertexSet_Type.value_type>());
    //		}
    //		//get original vertex pay 
    //		public double getVertexOriginalPay(_NameType name)
    //		{
    //			return _vertexMap[name]._originalPay;
    //		}
    //		//overload get vertex pay
    //		public double getVertexPay(_NameType name)
    //		{
    //			return _vertexMap[name]._pay;
    //		}
    //		//add arc to graph
    //		public void addArc(_NameType tail, _NameType head, double probability)
    //		{
    //#if ARC_CHECK_ON
    //			Debug.Assert(_vertexMap.find(tail) != _vertexMap.end() && _vertexMap.find(head) != _vertexMap.end());
    //#endif
    //			_vertexMap[tail].addArcToNeighborhood(_vertexMap[head], probability);
    //		}
    //		//perform one iteration of the optimization model
    //		public void markovOptimization()
    //		{
    //			optimizationIteration(); //sum_{(u,v)}p(u,v)xv
    //			updatePays(); //update pay and strategy
    //		}
    //		//perform numIteratinos of the optimization model
    //		public void markovOptimization(int numIterations)
    //		{
    //			Debug.Assert(numIterations >= 0);
    //			for (int i = 0; i != numIterations; ++i)
    //			{
    //				optimizationIteration(); //sum_{(u,v)}p(u,v)xv
    //				updatePays(); //update pay and strategy
    //			}
    //		}
    //		//Returns the current expected value
    //		public double expectedValue()
    //		{
    //			double temp = 0;
    //			return std::accumulate(_vertexSet.begin(), _vertexSet.end(), temp, MarkovFunctors.VertexPayback<VertexSet_Type.value_type>());
    //		}
    //		//Prints the current optimal strategy to fout
    //		public void printCurOptimalStrategy(std::ofstream fout, string firstSepr, string secondSepr)
    //		{
    //			VertexMap_Type.iterator vertItr = new VertexMap_Type.iterator();
    //			VertexMap_Type.iterator endVert = _vertexMap.end();
    //			for (vertItr = _vertexMap.begin(); vertItr != endVert; ++vertItr)
    //			{
    //				fout << vertItr.first << firstSepr << vertItr.second._strategy << secondSepr;
    //			}
    //		}
    //	}
    namespace MarkovFunctors
    {
        static class MarkovFunctors
        { 
            ////////////////////////////////////////
            //Functor for sorting arcs by neighbor 

            public class SortArcByNeighbor<_ArcType> where _ArcType : IArc<_VertexType> where _VertexType : IVertex
            {
                //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
                //ORIGINAL LINE: result_type operator ()(first_argument_type& left, second_argument_type& right) const
                public static result_type functorMethod(first_argument_type left, second_argument_type right)
                {
                    return (left._neighbor < right._neighbor);
                }
            }
            ///////////////////////////////////////////////////////////////
            ///Specialized Inner Product Function Objects
            //////////////////////////////////////////////////////////////
            public delegate double ProbInnerProduct<_Ty, _Functor> (_Ty vertex,ProbInnerProduct<_Ty, _Functor> func1);
        
            public static double ProbInnerProductMethod<_Ty, _Functor> (_Ty vertex, ProbInnerProduct<_Ty, _Functor> itr)
            {
                    double temp = 0;
                    return vertex._tempProbability = std::accumulate(itr._neighborhood.begin(), itr._neighborhood.end(), temp, default(_Functor));
            
            }
            //public class ProbInnerProduct<_Ty, _Functor> : std::unary_function<_Ty, void>
            //{
            //    public static result_type functorMethod(argument_type itr)
            //    {
            //        double temp = 0;
            //        itr._tempProbability = std::accumulate(itr._neighborhood.begin(), itr._neighborhood.end(), temp, default(_Functor));
            //    }
            //}

            public class PassProbInnerProduct<_Ty, _Functor, _argTy> : std::unary_function<_Ty, void>
            {
                private _argTy _arg = new _argTy();
                public PassProbInnerProduct(_argTy arg)
                {
                    _arg = arg;
                }
                public static result_type functorMethod(argument_type itr)
                {
                    double temp = 0;
                    itr._tempProbability = std::accumulate(itr._neighborhood.begin(), itr._neighborhood.end(), temp, _Functor(_arg));
                }
            }
            //C++ TO C# CONVERTER WARNING: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
            //ORIGINAL LINE: template<class _Ty, class _Functor>
            public class PayInnerProduct<_Ty, _Functor> : std::unary_function<_Ty, void>
            {
                public static result_type functorMethod(argument_type itr)
                {
                    double temp = 0;
                    itr._tempPay = std::accumulate(itr._neighborhood.begin(), itr._neighborhood.end(), temp, default(_Functor));
                }
            }

            ////////////////////////////////////////////////
            //Functors for Inner Products
            //////////////////////////////////////////////

            public delegate double ArcTimesProb<IArc<_VertexType>>(double tempProb, IArc<_VectorType> arc);
            public static double ArcTimesProbMethod<IArc<_VertexType>>(double tempProb, IArc<_VectorType> arc)
            {
                    return tempProb + (arc._probability* arc._neighbor._probability);
            }
	        //public class ArcTimesProb<Arc<IVertex>> : std::binary_function<double, Arc<Vertex>*, double> // Binary function provides these types <first_argument_type,second_argument_type,result_type>
	        //{
	        //		//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
	        //		//ORIGINAL LINE: result_type operator ()(const first_argument_type& tempProb, const second_argument_type& arc) const
	        //	public static result_type functorMethod(first_argument_type tempProb, second_argument_type arc)
	        //    {
	        //	    return tempProb + (arc._probability * arc._neighbor._probability);
	        //    }
         //   }
            //Arcs that pay model 
            public class ArcExp : std::binary_function<double, PayArc<VertexAP>*, double>
            {
	            public static result_type functorMethod(first_argument_type tempPay, second_argument_type arc)
	            {
		            return tempPay + (arc._probability * ((arc._neighbor._pay) + (arc._neighbor._probability) * (arc._pay))); //calc
	            }
            }
        //Traditional dot product on pay
        public class ArcTimesPay<Arc<Vertex>> : std::binary_function<double, Arc<Vertex>*, double>
		        {
	        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
	        //ORIGINAL LINE: result_type operator ()(const first_argument_type& tempPay, const second_argument_type& arc) const
	        public static result_type functorMethod(first_argument_type tempPay, second_argument_type arc)
	        {
		        return (tempPay + arc._probability * arc._neighbor._pay); //value of moving ahead
	        }
        }

            //traditional dot product excluding any vertex with a given pay
            public class ExcludeTimes<Arc<Vertex>, double > : std::binary_function<double, Arc<Vertex>*, double>
		    {
			    private double _excludePay = new double();
			    public ExcludeTimes(double pay)
			    {
				    _excludePay = pay;
			    }
			    //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
			    //ORIGINAL LINE: result_type operator ()(const first_argument_type& tempProb, const second_argument_type& arc) const
			    public static result_type functorMethod(first_argument_type tempProb, second_argument_type arc)
			    {
				    return ((arc._neighbor._pay != _excludePay) ? (tempProb + (arc._probability * arc._neighbor._probability)) : (tempProb));
			    }
		    }
		    ////////////////////////////////////////
		    //Update functions
		    ////////////////////////////////////////
	    public class UpdateProbs<_Ty> : std::unary_function<_Ty, void>
	    {
		    public static result_type functorMethod(argument_type itr)
		    {
			    itr._probability = itr._tempProbability;
		    }
	    }

        public class UpdatePays<_Ty> : std::unary_function<_Ty, void>
        {
	        public static result_type functorMethod(argument_type itr)
	        {
		        itr._pay = itr._tempPay;
	        }
        }
        public class OptiPayUpdate<_Ty> : std::unary_function<_Ty, void>
        {
	        public static result_type functorMethod(argument_type itr)
	        {
		        if (!(itr._pay > itr._tempPay)) //optimal to move forward, update _pay
		        {
			        itr._pay = itr._tempPay;
			        itr._strategy = true;
		        }
		        else
		        {
			        itr._strategy = false;
		        }
	        }
        }
        ///////////////////////////////////////////////////////
        ///Delete and reset functions
        /////////////////////////////////////////////////////
        public class ClearNeighborhood<_Ty, Arc<Vertex>> : std::unary_function<_Ty, void>
        {
	        public static result_type functorMethod(argument_type itr)
	        {
		        List<Arc<Vertex>>.Enumerator arcItr;
		        List<Arc<Vertex>>.Enumerator end = itr._neighborhood.end();
		        for (arcItr = itr._neighborhood.begin(); arcItr != end; ++arcItr)
		        {
			        //deallocate arc
			        //C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
			        arcItr = null;
		        }
		        //clear neighborhood vector
		        itr._neighborhood.clear();
	        }
        }   

        static System.Action<_VertexType> ResetVertex<_Ty> ()
        {
		        itr.reset();	
        }

        ///////////////////////////////////////////////////////////////////
        ////Calcualates _pay*_probability of a vertex and adds it to temp
        //////////////////////////////////////////////////////////////////
        public class VertexPayback<_Ty> : std::binary_function<double, _Ty, double>
        {
	        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
	        //ORIGINAL LINE: result_type operator ()(first_argument_type& temp, second_argument_type& itr) const
	        public static result_type functorMethod(first_argument_type temp, second_argument_type itr)
	        {
		        return (temp + (double)(itr._pay) * itr._probability);
	        }
        }
        //////////////////////////////////////////////////////////
        /////For calculating max pay and max pay odds 
        //////////////////////////////////////////////////////////
        public class FindVertexMaxPay<_Ty, double> : std::binary_function<double, _Ty, double>
        {
            public static result_type functorMethod(first_argument_type curMax, second_argument_type itr)
            {
                return ((curMax < (itr.getPay())) ? itr.getPay() : curMax);
            }
        }
        public class CollectLikeProbs<_Ty, double> : std::binary_function<double, _Ty, double>
        {
	        private double _payToFind = new double();
	        public CollectLikeProbs(double pay)
	        {
		        _payToFind = pay;
	        }
	        public static result_type functorMethod(first_argument_type temp, second_argument_type itr)
	        {
		        return temp + ((itr._probability > 0 && itr.getPay() == _payToFind) ? (itr._probability) : (0));
	        }
        }

    //perform P=P+sum_{v in V(D) pv*p^0_v)

        public class OptiProbSum<_Ty, _NameType, double> : std::binary_function<double, _Ty, double>
        {
	        private SortedDictionary<_NameType, double> _startingProbs = new SortedDictionary<_NameType, double>();
	        public OptiProbSum(SortedDictionary<_NameType, double> startingProbs)
	        {
		        _startingProbs = new SortedDictionary<_NameType, double>(startingProbs);
	        }
	        public static result_type functorMethod(first_argument_type temp, second_argument_type itr)
	        {
		        return (temp + (itr.second._probability * _startingProbs[itr.first]));
	        }
        }
	}
    }
}
