using System;
using System.Collections.Generic;

namespace MarkovLibraryCSharp
{
    public class StrategyVertex : IVertex
    {
        public bool _strategy;
        public double _probability { get; set; }
        public double _pay { get; set; }
        public double _originalPay;
        public double _tempProbability;
        public double _tempPay;
        public List<Arc<StrategyVertex>> _neighborhood = new List<Arc<StrategyVertex>>();

        public StrategyVertex(double probability, double pay)
        {
            _probability = probability;
            _tempProbability = 0;
            _tempPay = 0;
            _strategy = true;
            _originalPay = pay;
            _pay = (double)Convert.ChangeType(pay, typeof(double));
        }
        //Add an arc to the neighborhood vector
        public void addArcToNeighborhood<_VertexType>(_VertexType neighbor, double probability, double pay =0)
        {
            _neighborhood.Add(new Arc<_VertexType>( neighbor, probability));
        }
        //set the pay and original pay values simultaneously
        public void setPay(double pay)
        {
            _originalPay = pay;
            _pay = (double)Convert.ChangeType(pay, typeof(double));
        }
        //get the original pay value
        public double getPay()
        {
            return _originalPay;
        }
        //reset all values to 0 and strategy to true
        public void reset()
        {
            _probability = 0;
            _tempProbability = 0;
            _pay = 0;
            _originalPay = 0.0;
            _tempPay = 0;
            _strategy = true;
        }
    }
}