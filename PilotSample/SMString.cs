using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SevMinPilotExt
{
    class SMString
    {
        public string _string;


        public SMString(string initString)
        {
            _string = initString;
        }
        public string Filename()
        {

            string fullFilename = Path.GetFileName(_string);
            return fullFilename.Substring(0, fullFilename.Length - 4);
        }
    }
}
