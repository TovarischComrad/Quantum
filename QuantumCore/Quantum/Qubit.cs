using System;
using System.Collections.Generic;
using System.Text;
using QuantumCore.Math;

namespace QuantumCore.Quantum
{
    internal class Qubit
    {
        protected Vector Amplitude { get; set; }
        public Qubit()
        {
            Amplitude = new Vector { new Complex(1), new Complex(0) };
        }
    }

    internal class Operator
    {
        public static readonly Matrix Zero
            = new Matrix { new Vector { new Complex(1), new Complex(0) },
                           new Vector { new Complex(0), new Complex(0) } };

        public static readonly Matrix One
            = new Matrix { new Vector { new Complex(0), new Complex(0) },
                           new Vector { new Complex(0), new Complex(1) } };

        public static readonly Matrix I 
            = new Matrix { new Vector { new Complex(1), new Complex(0) },
                           new Vector { new Complex(0), new Complex(1) } };

        public static readonly Matrix X 
            = new Matrix { new Vector { new Complex(0), new Complex(1) },
                           new Vector { new Complex(1), new Complex(0) } };

        public static readonly Matrix Y 
            = new Matrix { new Vector { new Complex(0), new Complex(0, -1) },
                           new Vector { new Complex(0, 1), new Complex(0) } };

        public static readonly Matrix Z 
            = new Matrix { new Vector { new Complex(1), new Complex(0) },
                           new Vector { new Complex(0), new Complex(-1) } };

        public static readonly Matrix H 
            = new Matrix { new Vector { new Complex(1), new Complex(1) },
                           new Vector { new Complex(1), new Complex(-1) } }
                         .ScalarProduct(new Complex(1.0 / System.Math.Sqrt(2)));

        public static readonly Matrix S 
            = new Matrix { new Vector { new Complex(1), new Complex(0) },
                           new Vector { new Complex(0), new Complex(0, 1) } };

        public static readonly Matrix T 
            = new Matrix { new Vector { new Complex(1), new Complex(0) },
                           new Vector { new Complex(0), new Complex(1, System.Math.PI / 4, true) } };

        public static readonly Dictionary<string, Matrix> OperatorsDict = new Dictionary<string, Matrix>()
        {
            { "Zero", Zero },
            { "One", One },
            { "I", I },
            { "H", H },
            { "X", X },
            { "Y", Y },
            { "Z", Z },
            { "S", S },
            { "T", T }
        };
    }
}
