﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuantumCore.Quantum
{
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
                        _Template[i].Add(Operator + "0");
                    }
                    else if (i == Param[1])
                    {
                        _Template[i].Add(Operator + "1");
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