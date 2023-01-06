using System.Diagnostics;
using System.Collections;
using MarkovLibraryCSharp.MarkovFunctors;
using System.Linq;
namespace MarkovLibraryCSharp
{
    public class BaseGraph<_NameType, _VertexType> : IDisposable where _VertexType : IVertex
	{
		//protect constructor
		protected BaseGraph()
		{
		}
		//vertex map type
		//vertex set type
		//map used for constructing and examining graph
		protected Dictionary<_NameType, _VertexType> _vertexMap = new();
		//vector used in algorithms
		protected List<_VertexType> _vertexSet = new();
		//assign tempProbabilities to probabilities in any model
		protected void updateProbs()
		{
			foreach (_VertexType i in _vertexSet)
			{
				//UpdateProbs<List<List<_VertexType>>::value_type>();
			}
		}
		//Destructor
		public void Dispose()
		{
			//Delete the arcs
			clearArcSet();
			//clear the sets (not really necessary but eh, why not)
			_vertexMap.Clear();
			_vertexSet.Clear();
		}
		//Add a vertex to the graph
		public void addVertex(_NameType name, double probability = 0, double pay = 0)
		{
			Debug.Assert(probability >= 0 && probability <= 1);
            _VertexType vert = (_VertexType)Activator.CreateInstance(typeof(_VertexType), (probability, pay));
			_vertexMap.TryAdd(name, vert);
			_vertexSet.Add(vert);

		}
		//Set the probability of a vertex
		public void setVertexProb(_NameType name, double probability)
		{
			Debug.Assert(probability >= 0 && probability <= 1);
            _vertexMap[name]._probability= probability;

		}
			//Set the pay of a vertex
		public void setVertexPay(_NameType name, double pay)
		{
			_vertexMap[name].setPay(pay);
		}
		//Get the current probability of a vertex
		public double getVertexProb(_NameType name)
		{
			return _vertexMap[name]._probability;
		}
		//Get the current pay of a vertex
		public double getVertexPay(_NameType name)
		{
			return _vertexMap[name]._pay;
		}
		//Remove all Arcs from the graph 
		public void clearArcSet()
		{

			foreach (var vertex in _vertexSet)
				ClearNeighborhood<List<_VertexType>, _VertexType.Arc<Vertex>>());
		}
		//Reset all vertex values
		public void resetVertexValues()
		{
			_vertexSet.ForEach(ResetVertex<List<_VertexType>>());
		}
		//print current graph probabilities 
		public void printProbabilities(std::ofstream fout, string firstSepr, string secondSepr)
		{
			SortedDictionary<_NameType, _VertexType>.Enumerator vertItr;
			SortedDictionary<_NameType, _VertexType>.Enumerator endVert = _vertexMap.end();
			for (vertItr = _vertexMap.GetEnumerator(); vertItr != endVert; ++vertItr)
			{
				//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				fout << vertItr.first << firstSepr << vertItr.second._probability << secondSepr;
			}
		}
		//print current graph pays
		public void printPays(std::ofstream fout, string firstSepr, string secondSepr)
		{
			SortedDictionary<_NameType, _VertexType>.Enumerator vertItr;
			SortedDictionary<_NameType, _VertexType>.Enumerator endVert = _vertexMap.end();
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
			List<_VertexType.Arc<Vertex>>.Enumerator curArc1;
			List<_VertexType.Arc<Vertex>>.Enumerator curArc2;
			List<_VertexType.Arc<Vertex>>.Enumerator endArc;
			List<_VertexType.Arc<Vertex>> tempVec = new List<_VertexType.Arc<Vertex>>(); //create tempNeighborhood
			double tempPay;
			double tempProb;
			_VertexType tempNeighbor;
			MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type, _VertexType.Arc<Vertex>> CNF = new MarkovFunctors.ClearNeighborhood<List<_VertexType>.value_type, _VertexType.Arc<Vertex>>();

			//algorithm
			for (vertexItr = _vertexSet.GetEnumerator(); vertexItr != endVertex; ++vertexItr)
			{
				//sort neighborhood by neighbor to speed up algorithm
				//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				sort((vertexItr)._neighborhood.begin(), (vertexItr)._neighborhood.end(), MarkovFunctors.SortArcByNeighbor<_VertexType.Arc<Vertex>>());
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
					tempVec.Add(new _VertexType.Arc<Vertex>(tempNeighbor, tempProb));
				}
				//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				CNF(vertexItr); //delete all old arcs
								//replace neighborhood with simple arcs
								//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
				(vertexItr)._neighborhood.insert((vertexItr)._neighborhood.begin(), tempVec.GetEnumerator(), tempVec.end());
			}
		}
	}
}
