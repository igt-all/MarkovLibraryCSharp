using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovLibraryCSharp
{
    internal interface IArc<_VertexType> where _VertexType : IVertex
    {
        public double _probability { get; set; }
        public _VertexType _neighbor { get; set; }
    }
}
