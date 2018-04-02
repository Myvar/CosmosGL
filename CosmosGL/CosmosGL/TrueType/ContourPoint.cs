using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.TrueType
{
    public class ContourPoint
    {
        public List<string> ValueList { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();

        public bool OnCurve { get; set; }

        public ContourPoint(bool onCurve)
        {
            OnCurve = onCurve;
        }

        public int this[string s]
        {
            get { return Values[GetOffset(s)]; }
            set
            {
                if (GetOffset(s) == -1)
                {
                    ValueList.Add(s);
                    Values.Add(value);
                }
                else
                {
                    Values[GetOffset(s)] = value;
                }
            }
        }

        public int GetOffset(string s)
        {
            for (var index = 0; index < ValueList.Count; index++)
            {
                var t = ValueList[index];
                if (t == s)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}