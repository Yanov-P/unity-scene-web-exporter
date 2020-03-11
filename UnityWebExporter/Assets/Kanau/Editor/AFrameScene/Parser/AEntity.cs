using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace AnimationParser
{
    class AEntity
    {
        private string _name;
        private Dictionary<string, string> _properties;
        private Vector3 _position;
        private Vector3 _scale;
        private Vector3 _rotation;
        private Vector2 _indexOfAnimation;
        public Vector2 IndexOfAnimation => _indexOfAnimation;
        public Vector3 Position => _position;
        public Vector3 Rotation => _rotation;
        public Vector3 Scale => _scale;
        public string Name => _name;
        public Dictionary<string, string> Properties => _properties;

        public AEntity(string str, int lineNum)
        {
            _indexOfAnimation.X = lineNum;
            _properties = new Dictionary<string, string>();
            int index = -1;
            int lastIndex = str.LastIndexOf('=');
            for (;;)
            {
                index = str.IndexOf('=', index + 1);
                if (index == -1) break;
                var substring = str.Substring(0, index);
                string[] words = substring.Split(new char[] { ' ' });
                string propName = words[words.Length - 1];

                var kavichkaNum = str.IndexOf("\"", index + 2);
                if(propName == "name")
                {
                    _indexOfAnimation.Y = kavichkaNum + 1;
                }
                string propValue = str.Substring(index + 2, kavichkaNum - index - 2);

                StringsToField(propName, propValue);

                if (index == lastIndex) break;
            }
        } 

        private void StringsToField(string name, string value)
        {
            _properties.Add(name, value);
            //if(name == "name")
            //{
            //    _name = value;
            //}
            //else if(name == "position")
            //{
            //    _position = StringToVector(value);
            //}
            //else if (name == "rotation")
            //{
            //    _rotation = StringToVector(value);
            //}
            //else if (name == "scale")
            //{
            //    _scale = StringToVector(value);
            //}
        }

        private Vector3 StringToVector(string s)
        {
            var words = s.Split(new char[] { ' ' });
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            Vector3 res = new Vector3(float.Parse(words[0],NumberStyles.Any,ci), float.Parse(words[1], NumberStyles.Any, ci), float.Parse(words[2], NumberStyles.Any, ci));
            return res;
        }

        public override string ToString()
        {
            string res = "";
            foreach (var item in _properties)
            {
                res += item.Key + " " + item.Value + " ";
            }
            return res; 

        }

    }
}
