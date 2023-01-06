using System;
using System.Collections.Generic;

namespace MarkovLibraryCSharp
{
    public class Vertex : IVertex
    {
        public double _probability { get; set; }
        public double _tempProbability { get; set; }
        public double _pay { get; set; }
        public List<Arc<Vertex>> _neighborhood = new List<Arc<Vertex>>();

        public Vertex(double probability, double pay)
        {
            _probability = probability;
            _pay = pay;
            _tempProbability = 0;
        }
        //Add an arc to the neighborhood vector
        public void addArcToNeighborhood<_VertexType>(_VertexType neighbor, double probability, double pay =0)
        {
            _neighborhood.Add(new Arc<_VertexType>(neighbor, probability));
        }
        //Set the pay value
        public  void setPay(double pay)
        {
            _pay = pay;
        }
        //Get the pay value
        public  double getPay()
        {
            return _pay;
        }
        //reset all values to 0
        public  void reset()
        {
            _probability = 0;
            _tempProbability = 0;
            _pay = 0.0;
        }
    }
}