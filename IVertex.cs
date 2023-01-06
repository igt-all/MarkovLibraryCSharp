using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovLibraryCSharp
{
    public interface IVertex
    {
        public double _pay { get; set; }
        public double _probability { get; set; }
        public void addArcToNeighborhood<_VertexType>(_VertexType neighbor, double probability, double pay);
        public void setPay(double pay);
        public double getPay();
        public void reset();
    }
}
