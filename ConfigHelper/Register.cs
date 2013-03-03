using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace Initialize
{
    public class Register
    {
        RegistryKey key;
        string path;
        string name;

        public RegistryKey Key { get { return key; } set { this.key = value; } }
        public string Path { get { return path; } set { this.path = value; } }
        public string Name { get { return name; } set { this.name = value; } }

        public Register()
        {
        }
        public Register(RegistryKey key, string path, string name)
        {
            this.key = key;
            this.path = path;
            this.name = name;
        }
        public void SetValue(object value)
        {
            Key.OpenSubKey(Path, true).SetValue(Name, value);
        }
        public object GetValue()
        {
            return Key.OpenSubKey(Path, true).GetValue(Name);
        }
    }
}
