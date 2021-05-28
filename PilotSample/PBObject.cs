using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ascon.Pilot.SDK;

namespace SevMinPilotExt
{
    class PBObject

    {
        public Ascon.Pilot.SDK.IDataObject _iDataObject;
        public IObjectsRepository _repository;

        public PBObject(Ascon.Pilot.SDK.IDataObject iDataObject, IObjectsRepository repository)

        {
            _iDataObject = iDataObject;
            _repository = repository;
        }

        public bool HasAttribute(string attName)
        {
            return _iDataObject.Attributes.ContainsKey(attName);
        }
        public bool CanHasAttribute(string attName)
        {
            bool result = false;
            foreach (IAttribute iatt in _iDataObject.Type.Attributes)
            {
                if (iatt.Name == attName)
                {
                    result = true;
                }
            }
            return result;
        }
        public string DisplayName()
        {
            return _iDataObject.DisplayName;
        }
        public string TypeName()
        {
            return _iDataObject.Type.Name;
        }
        public string isRevitFamily()
        {
            string path = null;
            if (_iDataObject.Type.IsMountable)
            {
                //foreach (IRelation item in _iDataObject.Relations)
                //{
                //    try
                //    {
                //        path = _repository.GetStoragePath(item.Id);
                //    }
                //    catch (Exception e)
                //    {
                //        MessageBox.Show(e.ToString());
                //    }
                //    MessageBox.Show(path);
                //}

                //try
                //{
                //    _repository.Mount(_iDataObject.Id);
                //}
                //catch (Exception)
                //{ }

                try
                {
                    path = _repository.GetStoragePath(_iDataObject.Id);
                }
                catch (Exception)
                {
                    path = null;
                }
            }

            if (path == null)
            { return ""; }
            else
            { return path; }

        }
    }
}
