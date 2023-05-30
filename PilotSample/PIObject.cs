using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.ObjectCard;

namespace SevMinPilotExt
{
    //class PIObject
    //{
    //}
    [Export(typeof(IObjectCardHandler))]
    public class ObjectCardHandler : IObjectCardHandler
    {
        public bool Handle(IAttributeModifier modifier, ObjectCardContext context)
        {
            //
            //Guid parentObject = context.Parent;
            //String parentName = parentObject.ToString();
            var isObjectModification = context.EditiedObject != null;
            if (isObjectModification || context.IsReadOnly)
                return false;

            var targetStrAttr = context.DisplayAttributes.FirstOrDefault(a => a.Type == AttributeType.String);
            if (targetStrAttr == null)
                return false;

            modifier.SetValue(targetStrAttr.Name, "222");

            var targetAttr = context.DisplayAttributes.FirstOrDefault(a => a.Type == AttributeType.OrgUnit);
            if (targetAttr == null)
                return false;

            var valueToSet = new int[] { 5 };

            modifier.SetValue(targetAttr.Name, valueToSet);
            return true;
        }

        public bool OnValueChanged(IAttribute sender, AttributeValueChangedEventArgs args, IAttributeModifier modifier)
        {
            var currentAttributeValues = string.Empty;
            foreach (var displayAttribute in args.Context.DisplayAttributes)
            {
                if (args.Context.AttributesValues.ContainsKey(displayAttribute.Name))
                    currentAttributeValues += displayAttribute.Name == sender.Name
                        ? args.NewValue
                        : displayAttribute.Name + ": " + args.Context.AttributesValues[displayAttribute.Name] + Environment.NewLine;
            }

            if (args.Context.Type.Name == "Document")
            //if (args.Context.Type.Name == "Document" && sender.Name == "Sheet_number")
            {
                //var newNameAttrValue = "Sheet no " + args.NewValue + "; " + (args.Context.EditiedObject == null ? " New object " : " Existed object");
                var newNameAttrValue = "Sheet no ";
                modifier.SetValue("name", newNameAttrValue);
                return true;
            }

            return false;
        }
    }
}
