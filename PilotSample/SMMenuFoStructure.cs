
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
    public class SMMenuFoStructure : IMenu<ObjectsViewContext>
    {
       
        private readonly IObjectModifier _modifier;
        private readonly IObjectsRepository _repository;

        [ImportingConstructor]
        public SMMenuFoStructure(IObjectModifier modifier, IObjectsRepository repository)
        {
            _modifier = modifier;
            _repository = repository;
        }

        public void Build(IMenuBuilder builder, ObjectsViewContext context)
        {

            string objectName = context.SelectedObjects.First().DisplayName;
            string objectType = context.SelectedObjects.First().Type.Name;
            if
                (objectType == "folder_fo")
            {
                fo helpfo = new fo();
                helpfo.foName = objectName;
                string foStructure = helpfo.getfoList();
                IMenuItemBuilder myitem = builder.AddItem("FO", 0).WithHeader("Состав ФО");
                IMenuBuilder subMenu = myitem.WithSubmenu();
                subMenu.AddItem("test", 0).WithHeader(foStructure).WithIsEnabled(false);

            }
        }

        public void OnMenuItemClick(string name, ObjectsViewContext context)
        {
            
        }
    }
}

