using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SevMinPilotExt
{
    class SmString
    {
        public string _string;


        public SmString(string initString)
        {
            _string = initString;
        }
        public string FileName()
        {

            string fullFilename = Path.GetFileName(_string);
            return fullFilename.Substring(0, fullFilename.Length - 4);
        }
        public string FullFileName()
        {

            string fullFilename = Path.GetFileName(_string);
            return fullFilename;
        }
    }
}
