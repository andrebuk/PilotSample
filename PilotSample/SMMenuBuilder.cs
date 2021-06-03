
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
    public class SMMenuBuilder : IMenu<ObjectsViewContext>
    {
        public string attLinkToDocumentName = "LinkToDocument";
        private readonly IObjectModifier _modifier;
        private readonly IObjectsRepository _repository;
        public string pathToRevitFile;

        [ImportingConstructor]
        public SMMenuBuilder(IObjectModifier modifier, IObjectsRepository repository)
        {
            _modifier = modifier;
            _repository = repository;
        }

        public void Build(IMenuBuilder builder, ObjectsViewContext context)
        {
            //счетчик количества добавленных пунктов
            int itemAddedIndex = 0;
            
            IEnumerable<Ascon.Pilot.SDK.IDataObject> allSelectedObjects = context.SelectedObjects;
            Ascon.Pilot.SDK.IDataObject firstSelectedObject = allSelectedObjects.First();
            //Добавим пункт с названием типа объекта
            //Найдем название типа первого выбранного объекта
             builder.AddItem("ObjectTypeTitle", itemAddedIndex).WithHeader(firstSelectedObject.Type.Title);
            
            itemAddedIndex = +1;
            //Создаем объект из первого выбранного на момет вызова меню
            PBObject currentObject = new PBObject(firstSelectedObject, _repository);
            pathToRevitFile = currentObject.isRevitFamily();
            //Дополнительное меню для федеральных округов

            //string objectName = context.SelectedObjects.First().DisplayName;
            string objectType = context.SelectedObjects.First().Type.Name;
            if
                (currentObject.TypeName() == "folder_fo")
            {
                fo helpfo = new fo();
                helpfo.foName = currentObject.DisplayName();
                string foStructure = helpfo.getfoList();
                IMenuItemBuilder myitem = builder.AddItem("FO", itemAddedIndex).WithHeader("Состав ФО");
                IMenuBuilder subMenu = myitem.WithSubmenu();
                subMenu.AddItem("test", 0).WithHeader(foStructure).WithIsEnabled(false);
                itemAddedIndex = +1;

            }



            //Дополнительные меню для ссылки на документацию
            if (currentObject.CanHasAttribute(attLinkToDocumentName))
            {
                builder.AddItem("CreateDocumentsLink", itemAddedIndex).WithHeader("Привязать документацию");
                itemAddedIndex = +1;
            }
            if (currentObject.HasAttribute(attLinkToDocumentName))
            {
                builder.AddItem("OpenDocuments", itemAddedIndex).WithHeader("Открыть документацию");
                itemAddedIndex = +1;
            }
            if (pathToRevitFile != "")
            {
                builder.AddItem("RevitPath", itemAddedIndex).WithHeader(pathToRevitFile);
                itemAddedIndex = +1;
            }
            else
            {
                builder.AddItem("None", itemAddedIndex).WithHeader("Это не семейство Revit");
                itemAddedIndex = +1;
            }

        }
            //дополнительное меню для работы с Семейством Revit
            



        

        
        
        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {

            if (name == "RevitPath")
            {

                
                //Проверим является ли путь реальным
                if (Directory.Exists(pathToRevitFile))
                {
                    Process.Start("explorer.exe", pathToRevitFile);
                }


                //IObjectBuilder obj = _modifier.Edit(context.SelectedObjects.First());

                //obj.SetAttribute("name", "BrandNewName");
                //_modifier.Apply();

            }
            if (name == "OpenDocuments")
            {

                object AttValue = "";
                context.SelectedObjects.First().Attributes.TryGetValue(attLinkToDocumentName, out AttValue);
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

