using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace AnimationParser
{
    class AnimationParser
    {
        private string _fullPath;
        private string[] _lines;
        private List<AEntity> _entities;
        public List<AEntity> Entities => _entities;
        public string[] Lines => _lines;
        public string Path => _fullPath;

        public AnimationParser(string path)
        {
            
            _fullPath = path;
            _entities = new List<AEntity>();
            _lines = ReadFile(_fullPath);
            
        }

        private string[] ReadFile(string textFilePath)
        {
            if (File.Exists(textFilePath))
            { 
                string[] lines = File.ReadAllLines(textFilePath);
                for (int i = 0; i<lines.Length; i++ )
                {
                    if (lines[i].Contains("a-entity") && lines[i].Contains("name"))
                    {
                        AEntity ent = new AEntity(lines[i], i);
                        if(ent != null)
                        {
                            _entities.Add(ent);
                        }
                        
                    }
                    
                }
                return lines;
            }
            throw (new Exception("File does not exist"));
        }

        public override string ToString()
        {
            string res = "";
            foreach (var item in _entities)
            {
                res += item.ToString() + "\n";
            }
            return res;
        }
    }
    
}
