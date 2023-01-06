using System.Collections.Generic;
using IGTMarkovMath.MarkovFunctors;
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



namespace IGTMarkovMath
{
    //Vertex class for use in transition graphs
    //Vertex class for transition graph plus

    //Vertex class for optimization graphs
    ///The Base Graph class from which all other graphs are derived 

	public class BaseGraph <_NameType, _PayType, _VertexType>
	{
		protected BaseGraph()
		{
		}
		//vertex map type
		//vertex set type
		//map used for constructing and examining graph
		protected Dictionary<_NameType,_VertexType> _vertexMap = new Dictionary<_NameType,_VertexType>();
		//vector used in algorithms
		protected List<_VertexType> _vertexSet = new List<_VertexType>();
		//assign tempProbabilities to probabilities in any model
		protected void updateProbs()
		{
			_vertexSet.ForEach(new MarkovFunctors.UpdateProbs<List<_VertexType*>.value_type>());
		}
		//Destructor
		public void Dispose()
		{
			//Delete the arcs
			clearArcSet();
			//deallocate the vertices
			List<_VertexType>.Enumerator vertItr;
			List<_VertexType>.Enumerator end = _vertexSet.end();
			for (vertItr = _vertexSet.GetEnumerator(); vertItr != end; ++vertItr)
			{
				//deallocate vertex
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				vertItr = null;
			}
			//clear the sets (not really necessary but eh, why not)
			_vertexMap.Clear();
			_vertexSet.Clear();
		}
		//Add a vertex to the graph
		public void addVertex(_NameType name, double probability)
		{
			addVertex(name, probability, 0);
		}
		public void addVertex(_NameType name)
		{
			addVertex(name, 0, 0);
		}
//C++ TO C# CONVERTER NOTE: Overloaded method(s) are created above to convert the following method having default parameters:
//ORIGINAL LINE: void addVertex(const _NameType &name, double probability=0, _PayType pay=0)
		public void addVertex(_NameType name, double probability, _PayType pay)
		{
#if _ASSERTE_ConditionalDefinition1
			x(probability >= 0 && probability <= 1);
#else
			_ASSERTE(probability >= 0 && probability <= 1);
#endif
			std.auto_ptr<_VertexType> vert = new std.auto_ptr<_VertexType>(new _VertexType(probability,pay));
			if (_vertexMap.insert(Dictionary<_NameType,_VertexType*>.value_type(name,vert.get())).second)
			{
				_vertexSet.Add(vert.release());
			}
		}
		//Set the probability of a vertex
		public void setVertexProb(_NameType name, double probability)
		{
#if _ASSERTE_ConditionalDefinition1
			x(probability >= 0 && probability <= 1);
#else
			_ASSERTE(probability >= 0 && probability <= 1);
#endif
			_vertexMap[name]._probability = probability;
		}
		//Set the pay of a vertex
		public void setVertexPay(_NameType name, _PayType pay)
		{
			_vertexMap[name].setPay(pay);
		}
		//Get the current probability of a vertex
		public double getVertexProb(_NameType name)
		{
			return _vertexMap[name]._probability;
		}
		//Get the current pay of a vertex
		public _PayType getVertexPay(_NameType name)
		{
			return _vertexMap[name]._pay;
		}
		//Remove all Arcs from the graph 
		public void clearArcSet()
		{
			_vertexSet.ForEach(new MarkovFunctors.ClearNeighborhood<List<_VertexType*>.value_type,_VertexType._ArcType>());
		}
		//Reset all vertex values
		public void resetVertexValues()
		{
			_vertexSet.ForEach(new MarkovFunctors.ResetVertex<List<_VertexType*>.value_type>());
		}
		//print current graph probabilities 
		public void printProbabilities(std.ofstream fout, string firstSepr, string secondSepr)
		{
			Dictionary<_NameType,_VertexType>.Enumerator vertItr;
			Dictionary<_NameType,_VertexType>.Enumerator endVert = _vertexMap.end();
			for (vertItr = _vertexMap.GetEnumerator(); vertItr != endVert; ++vertItr)
			{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				fout << vertItr.first << firstSepr << vertItr.second._probability << secondSepr;
			}
		}
		//print current graph pays
		public void printPays(std.ofstream fout, string firstSepr, string secondSepr)
		{
			Dictionary<_NameType,_VertexType>.Enumerator vertItr;
			Dictionary<_NameType,_VertexType>.Enumerator endVert = _vertexMap.end();
			for (vertItr = _vertexMap.GetEnumerator(); vertItr != endVert; ++vertItr)
			{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				fout << vertItr.first << firstSepr << vertItr.second._pay << secondSepr;
			}
		}
		//Reduce a multi-graph to a simple graph 
		public void simplifyGraph()
		{
			//objects
			List<_VertexType>.Enumerator vertexItr;
			List<_VertexType>.Enumerator endVertex = _vertexSet.end();
			List<_VertexType._ArcType>.Enumerator curArc1;
			List<_VertexType._ArcType>.Enumerator curArc2;
			List<_VertexType._ArcType>.Enumerator endArc;
			List<_VertexType._ArcType> tempVec = new List<_VertexType._ArcType>(); //create tempNeighborhood
			double tempPay;
			double tempProb;
			_VertexType tempNeighbor;
			MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type,_VertexType._ArcType> CNF = new MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type,_VertexType._ArcType>();

			//algorithm
			for (vertexItr = _vertexSet.GetEnumerator(); vertexItr != endVertex; ++vertexItr)
			{
				//sort neighborhood by neighbor to speed up algorithm
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				sort((vertexItr)._neighborhood.begin(),(vertexItr)._neighborhood.end(),new MarkovFunctors.SortArcByNeighbor<_VertexType._ArcType>());
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				curArc1 = curArc2 = (vertexItr)._neighborhood.begin();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				endArc = (vertexItr)._neighborhood.end();
				tempVec.Clear(); //init tempneighborhood
				//partition neighborhood into sets S1,S2,...,Sk taking advantage of sorting above
				while (curArc2 != endArc)
				{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
					while (curArc2 != endArc && (curArc1)._neighbor == (curArc2)._neighbor)
					{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						++curArc2; //advance
					}
					tempPay = tempProb = 0;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
					tempNeighbor = (curArc1)._neighbor;
					while (curArc1 != curArc2)
					{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						tempProb += (curArc1)._probability; //set temp prob = sum_{a in Si}pa
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						++curArc1;
					}
					//create new, equivalent simple arc in tempNeighborhood
					tempVec.Add(new _VertexType._ArcType(tempNeighbor, tempProb));
				}
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				CNF(vertexItr); //delete all old arcs
				//replace neighborhood with simple arcs
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				(vertexItr)._neighborhood.insert((vertexItr)._neighborhood.begin(), tempVec.GetEnumerator(),tempVec.end());
			}
		}
	}
	//Constructs Clasic Transition Graphs and perform Markov operations
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename _NameType, typename _PayType>
	public class TransitionGraph <_NameType, _PayType>: BaseGraph<_NameType,_PayType,Vertex<_PayType>>
	{
		//one iteration of a Markov Chain
		private void markovProbabilityIteration()
		{
			_vertexSet.ForEach(new MarkovFunctors.ProbInnerProduct<List<_VertexType*>.value_type, MarkovFunctors.ArcTimesProb<Vertex<_PayType>._ArcType>>());
		}
		//add an arc
		public void addArc(_NameType tail, _NameType head, double probability)
		{
			#if IGT_ARC_CHECK_ON
#if _ASSERTE_ConditionalDefinition1
			x(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#else
			_ASSERTE(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#endif
			#endif
			_vertexMap[head].addArcToNeighborhood(_vertexMap[tail],probability);
		}
		//Performs one iteration of a standard Markov Chain
		public void markovProbability()
		{
			markovProbabilityIteration();
			updateProbs();
		}
		//Performs numIterations iterations of a standard Markov Chain
		public void markovProbability(int numIterations)
		{
#if _ASSERTE_ConditionalDefinition1
			x(numIterations >= 0);
#else
			_ASSERTE(numIterations >= 0);
#endif
			for (int i = 0; i != numIterations; ++i)
			{
				markovProbabilityIteration();
				updateProbs();
			}
		}
		//Returns the current expected value of the graph
		public double expectedValue()
		{
			double temp = 0;
			return std.accumulate(_vertexSet.GetEnumerator(),_vertexSet.end(),temp,new MarkovFunctors.VertexPayback<List<_VertexType*>.value_type>());
		}
		//Finds the probability of hitting a vertex with given pay at least once in numIterations
		public double givenPayProbability(int numIterations, _PayType pay)
		{
#if _ASSERTE_ConditionalDefinition1
			x(numIterations >= 1);
#else
			_ASSERTE(numIterations >= 1);
#endif

			double toReturn = 0; //Set P=0
			markovProbability(); //perform one iteration of Alg 5
			toReturn += curPayProbability(pay); //P=P+sum_{v:xv=pay} pv
			for (int i = 1; i != numIterations; ++i)
			{
				//v._tempProbability = sum_{(u,v):xu neq pay}p(u,v)pu
				_vertexSet.ForEach(new MarkovFunctors.PassProbInnerProduct<List<_VertexType*>.value_type, MarkovFunctors.ExcludeTimes<Vertex<_PayType>._ArcType,_PayType>,_PayType>(pay));
				updateProbs(); //pv=v._tempProbability
				toReturn += curPayProbability(pay); //P=P+sum_{v:xv=pay}pv
			}
			return toReturn; //return P
		}
		//Returns highest vertex pay, and probability of hitting it at least once in numIterations
		public double maxPayProbability(int numIterations, ref _PayType maxPay)
		{
#if _ASSERTE_ConditionalDefinition1
			x(numIterations >= 1);
#else
			_ASSERTE(numIterations >= 1);
#endif

			_PayType temp = 0;
			//set maxPay = max{xv:v in V(D)}
			maxPay = std.accumulate(_vertexSet.GetEnumerator(), _vertexSet.end(),temp,new MarkovFunctors.FindVertexMaxPay<List<_VertexType*>.value_type,_PayType>());
			//perform alg 7
			return givenPayProbability(numIterations, maxPay);
		}
		//returns the current probability of any vertex with given pay
		public double curPayProbability(_PayType pay)
		{
			double temp = 0; //return sum_{v in V(D):xv=pay}pv
			return std.accumulate(_vertexSet.GetEnumerator(),_vertexSet.end(),temp,new MarkovFunctors.CollectLikeProbs<List<_VertexType*>.value_type,_PayType>(pay));
		}
		//returns the current probability of any vertex with max pay
		public double curMaxPayProbability()
		{
			_PayType temp = 0; //Set MP = max{xv:v in V(D)}
			_PayType maxPay = std.accumulate(_vertexSet.GetEnumerator(), _vertexSet.end(),temp,new MarkovFunctors.FindVertexMaxPay<List<_VertexType*>.value_type,_PayType>());
			//return sum_{v:xv=pay}pv
			return curPayProbability(maxPay);
		}
	}
	//Construct a transition graph plus for models where arcs pay
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename _NameType>
	public class TransitionGraphPlus <_NameType>: BaseGraph<_NameType,double,VertexAp>
	{
		private void markovProbabilityIteration()
		{
			_vertexSet.ForEach(new MarkovFunctors.ProbInnerProduct<List<_VertexType*>.value_type, MarkovFunctors.ArcTimesProb<PayArc<VertexAp>> >());
		}
		private void markovPayIteration()
		{
			_vertexSet.ForEach(new MarkovFunctors.PayInnerProduct<List<_VertexType*>.value_type, MarkovFunctors.ArcExp>());
		}
		private void updatePays()
		{
			_vertexSet.ForEach(new MarkovFunctors.UpdatePays<List<_VertexType*>.value_type>());
		}
		//add an arc
		public void addArc(_NameType tail, _NameType head, double probability, double pay)
		{
			#if IGT_ARC_CHECK_ON
#if _ASSERTE_ConditionalDefinition1
			x(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#else
			_ASSERTE(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#endif
			#endif
			_vertexMap[head].addArcToNeighborhood(_vertexMap[tail],probability,pay);
		}
		//perform one iteration of model with arcs that pay
		public void markovPay()
		{
			markovProbabilityIteration(); //sum_{(u,v)}p(u,v)*pv
			markovPayIteration(); //sum_{(u,v)}p(u,v)*(xu+pu*x(uv))
			updateProbs(); //set pv = v.tempProbability
			updatePays(); //set xv = v.tempPay
		}
		//performs numIterations iterations of the "arcs that pay" model
		public void markovPay(int numIterations)
		{
#if _ASSERTE_ConditionalDefinition1
			x(numIterations >= 0);
#else
			_ASSERTE(numIterations >= 0);
#endif
			for (int i = 0; i != numIterations; ++i)
			{
				markovProbabilityIteration(); //sum_{(u,v)}p(u,v)*pv
				markovPayIteration(); //sum_{(u,v)}p(u,v)*(xu+pu*x(uv))
				updateProbs(); //set pv = v.tempProbability
				updatePays(); //set xv = v.tempPay
			}
		}
		//Return the current expected value of the graph
		public double expectedValue()
		{
			double temp = 0;
			List<_VertexType>.Enumerator temp_itr;
			//sum_{v in V(D)} xv
			for (temp_itr = _vertexSet.GetEnumerator(); temp_itr.MoveNext();)
			{
				temp += (temp_itr.Current)._pay;
			}
			return temp;
		}
		//Reduce a multi-graph to a simple graph 
		public void simplifyGraph()
		{
			//objects
			List<_VertexType>.Enumerator vertexItr;
			List<_VertexType>.Enumerator endVertex = _vertexSet.end();
			List<PayArc<VertexAp>>.Enumerator curArc1;
			List<PayArc<VertexAp>>.Enumerator curArc2;
			List<PayArc<VertexAp>>.Enumerator endArc;
			List<PayArc<VertexAp>> tempVec = new List<PayArc<VertexAp>>();
			double tempPay;
			double tempProb;
			VertexAp tempNeighbor; //create tempNeighborhood
			MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type,PayArc<VertexAp>> CNF = new MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type,PayArc<VertexAp>>();

			//algorithm
			for (vertexItr = _vertexSet.GetEnumerator(); vertexItr != endVertex; ++vertexItr)
			{
				//sort neighborhood by neighbor
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				sort((vertexItr)._neighborhood.begin(),(vertexItr)._neighborhood.end(),new MarkovFunctors.SortArcByNeighbor<PayArc<VertexAp>>());
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				curArc1 = curArc2 = (vertexItr)._neighborhood.begin();
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				endArc = (vertexItr)._neighborhood.end();
				tempVec.Clear();
				//partition arcs into sets S1,S2,...Sk
				while (curArc2 != endArc)
				{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
					while (curArc2 != endArc && (curArc1)._neighbor == (curArc2)._neighbor)
					{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						++curArc2; //advance
					}
					tempPay = tempProb = 0;
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
					tempNeighbor = (curArc1)._neighbor;
					while (curArc1 != curArc2)
					{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						tempProb += (curArc1)._probability; //sum_{a in Si} pa
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						tempPay += (curArc1)._pay * (curArc1)._probability; //sum_{a in Si} xa
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
						++curArc1;
					}
					//add new arc with pa = tempProb and xa = tempPay/tempProb
					tempVec.Add(new PayArc<VertexAp>(tempNeighbor, tempProb, (tempPay / tempProb)));
				}
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				CNF(vertexItr); //delete all old arcs
				//set neighborhood to tempNeighborhood
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				(vertexItr)._neighborhood.insert((vertexItr)._neighborhood.begin(), tempVec.GetEnumerator(),tempVec.end());
			}
		}
	}
	//Construct an optimization graph for TAKE/GO strategy models
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename _NameType, typename _PayType>
	public class OptimizationGraph <_NameType, _PayType>: BaseGraph<_NameType,_PayType,StrategyVertex<_PayType>>
	{
		private void optimizationIteration()
		{
			_vertexSet.ForEach(new MarkovFunctors.PayInnerProduct<List<_VertexType*>.value_type, MarkovFunctors.ArcTimesPay<StrategyVertex<_PayType>._ArcType>>());
		}
		private void updatePays()
		{
			_vertexSet.ForEach(new MarkovFunctors.OptiPayUpdate<List<_VertexType*>.value_type>());
		}
		//get original vertex pay 
		public _PayType getVertexOriginalPay(_NameType name)
		{
			return _vertexMap[name]._originalPay;
		}
		//overload get vertex pay
		public double getVertexPay(_NameType name)
		{
			return _vertexMap[name]._pay;
		}
		//add arc to graph
		public void addArc(_NameType tail, _NameType head, double probability)
		{
			#if IGT_ARC_CHECK_ON
#if _ASSERTE_ConditionalDefinition1
			x(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#else
			_ASSERTE(_vertexMap.ContainsKey(tail) && _vertexMap.ContainsKey(head));
#endif
			#endif
			_vertexMap[tail].addArcToNeighborhood(_vertexMap[head],probability);
		}
		//perform one iteration of the optimization model
		public void markovOptimization()
		{
			optimizationIteration(); //sum_{(u,v)}p(u,v)xv
			updatePays(); //update pay and strategy
		}
		//perform numIteratinos of the optimization model
		public void markovOptimization(int numIterations)
		{
#if _ASSERTE_ConditionalDefinition1
			x(numIterations >= 0);
#else
			_ASSERTE(numIterations >= 0);
#endif
			for (int i = 0; i != numIterations; ++i)
			{
				optimizationIteration(); //sum_{(u,v)}p(u,v)xv
				updatePays(); //update pay and strategy
			}
		}
		//Returns the current expected value
		public double expectedValue()
		{
			double temp = 0;
			return std.accumulate(_vertexSet.GetEnumerator(),_vertexSet.end(),temp,new MarkovFunctors.VertexPayback<List<_VertexType*>.value_type>());
		}
		//Prints the current optimal strategy to fout
		public void printCurOptimalStrategy(std.ofstream fout, string firstSepr, string secondSepr)
		{
			Dictionary<_NameType,_VertexType>.Enumerator vertItr;
			Dictionary<_NameType,_VertexType>.Enumerator endVert = _vertexMap.end();
			for (vertItr = _vertexMap.GetEnumerator(); vertItr != endVert; ++vertItr)
			{
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				fout << vertItr.first << firstSepr << vertItr.second._strategy << secondSepr;
			}
		}
	}
	namespace MarkovFunctors
    {
        ////////////////////////////////////////
        //Functor for sorting arcs by neighbor 
        ////////////////////////////////////////
        //C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
        //ORIGINAL LINE: template<typename Arc<StrategyVertex>>
        public class SortArcByNeighbor<Arc<StrategyVertex>>: std.binary_function<Arc<StrategyVertex>*,Arc<StrategyVertex>*,bool>
		{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(first_argument_type &left, second_argument_type &right) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type left, second_argument_type right)
			{
				return (left._neighbor < right._neighbor);
			}
		}
		///////////////////////////////////////////////////////////////
		///Specialized Inner Product Function Objects
		//////////////////////////////////////////////////////////////
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,class _Functor>
		public class ProbInnerProduct<_Ty,_Functor>: unary_function<_Ty, void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				double temp = 0;
				itr._tempProbability = std.accumulate(itr._neighborhood.begin(),itr._neighborhood.end(),temp,default(_Functor));
			}
		}
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,class _Functor,typename _argTy>
		public class PassProbInnerProduct<_Ty,_Functor,_argTy>: std.unary_function<_Ty, void>
		{
			private _argTy _arg = new _argTy();
			public PassProbInnerProduct(_argTy arg)
			{
				_arg = arg;
			}
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				double temp = 0;
				itr._tempProbability = std.accumulate(itr._neighborhood.begin(),itr._neighborhood.end(),temp,_Functor(_arg));
			}
		}
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,class _Functor>
		public class PayInnerProduct<_Ty,_Functor>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				double temp = 0;
				itr._tempPay = std.accumulate(itr._neighborhood.begin(),itr._neighborhood.end(),temp,default(_Functor));
			}
		}

		////////////////////////////////////////////////
		//Functors for Inner Products
		//////////////////////////////////////////////
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename Arc<StrategyVertex>>
		public class ArcTimesProb<Arc<StrategyVertex>>: std.binary_function<double, Arc<StrategyVertex>*, double> //<first_argument_type,second_argument_type,result_type>
		{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(const first_argument_type &tempProb, const second_argument_type &arc) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type tempProb, second_argument_type arc)
			{
				return tempProb + (arc._probability * arc._neighbor._probability);
			}
		}
		//Arcs that pay model 
		public class ArcExp: std.binary_function<double, PayArc<VertexAp>*, double>
		{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(const first_argument_type &tempPay, const second_argument_type &arc) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type tempPay, second_argument_type arc)
			{
				return tempPay + (arc._probability * ((arc._neighbor._pay) + (arc._neighbor._probability) * (arc._pay))); //calc
			}
		}
		//Traditional dot product on pay
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename Arc<StrategyVertex>>
		public class ArcTimesPay<Arc<StrategyVertex>>: std.binary_function<double, Arc<StrategyVertex>*, double>
		{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(const first_argument_type &tempPay, const second_argument_type &arc) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type tempPay, second_argument_type arc)
			{
				return (tempPay + arc._probability * arc._neighbor._pay); //value of moving ahead
			}
		}

		//traditional dot product excluding any vertex with a given pay
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename Arc<StrategyVertex>, typename _PayType>
		public class ExcludeTimes<Arc<StrategyVertex>, _PayType>: std.binary_function<double, Arc<StrategyVertex>*, double>
		{
			private _PayType _excludePay = new _PayType();
			public ExcludeTimes(_PayType pay)
			{
				_excludePay = pay;
			}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(const first_argument_type &tempProb, const second_argument_type &arc) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type tempProb, second_argument_type arc)
			{
				return ((arc._neighbor._pay != _excludePay)?(tempProb + (arc._probability * arc._neighbor._probability)):(tempProb));
			}
		}
		////////////////////////////////////////
		//Update functions
		////////////////////////////////////////
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty>
		public class UpdateProbs <_Ty>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				itr._probability = itr._tempProbability;
			}
		}
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty>
		public class UpdatePays <_Ty>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				itr._pay = itr._tempPay;
			}
		}
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty>
		public class OptiPayUpdate <_Ty>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
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
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,typename Arc<StrategyVertex>>
		public class ClearNeighborhood<_Ty,Arc<StrategyVertex>>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				List<Arc<StrategyVertex>>.Enumerator arcItr;
				List<Arc<StrategyVertex>>.Enumerator end = itr._neighborhood.end();
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

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty>
		public class ResetVertex<_Ty>: std.unary_function<_Ty,void>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(argument_type itr)
			{
				itr.reset();
			}
		}

		///////////////////////////////////////////////////////////////////
		////Calcualates _pay*_probability of a vertex and adds it to temp
		//////////////////////////////////////////////////////////////////
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty>
		public class VertexPayback<_Ty>: std.binary_function<double,_Ty,double>
		{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: result_type operator ()(first_argument_type & temp, second_argument_type & itr) const
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type temp, second_argument_type itr)
			{
				return (temp + (double)(itr._pay) * itr._probability);
			}
		}
		//////////////////////////////////////////////////////////
		/////For calculating max pay and max pay odds 
		//////////////////////////////////////////////////////////
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,typename _PayType>
		public class FindVertexMaxPay<_Ty,_PayType>: std.binary_function <_PayType, _Ty, _PayType>
		{
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type curMax, second_argument_type itr)
			{
				return ((curMax < (itr.getPay()))?itr.getPay():curMax);
			}
		}
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,typename _PayType>
		public class CollectLikeProbs<_Ty,_PayType>: std.binary_function<double,_Ty,double>
		{
			private _PayType _payToFind = new _PayType();
			public CollectLikeProbs(_PayType pay)
			{
				_payToFind = pay;
			}
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type temp, second_argument_type itr)
			{
				return temp + ((itr._probability > 0 && itr.getPay() == _payToFind)?(itr._probability):(0));
			}
		}

		//perform P=P+sum_{v in V(D) pv*p^0_v)
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<class _Ty,typename _NameType,typename _PayType>
		public class OptiProbSum<_Ty,_NameType,_PayType>: std.binary_function<double,_Ty,double>
		{
			private Dictionary<_NameType,double> _startingProbs = new Dictionary<_NameType,double>();
			public OptiProbSum(Dictionary<_NameType,double> startingProbs)
			{
				_startingProbs = new Dictionary<_NameType,double>(startingProbs);
			}
//C++ TO C# CONVERTER TODO TASK: The () operator cannot be overloaded in C#:
			public static result_type operator ()(first_argument_type temp, second_argument_type itr)
			{
				return (temp + (itr.second._probability * _startingProbs[itr.first]));
			}
		}
	}
}