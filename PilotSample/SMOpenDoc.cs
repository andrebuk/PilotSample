
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SevMinPilotExt
{

    [Export(typeof(IMenu<ObjectsViewContext>))]
    public class SMOpenDoc : IMenu<ObjectsViewContext>
    {
        public string attname = "LinkToDocument";
        private readonly IObjectModifier _modifier;
        private readonly IObjectsRepository _repository;

        [ImportingConstructor]
        public SMOpenDoc(IObjectModifier modifier, IObjectsRepository repository)
        {
            _modifier = modifier;
            _repository = repository;
        }

        public void Build(IMenuBuilder builder, ObjectsViewContext context)
        {
            //string prefix = "Вы выбрали: ";
            //string MenuName =  prefix + context.SelectedObjects.First().DisplayName;

            IPerson curPerson = _repository.GetCurrentPerson();
            var curPersonName = curPerson.DisplayName;

            var selectedItems = context.SelectedObjects;
            //проверяю доступ к файлам

            //var files = selectedItems.First().Relations;
            string path="";
            Ascon.Pilot.SDK.IDataObject firstObject = selectedItems.First();
            if (firstObject.Type.IsMountable)
            {

                Guid objectGUID = selectedItems.First().Id;

                try
                {
                    _repository.Mount(objectGUID);
                }
                catch (Exception e)
                {

                }

                path = _repository.GetStoragePath(objectGUID);
            }


            //string MenuName = "Id: " + Info.ToString() + " Пользователь: " + curPersonName;
            string MenuName = "Путь: " + path;

            builder.AddItem("GetInfo", 0).WithHeader(MenuName);

            //MessageBox.Show(path,"Alert");


            if (canHasAttribute(context, attname))
            {

                builder.AddItem("CreateDocumentsLink", 1).WithHeader("Привязать документацию");
            }
            if (hasAttribute(context, attname))
            {
                builder.AddItem("OpenDocuments", 2).WithHeader("Открыть документацию");
            }




        }

        public bool hasAttribute(ObjectsViewContext context, string attributeName)
        {
            bool HasAttribute = context.SelectedObjects.First().Attributes.ContainsKey(attributeName);
            return HasAttribute;
        }
        public bool canHasAttribute(ObjectsViewContext context, string attributeName)
        {

            bool result = false;
            foreach (IAttribute iatt in context.SelectedObjects.First().Type.Attributes)
            {
                if (iatt.Name == attributeName)
                {
                    result = true;
                }
            }
            return result;
        }
        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {

            if (name == "OpenDocuments")
            {

                object AttValue = "";
                context.SelectedObjects.First().Attributes.TryGetValue(attname, out AttValue);
                string path = AttValue.ToString();
                //Проверим является ли путь реальным
                if (Directory.Exists(path))
                {
                    Process.Start("explorer.exe", path);
                }


                //IObjectBuilder obj = _modifier.Edit(context.SelectedObjects.First());

                //obj.SetAttribute("name", "BrandNewName");
                //_modifier.Apply();

            }
            if (name == "CreateDocumentsLink")
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.FileName = "Укажите любой файл внутри папки с документацией";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetDirectoryName(folderBrowser.FileName);

                    IObjectBuilder obj = _modifier.Edit(context.SelectedObjects.First());

                    obj.SetAttribute("LinkToDocument", folderPath);
                    _modifier.Apply();
                }

            }

        }
    }


}

