using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using static QuickClassDesigner.MiscUtils;

namespace QuickClassDesigner
{
    public class InterfaceData:IGeneratable
    {
        public InterfaceData()
        {
            namespaces = new ObservableCollection<string>();
            properties = new List<PropertyData>();
            functions = new List<FunctionData>();
        }

        public string @namespace
        {
            get;
            set;
        }

        public AccessLevel accessmodifier
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public List<PropertyData> properties
        {
            get;
            set;
        }

        public List<FunctionData> functions
        {
            get;
            set;
        }

        public Type extends
        {
            get;
            set;
        }

        public ObservableCollection<string> namespaces
        {
            get;
            set;
        }


        public string generateSourceCode(int separatorcount = 1)
        {
            //string spacing = separator;
            
            StringBuilder builder = new StringBuilder();

            //first, generate the using namespaces
            foreach(var space in namespaces)
            {
                builder.AppendLine($"using {space};");
            }

            //next, get the namepace definition
            builder.AppendLine($"namespace {@namespace}");
            builder.AppendLine("{");

            separatorcount = 1;

            //next, get the interface definition
            builder.AppendLine($"{separator}{getAccessModifierString(accessmodifier)} interface {name}");
            builder.AppendLine($"{separator}{{");

            //next, let's try to add the functions
            foreach(FunctionData func in this.functions)
            {
                builder.AppendLine(func.generateSourceCodeForInterface(separatorcount + 1));
            }

            //next, append the closing brace for interface
            builder.AppendLine(getSeparater(2));
            builder.AppendLine($"{separator}}}");

            //last, append the closing brace for namespace
            builder.Append('}');

            return builder.ToString();
        }

        public static InterfaceData fromExistingInterface(string s)
        {
            var data = new InterfaceData();
            data.accessmodifier = AccessLevel.@public;
            var t = System.Reflection.TypeInfo.GetType(s);
            data.@namespace = t.Namespace;
            data.name = MiscUtils.getClassName(t);
            var funcs = t.GetMethods();
            foreach (var func in funcs)
            {
                data.functions.Add(FunctionData.getDataForExistingFunction(func));
            }
            return data;
        }
    }
}
