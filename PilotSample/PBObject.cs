using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Ascon.Pilot.SDK;

namespace SevMinPilotExt
{
    class PBObject

    {
        public Ascon.Pilot.SDK.IDataObject _parentObject;
        public IObjectsRepository _repository;
        public IObjectModifier _modifier;

        [ImportingConstructor]

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
            string[] filesInFolder;
            
            IType typeOfNewObject = this.TypeByname("bim_family");
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                //if (true)
                {
                    filesInFolder = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string item in filesInFolder)
                    {
                        
                        SMString objectName = new SMString(item);

                        IObjectBuilder objectBuilder = _modifier.Create(_parentObject, typeOfNewObject);
                        objectBuilder.SetAttribute("name", objectName.Filename());
                        //objectBuilder.SetAttribute("path", item);

                        objectBuilder.AddFile(item,new MemoryStream(), DateTime.Now, DateTime.Now, DateTime.Now) ;
                        //Ascon.Pilot.SDK.IDataObject newObject = objectBuilder.DataObject;
                        _modifier.Apply();


                    }

                }
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
