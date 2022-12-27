using System;
using System.Diagnostics;
using QuantumCore.Math;
using QuantumCore.Quantum;

namespace QuantumCore
{
    internal class Program
    {
        static void ComplexTest()
        {
            Complex a = new Complex(1, 2);
            Complex b = new Complex(-1, 0);
            Complex c = new Complex(0, 3);
            Complex d = new Complex(2.5, -2.5);

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
        }

        static void VectorTest()
        {
            Vector e = new Vector();
            Console.WriteLine(e);

            Vector x = new Vector(2);
            Console.WriteLine(x);

            Vector y = new Vector(3, new Complex(2, 1));
            Console.WriteLine(y);

            Complex a = new Complex(1, 2);
            Complex b = new Complex(-1, 0);
            Complex c = new Complex(0, 3);
            Complex d = new Complex(2.5, -2.5);

            Complex[] arr = { a, b, c, d };
            Vector u = new Vector(arr);
            Console.WriteLine(u);

            Vector v = new Vector { a, b, c, d };
            Console.WriteLine(v);

            x.Add(new Complex(-1, 5));
            Console.WriteLine(x);

            x.Delete();
            Console.WriteLine(x);

            Vector s = u.Plus(v);
            Console.WriteLine(s);

            Vector s2 = u.Minus(v);
            Console.WriteLine(s2);

            Vector n = v.Plus(y);
            Console.WriteLine(n);

            Vector t = v.TensorProduct(y);
            Console.WriteLine(t);
        }

        static void MatrixTest()
        {
            Matrix m = new Matrix(2, 3);
            Console.WriteLine(m);
            Console.WriteLine(m.Height);
            Console.WriteLine(m.Width);

            Matrix m2 = new Matrix(4, 4, true);
            Console.WriteLine(m2);
            Console.WriteLine(m2.Height);
            Console.WriteLine(m2.Width);
            Console.WriteLine(m2[1]);
            Console.WriteLine(m2[1][2]);

            Matrix m3 = new Matrix(4, 2, new Complex(0, 1));
            Console.WriteLine(m3);
            Console.WriteLine(m3.Height);
            Console.WriteLine(m3.Width);

            Complex[][] arr = new Complex[3][];
            for (int i = 0; i < 3; i++)
            {
                arr[i] = new Complex[2];
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    arr[i][j] = new Complex(i, j);
                }
            }
            Matrix m4 = new Matrix(arr);
            Console.WriteLine(m4);
            Console.WriteLine(m4.Height);
            Console.WriteLine(m4.Width);

            Complex a = new Complex(1, 2);
            Complex b = new Complex(-1, 0);
            Complex c = new Complex(0, 3);
            Complex d = new Complex(2.5, -2.5);
            Matrix m5 = new Matrix { new Vector { a, b, c, d},
                                     new Vector { b, c, d, a},
                                     new Vector { c, d, a, b} };
            Console.WriteLine(m5);
            Console.WriteLine(m5.Height);
            Console.WriteLine(m5.Width);
            m5.Delete();
            Console.WriteLine(m5);
            Console.WriteLine(m5.Height);
            Console.WriteLine(m5.Width);
            m5.AddColumn(new Vector { a, d });
            Console.WriteLine(m5);
            Console.WriteLine(m5.Height);
            Console.WriteLine(m5.Width);
            m5.DeleteColumn();
            Console.WriteLine(m5);
            Console.WriteLine(m5.Height);
            Console.WriteLine(m5.Width);

            Matrix m6 = m2.Plus(m2);
            Console.WriteLine(m6);
            Console.WriteLine(m6.Height);
            Console.WriteLine(m6.Width);

            Matrix m7 = m2.Minus(m2.ScalarProduct(new Complex(0, 2)));
            Console.WriteLine(m7);
            Console.WriteLine(m7.Height);
            Console.WriteLine(m7.Width);

            Matrix A = new Matrix { new Vector { new Complex(2), new Complex(-3), new Complex(1) },
                                    new Vector { new Complex(5), new Complex(4), new Complex(-2) } };
            Matrix B = new Matrix { new Vector { new Complex(-7), new Complex(5) },
                                    new Vector { new Complex(2), new Complex(-1) },
                                    new Vector { new Complex(4), new Complex(3) } };
            Matrix C = A.Product(B);
            Console.WriteLine(C);
            Console.WriteLine(C.Height);
            Console.WriteLine(C.Width);
            Matrix D = A.Product(B);
            Console.WriteLine(D);
            Console.WriteLine(D.Height);
            Console.WriteLine(D.Width);

            Matrix X = new Matrix { new Vector { new Complex(1), new Complex(2) },
                                    new Vector { new Complex(3), new Complex(4) },
                                    new Vector { new Complex(5), new Complex(6) } };
            Matrix Y = new Matrix { new Vector { new Complex(7), new Complex(8) },
                                    new Vector { new Complex(9), new Complex(0) } };
            Matrix T = X.TensorProduct(Y);
            Console.WriteLine(T);
            Console.WriteLine(T.Height);
            Console.WriteLine(T.Width);

            Matrix M = new Matrix { new Vector { new Complex(1), new Complex(2), new Complex(-1), new Complex(2) },
                                    new Vector { new Complex(3), new Complex(0), new Complex (4), new Complex(-2) },
                                    new Vector { new Complex(2), new Complex(3), new Complex(3), new Complex(5) } };
            Vector V = new Vector { new Complex(-1), new Complex(2), new Complex(1), new Complex(3) };
            Vector R = V.MatrixProduct(M);
            Console.WriteLine(R);
        }

        static void SpeedTest()
        {
            int size = 1;
            for (int i = 0; i < 20; i++)
            {
                Matrix A = new Matrix(size, size, true);
                Matrix B = new Matrix(size, size, true);

                Stopwatch sw = Stopwatch.StartNew();
                A.Product(B);
                sw.Stop();
                Console.Write(size);
                Console.Write(": ");
                Console.Write(sw.ElapsedMilliseconds);
                Console.WriteLine();

                size *= 2;
            }
        }

        static void OperatorTest()
        {
            Console.WriteLine(Operator.I);
            Console.WriteLine(Operator.H);
            Console.WriteLine(Operator.X);
            Console.WriteLine(Operator.Y);
            Console.WriteLine(Operator.Z);
            Console.WriteLine(Operator.T);
            Console.WriteLine(Operator.S);
        }

        static void TemplateTest()
        {
            Template t = new Template(3);
            t.Add("H", new int[1] { 0 });
            t.Add("X", new int[1] { 1 });
            t.Add("CNOT", new int[2] { 1, 2 });
            t.Add("M", new int[1] { 1 });
            t.Add("SWAP", new int[2] { 0, 2 });
            Console.WriteLine(t);
        }

        static void CircuitTest()
        {
            Template t = new Template(2);
            t.Add("H", new int[1] { 0 });
            t.Add("X", new int[1] { 1 });

            Circuit circ = new Circuit(t);
            Console.WriteLine(circ.Operators[0]);
        }

        static void CNOTTest()
        {
            Template t = new Template(3);
            t.Add("CNOT", new int[2] { 2, 1 });

            Circuit circ = new Circuit(t);
            Console.WriteLine(circ.Operators[0]);
        }

        static void SWAPTest()
        {
            Template t = new Template(3);
            t.Add("SWAP", new int[2] { 2, 1 });

            Circuit circ = new Circuit(t);
            Console.WriteLine(circ.Operators[0]);
        }

        static void Bell()
        {
            Template t = new Template(2);
            t.Add("H", new int[1] { 0 });
            t.Add("CNOT", new int[2] { 0, 1 });
            t.Add("M", new int[1] { 0 });
            t.Add("M", new int[1] { 1 });

            Circuit circuit = new Circuit(t);
            for (int i = 0; i < circuit.Operators.Count; i++) {
                Console.WriteLine(circuit.Operators[i]);
            }

            Console.WriteLine(t);

            //Matrix M1 = circuit.Operators[0];
            //Matrix M2 = circuit.Operators[1];
            //Console.WriteLine(M1);
            //Console.WriteLine(M2);
            //Console.WriteLine(M2.Product(M1));
        }

        static void Hren()
        {
            Template t = new Template(3);
            t.Add("H", new int[1] { 0 });
            t.Add("CNOT", new int[2] { 0, 1 });
            t.Add("H", new int[1] { 1 });
            t.Add("CNOT", new int[2] { 1, 0 });
            t.Add("S", new int[1] { 1 });
            t.Add("T", new int[1] { 0 });
            t.Add("SWAP", new int[2] { 0, 2 });
            t.Add("Z", new int[1] { 2 });
            t.Add("M", new int[1] { 2 });

            Circuit circuit = new Circuit(t);
            for (int i = 0; i < circuit.Operators.Count; i++)
            {
                Console.WriteLine(circuit.Operators[i]);
            }
            Console.WriteLine(t);
        }

        static void Qubit()
        {
            Qubit q = new Qubit();
            QubitReg qreg = new QubitReg(3);

            Console.WriteLine(q);
            Console.WriteLine(qreg);
        }

        static void Main()
        {
            Qubit();
        }
    }
}
