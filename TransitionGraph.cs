using System.Diagnostics;

namespace MarkovLibraryCSharp
{
    public class TransitionGraph<_NameType, _VertexType> : BaseGraph<_NameType, _VertexType> where _VertexType : IVertex where _ArcType : IArc 
	{
		//one iteration of a Markov Chain
		private void markovProbabilityIteration()
		{
			
			MarkovFunctors.MarkovFunctors.ArcTimesProb<IArc<_VertexType>> arcTimesProbDel = MarkovFunctors.MarkovFunctors.ArcTimesProbMethod<IArc<_VertexType>>;
            MarkovFunctors.MarkovFunctors.ProbInnerProduct < _VertexType, delegate> probInnerProductDel = MarkovFunctors.MarkovFunctors.ProbInnerProductMethod < _VertexType, arcTimesProbDel>;
            var tempProb = 0.0;
			probInnerProductDel(_vertexSet, arcTimesProbDel<IArc<_VertexType>>(tempProb,_vertexSet));
			_vertexSet.ForEach(probInnerProduct);
			foreach(var vertex in _vertexSet)
            {
				//TODO: fix this
				ProbInnerProduct<<List<_VertexType>,ArcTimesPay<IVertex.Arc<IVertex>>>(vertex);
            }
			//TODO: fix this
			std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.ProbInnerProduct<List<_VertexType>, MarkovFunctors.ArcTimesProb<Vertex.Arc<Vertex>>>());
		}
		//add an arc
		public void addArc(_NameType tail, _NameType head, double probability)
		{
#if ARC_CHECK_ON
			Debug.Assert(_vertexMap.find(tail) != _vertexMap.end() && _vertexMap.find(head) != _vertexMap.end());
#endif
			_vertexMap[head].addArcToNeighborhood (_vertexMap[tail], probability,0);
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
			Debug.Assert(numIterations >= 0);
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
			//TODO: delete this
			return temp;
			//TODO: fix this
			//return std::accumulate(_vertexSet.begin(), _vertexSet.end(), temp, MarkovFunctors.VertexPayback<VertexSet_Type.value_type>());
		}
		//Finds the probability of hitting a vertex with given pay at least once in numIterations
		public double givenPayProbability(int numIterations, double pay)
		{
			Debug.Assert(numIterations >= 1);

			double toReturn = 0; //Set P=0
			markovProbability(); //perform one iteration of Alg 5
			toReturn += curPayProbability(pay); //P=P+sum_{v:xv=pay} pv
			for (int i = 1; i != numIterations; ++i)
			{
				//TODO: check this
				//v._tempProbability = sum_{(u,v):xu neq pay}p(u,v)pu
				//std::for_each(_vertexSet.begin(), _vertexSet.end(), MarkovFunctors.PassProbInnerProduct<VertexSet_Type.value_type, MarkovFunctors.ExcludeTimes<Vertex.Arc<Vertex>, double>, double>(pay));
				updateProbs(); //pv=v._tempProbability
				toReturn += curPayProbability(pay); //P=P+sum_{v:xv=pay}pv
			}
			return toReturn; //return P
		}
		//Returns highest vertex pay, and probability of hitting it at least once in numIterations
		public double maxPayProbability(int numIterations, ref double maxPay)
		{
			Debug.Assert(numIterations >= 1);

			double temp = 0;
			//set maxPay = max{xv:v in V(D)}
			//TODO: fix this
			//maxPay = std::accumulate(_vertexSet.begin(), _vertexSet.end(), temp, MarkovFunctors.FindVertexMaxPay<VertexSet_Type.value_type, double>());
			//perform alg 7
			return givenPayProbability(numIterations, maxPay);
		}
		//returns the current probability of any vertex with given pay
		public double curPayProbability(double pay)
		{
			double temp = 0; //return sum_{v in V(D):xv=pay}pv
							 //TODO: remove  this
			return temp;
							 //TODO: fix this
							 //return std::accumulate(_vertexSet.begin(), _vertexSet.end(), temp, MarkovFunctors.CollectLikeProbs<VertexSet_Type.value_type, double>(pay));
		}
		//returns the current probability of any vertex with max pay
		public double curMaxPayProbability()
		{
			double temp = 0; //Set MP = max{xv:v in V(D)}

			//TODO: fix this
			//double maxPay = std::accumulate(_vertexSet.begin(), _vertexSet.end(), temp, MarkovFunctors.FindVertexMaxPay<VertexSet_Type.value_type, double>());
			//return sum_{v:xv=pay}pv
			//TODO: fix this
			//return curPayProbability(maxPay);
			//TODO: remove  this
			return curPayProbability(temp);
		}
	}
}
