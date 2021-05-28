using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Menu;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace SevMinPilotExt
{
    [Export(typeof(IMenu<MainViewContext>))]
    public class SMMenuStorage : IMenu<MainViewContext>
    {

        public void Build(IMenuBuilder builder, MainViewContext context)
        {
            //var allItemsInMenu = builder.ItemNames;
            //builder.AddItem("firstItem", (allItemsInMenu.Count())).WithHeader("Привет");
        }

        public void OnMenuItemClick(string name, MainViewContext context)
        {
            //MessageBox.Show("You clicked!");

        }
    }
}
