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
        public Ascon.Pilot.SDK.IDataObject _parentObject;
        public IObjectsRepository _repository;
        public IObjectModifier _modifier;


        public PBObject(Ascon.Pilot.SDK.IDataObject parentObject, IObjectsRepository repository)

        {
            _parentObject = parentObject;
            _repository = repository;

        }
        public PBObject(Ascon.Pilot.SDK.IDataObject parentObject, IObjectsRepository repository, IObjectModifier modifier)

        {
            _parentObject = parentObject;
            _repository = repository;
            _modifier = modifier;
        }

        public void TestMethod()
        {

            IType typeOfNewObject = this.TypeByname("BIMContent");
            if (typeOfNewObject != null)
            {
                IObjectBuilder testObject =  _modifier.Create(_parentObject, typeOfNewObject);
                testObject.SetAttribute("name","Тестовый объект");
                testObject.AddFile(@"C:\Temp\Система ТКП.jpg");
                _modifier.Apply();
            }
            else
            {
                MessageBox.Show("Тип не найден");
            }


        }

        public IType TypeByname(string typeName)
        {
            var allTypes = _repository.GetTypes();
            IType result = null;
            foreach (var item in allTypes)
            {
                
                if (item.Name == typeName)
                {
                    result = item;
                    break;

                }
            }
            return result;

        }


        public bool HasAttribute(string attName)
        {
            return _parentObject.Attributes.ContainsKey(attName);
        }
        public bool CanHasAttribute(string attName)
        {
            bool result = false;
            foreach (IAttribute iatt in _parentObject.Type.Attributes)
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
            return _parentObject.DisplayName;
        }
        public string TypeName()
        {
            return _parentObject.Type.Name;
        }
        public string isRevitFamily()
        {
            string path = null;
            if (_parentObject.Type.IsMountable)
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

                try
                {
                    _repository.Mount(_parentObject.Id);
                }
                catch (Exception)
                { }
                //MessageBox.Show("01-" + path);
                try
                {
                    path = _repository.GetStoragePath(_parentObject.Id);
                }
                catch (Exception)
                {
                    path = "Ошибка чтения пути";
                }
                //MessageBox.Show("02-" + path);
            }

            if (path == null)
            { return ""; }
            else
            { return path; }

        }
    }
}
