using QuantumCore.Math;
using System.Collections.Generic;


namespace QuantumCore.Quantum
{
    internal class Circuit
    {
        public List<Matrix> Operators { get; }
        private Template Template;

        // Преобразование квантовой схемы в набор матричных операторов
        public Circuit(Template template)
        {
            Operators = new List<Matrix>();
            Template = template;

            List<int> types = Template.Type;

            int size = (int)System.Math.Pow(2, Template.Size);
            int j = 0; // Указатель на операторы

            // Перебор операторов
            while (j < types.Count)
            {

                // УНАРНЫЕ ОПЕРАТОРЫ
                if (types[j] == 1)
                {
                    // Накопитель для матричного произведения
                    Matrix M = new Matrix(size);

                    // Обработка унарных операторов
                    while (j < types.Count && types[j] == 1)
                    {
                        // Накопитель для тензорного произведения матриц
                        Matrix Op = new Matrix(1, 1, new Complex(1));

                        // Перебор кубитов
                        for (int i = Template.Size - 1; i >= 0; i--)
                        {
                            Op = Op.TensorProduct(Operator.OperatorsDict[Template._Template[i][j]]);
                        }
                        M = Op.Product(M);
                        j++;
                    }
                    Operators.Add(M);
                }
                

                // БИНАРНЫЕ ОПЕРАТОРЫ
                if (types[j] == 2)
                {
                    int i0 = 0, i1 = 0;
                    for (int i = 0; i < Template.Size; i++)
                    {
                        if (Template._Template[i][j] == "CNOT0") { i0 = i; }
                        if (Template._Template[i][j] == "CNOT1") { i1 = i; }
                    }
                    int n = i1 - i0;

                    // Формула с доски
                    Matrix first = new Matrix();
                    Matrix second = new Matrix();
                    if (n > 0)
                    {
                        first = Operator.OperatorsDict["Zero"];
                        second = Operator.OperatorsDict["One"];
                        for (int i = 0; i < n - 1; i++)
                        {
                            first = first.TensorProduct(Operator.OperatorsDict["I"]);
                            second = second.TensorProduct(Operator.OperatorsDict["I"]);
                        }
                        first = first.TensorProduct(Operator.OperatorsDict["I"]);
                        second = second.TensorProduct(Operator.OperatorsDict["X"]);
                    }
                    else
                    {
                        first = Operator.OperatorsDict["I"];
                        second = Operator.OperatorsDict["X"];
                        for (int i = 0; i < -1 - n; i++)
                        {
                            first = first.TensorProduct(Operator.OperatorsDict["I"]);
                            second = second.TensorProduct(Operator.OperatorsDict["I"]);
                        }
                        first = first.TensorProduct(Operator.OperatorsDict["Zero"]);
                        second = second.TensorProduct(Operator.OperatorsDict["One"]);
                    }
                    Matrix S = first.Plus(second);
                    Operators.Add(S);
                }

                j++;
            }
        } 
    }

    // Template - вспомогательный класс для подготовки операторов для квантовой схемы
    // И для создания диаграммы квантовой схемы для вывода на консоль
    internal class Template
    {
        // Кол-во обрабатываемых кубитов
        public int Size { get; }

        // Массив операторов
        public List<string>[] _Template { get; }

        // Кол-во букв для каждой колонки
        public List<int> LetterSize { get; protected set; }

        // Список индикаторов, показывающие тип операторов
        // 0 - оператор измерения
        // 1 - унарный оператор
        // 2 - бинарный оператор
        // 3 - тернарный оператор
        public List<int> Type { get; protected set; }

        public Template(int size)
        {
            Size = size;
            _Template = new List<string>[Size];
            LetterSize = new List<int>();
            Type = new List<int>();
            for (int i = 0; i < Size; i++)
            {
                _Template[i] = new List<string>();
            }
        }
        public void Add(string Operator, int[] Param)
        {
            if (Operator == "I" || Operator == "H" || Operator == "X" || Operator == "Y"
                 || Operator == "Z" || Operator == "S" || Operator == "T" || Operator == "M")
            {
                LetterSize.Add(1);
                for (int i = 0; i < Size; i++)
                {
                    if (i == Param[0])
                    {
                        _Template[i].Add(Operator);
                    }
                    else
                    {
                        _Template[i].Add("I");
                    }
                }

                if (Operator == "M") { Type.Add(0); }
                else { Type.Add(1); }
            }
            if (Operator == "CNOT")
            {
                LetterSize.Add(5);
                Type.Add(2);
                for (int i = 0; i < Size; i++)
                {
                    if (i == Param[0])
                    {
                        _Template[i].Add(Operator + "1");
                    }
                    else if (i == Param[1])
                    {
                        _Template[i].Add(Operator + "0");
                    }
                    else
                    {
                        _Template[i].Add("I");
                    }
                }
            }
            if (Operator == "SWAP")
            {
                LetterSize.Add(4);
                Type.Add(2);
                for (int i = 0; i < Size; i++)
                {
                    if (i == Param[0] || i == Param[1])
                    {
                        _Template[i].Add(Operator);
                    }
                    else
                    {
                        _Template[i].Add("I");
                    }
                }
            }
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Size; i++)
            {
                s += i.ToString() + ": ";
                for (int j = 0; j < _Template[i].Count; j++)
                {
                    if (_Template[i][j] == "I")
                    {
                        s += new string('-', LetterSize[j] + 2);
                    }
                    else
                    {
                        s += "-" + _Template[i][j] + "-";
                    }
                }
                s += "\n";
            }
            return s;
        }
    }
}
