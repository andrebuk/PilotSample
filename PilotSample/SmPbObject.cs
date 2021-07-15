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
    class SmPbObject

    {
        public Ascon.Pilot.SDK.IDataObject _parentObject;
        public IObjectsRepository _repository;
        public IObjectModifier _modifier;

        [ImportingConstructor]

        public SmPbObject(Ascon.Pilot.SDK.IDataObject parentObject, IObjectsRepository repository)

        {
            _parentObject = parentObject;
            _repository = repository;

        }
        public SmPbObject(Ascon.Pilot.SDK.IDataObject parentObject, IObjectsRepository repository, IObjectModifier modifier)

        {
            _parentObject = parentObject;
            _repository = repository;
            _modifier = modifier;
        }

        public void CreateObjectsFormFiles()
        {
            string[] filesInFolder;
            IType typeOfObject = this.TypeByname("Семейство");
            IType typeOfFileObject = this.TypeByname("Файл");




            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            
                {
                    filesInFolder = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string item in filesInFolder)
                    {
                        using (FileStream stream = File.OpenRead(item))
                        {
                            SmString objectName = new SmString(item);

                        IObjectBuilder objectBuilder = _modifier.Create(_parentObject, typeOfObject);
                        objectBuilder.SetAttribute("name", objectName.FileName());

                        Ascon.Pilot.SDK.IDataObject newObject = objectBuilder.DataObject;
                        _modifier.Apply();
                        //добавим объект типа File для того чтобы потом добавить к нему собственно файл

                        IObjectBuilder fileObjectBuilder = _modifier.Create(newObject, typeOfFileObject);
                        fileObjectBuilder.SetAttribute("Title 4C281306-E329-423A-AF45-7B39EC30273F", objectName.FullFileName());
                        fileObjectBuilder.AddFile(item, stream, DateTime.Now, DateTime.Now, DateTime.Now);
                        _modifier.Apply();

                        }

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

                if (item.Title == typeName)
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
        public string IsRevitFamily()
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
