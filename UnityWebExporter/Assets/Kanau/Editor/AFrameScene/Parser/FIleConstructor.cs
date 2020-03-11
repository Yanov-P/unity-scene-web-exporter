using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;

namespace AnimationParser
{
    class FIleConstructor
    {
        private AnimationParser[] _parsers;
        private string[] _lines;
        private string _newFile;
        private int _timeStep;
        public FIleConstructor(string directory)
        {
            
            _newFile = directory + "index.html";
            File.Delete(_newFile);
            var files = Directory.GetFiles(directory);

            string fname = Path.GetFileNameWithoutExtension(files[1]);
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            _timeStep = (int)( float.Parse(fname, NumberStyles.Any, ci) * 1000);

            _lines = File.ReadAllLines(files[0]);

            _parsers = new AnimationParser[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                _parsers[i] = new AnimationParser(files[i]);
                //Console.WriteLine(_parsers[i].Entities[2]);
            }
            File.Create(_newFile).Close();

            for (int j = _parsers[0].Entities.Count- 1 ; j >= 0; j--)
            {
                AEntity[] ents = new AEntity[_parsers.Length];
                for (int i = 0; i < _parsers.Length; i++)
                {
                    //Console.WriteLine(_parsers[i].Entities[j]);
                    ents[i] = _parsers[i].Entities[j];
                }
                WriteAnimation(ents);
            }
            File.WriteAllLines(_newFile, _lines);
        }

        private void WriteAnimation(AEntity[] ents)
        {
            var pos = ents[0].IndexOfAnimation;
            string stringToInsert = "";

            int animationNum = 0;
            foreach (var item in ents[0].Properties)
            {
                bool changed = false;
                for (int i = 1; i < ents.Length; i++)
                {
                    if (ents[i].Properties[item.Key] != ents[i-1].Properties[item.Key])
                    {
                        changed = true;
                    }

                }
                if (changed)
                {
                    for (int i = 1; i < ents.Length; i++)
                    {
                        stringToInsert += $"\nanimation__{animationNum}=\"delay:{_timeStep * i}; autoplay: true; dur: {_timeStep}; property: {item.Key}; from: {ents[i-1].Properties[item.Key]}; to: {ents[i].Properties[item.Key]}; easing: linear;\"";
                        animationNum++;
                    }
                    
                }
            }

            _lines[(int)pos.X] = _lines[(int)pos.X].Insert((int)pos.Y, stringToInsert);
            
        }

        

        private string VectorToString(Vector3 v)
        {
            return v.X.ToString("en-US") + " " + v.Y.ToString("en-US") + " " + v.Z.ToString("en-US");
        }


    }
}
