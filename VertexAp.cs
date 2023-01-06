using System.Collections.Generic;

namespace MarkovLibraryCSharp
{
    //Vertex class for transition graph plus
    public class VertexAp: IVertex
    {
        public double _probability { get; set; }
        public double _pay { get; set; }
        public double _tempProbability { get; set; }
        public double _tempPay { get; set; }
        public List<PayArc<VertexAp>> _neighborhood = new List<PayArc<VertexAp>>();

        public VertexAp(double probability, double pay)
        {
            _probability = probability;
            _pay = pay;
            _tempProbability = 0;
            _tempPay = 0;
        }
        //Add an arc to the neighborhood vector
        public  void addArcToNeighborhood<_VertexType>(_VertexType neighbor, double probability, double pay)
        {
            _neighborhood.Add(new PayArc<_VertexType>(neighbor, probability, pay));
        }
        //Set the pay value
        public void setPay(double pay)
        {
            _pay = pay;
        }
        //Get the pay value
        public double getPay()
        {
            return _pay;
        }
        //reset all values to 0
        public void reset()
        {
            _probability = 0;
            _tempProbability = 0;
            _pay = 0;
            _tempPay = 0;
        }
    }
}