using System;

namespace ConsoleApplication.Annotations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    class DesignPattern : Attribute
    {
        private string _name;

        public DesignPattern(string name)
        {
            _name = name;
        }
    }
}
