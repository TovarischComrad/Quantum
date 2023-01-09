using System;
using System.Collections.Generic;
using System.Text;
using QuantumCore.Math;

namespace QuantumCore.Quantum
{
    internal class Qubit
    {
        public Vector Amplitude { get; }
        public Qubit()
        {
            Amplitude = new Vector { new Complex(1), new Complex(0) };
        }

        public override string ToString()
        {
            return Amplitude.ToString();
        }
    }

    internal class QubitReg
    {
        public Vector Amplitude { get; set; }
        public int Size { get; }

        public QubitReg()
        {
            Qubit q = new Qubit();
            Amplitude = q.Amplitude;
            Size = 1;
        }

        public QubitReg(int size)
        {
            Vector res = new Vector(1, new Complex(1));  
            for (int i = 0; i < size; i++)
            {
                Qubit q = new Qubit();
                res = res.TensorProduct(q.Amplitude);
            }
            Amplitude = res;
            Size = size;
        }

        public override string ToString()
        {
            return Amplitude.ToString();
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

        public static Matrix CNOT(int n)
        {
            Matrix first = new Matrix();
            Matrix second = new Matrix();
            if (n > 0)
            {
                first = Zero;
                second = One;
                for (int i = 0; i < n - 1; i++)
                {
                    first = first.TensorProduct(I);
                    second = second.TensorProduct(I);
                }
                first = first.TensorProduct(I);
                second = second.TensorProduct(X);
            }
            else
            {
                first = I;
                second = X;
                for (int i = 0; i < -1 - n; i++)
                {
                    first = first.TensorProduct(I);
                    second = second.TensorProduct(I);
                }
                first = first.TensorProduct(Zero);
                second = second.TensorProduct(One);
            }
            Matrix S = first.Plus(second);
            return S;
        }

        public static Matrix SWAP(int n)
        {
            Matrix M1 = CNOT(n);
            Matrix M2 = CNOT(-n);
            Matrix Res = M1.Product(M2);
            Res = Res.Product(M1);
            return Res;
        }
    }
}
