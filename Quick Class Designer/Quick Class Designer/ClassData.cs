using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static QuickClassDesigner.MiscUtils;

namespace QuickClassDesigner
{
    public class ClassData: IGeneratable
    {
        public ClassData()
        {
            namespaces = new ObservableCollection<string>();
            variables = new List<VariableData>();
            functions = new List<FunctionData>();
            implements = new Dictionary<string,InterfaceData>();
            accessmodifier = AccessLevel.@public;
            @namespace = "";
            name = "";
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

        public bool isGeneric
        {
            get;
            set;
        }

        public bool isStatic
        {
            get;
            set;
        }

        public bool isSealed
        {
            get;
            set;
        }

        public bool isReadOnly
        {
            get;
            set;
        }

        public bool isAbstract
        {
            get;
            set;
        }

        public bool isSingleton
        {
            get;
            set;
        }

        public bool canSerialize
        {
            get;
            set;
        }

        public bool customserial
        {
            get;
            set;
        }

        public string customserialname
        {
            get;
            set;
        }

        public List<VariableData> variables
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

        public Dictionary<string, InterfaceData> implements
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
            for (int i = 0; i < namespaces.Count; i++)
            {
                builder.AppendLine($"using {namespaces[i]};");
            }

            //next, get the namepace definition
            builder.AppendLine($"namespace {@namespace}");
            builder.AppendLine("{");

            //separatorcount = 1;

            //next, get the class definition
            //check if we can serialize
            if(canSerialize)
            {
                //string customparams = $"Name={customserialname})";
                builder.AppendLine($"{getSeparater(separatorcount)}[DataContract{(customserial ? $"(Name={customserialname})" : String.Empty)}]\n");
            }
            builder.Append ($"{separator}{getAccessModifierString(accessmodifier)}{(this.isStatic ? " static" : "")}{(this.isSealed ? " sealed" : "")} class {name}");
            if (extends != null && implements.Count > 0)
            {
                var enumerator = implements.Keys.GetEnumerator();
                string block = ", ";
                /*
                string interfaces = implements[0].name;
                for (int i = 1; i < implements.Count; i++)
                {
                    String.Concat(interfaces, block, implements[i].name);
                }
                */
                enumerator.MoveNext();
                string interfaces = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    String.Concat(interfaces, block, enumerator.Current);
                }
                builder.AppendLine($":{getClassName(extends)}, {interfaces}");
            }
            else if(extends != null)
            {
                builder.AppendLine($":{getClassName(extends)}");
            }
            else if(implements.Count > 0)
            {
                var enumerator = implements.Keys.GetEnumerator();
                string block = ", ";
                enumerator.MoveNext();
                string interfaces = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    String.Concat(interfaces, block, enumerator.Current);
                }
                builder.AppendLine($":{interfaces}");
            }
            else
            {
                builder.AppendLine();
            }
            builder.AppendLine($"{separator}{{");

            //next, get class fields
            foreach(var variable in this.variables)
            {
                variable.generateSourceCode(separatorcount + 1);
            }

            //next, let's not forget the constructor
            if (isSingleton)
            {
                builder.AppendLine($"{getSeparater(separatorcount + 1)}public static {this.name} instance;\n");
                builder.AppendLine($"{getSeparater(separatorcount + 1)}private {this.name}()\n{getSeparater(separatorcount + 1)}{{\n{getSeparater(separatorcount + 2)}\n{getSeparater(separatorcount + 1)}}}");
            }
            else
            {
                if(variables.Count > 0)
                {
                    builder.AppendLine();
                }
                builder.AppendLine($"{getSeparater(separatorcount + 1)}{getAccessModifierString(this.accessmodifier)} {this.name}()\n{getSeparater(separatorcount + 1)}{{\n{getSeparater(separatorcount + 2)}\n{getSeparater(separatorcount + 1)}}}");
            }
            builder.AppendLine();

            //next, let's try to add the functions
            //first, we must get all functions from interfaces
            foreach(var functionimplements in this.implements)
            {
                foreach (FunctionData func in functionimplements.Value.functions)
                {
                    builder.AppendLine(func.generateSourceCode(separatorcount + 1));
                    builder.AppendLine();
                }
            }

            //next, we get all virtual and abstract functions from the base class we can extend
            if (extends != null)
            {
                var funcs = extends.GetMethods();
                foreach (var func in funcs)
                {
                    if (func.IsVirtual || func.IsAbstract)
                    {
                        //I might change this later based on how I want to organizae everything with inheritance
                        builder.AppendLine(FunctionData.getDataForExistingFunction(func).generateSourceCode(separatorcount + 1));
                        builder.AppendLine();
                    }
                }
            }

            //last, we get all functions defined in this specific class
            foreach (FunctionData func in this.functions)
            {
                builder.AppendLine(func.generateSourceCode(separatorcount + 1));
                builder.AppendLine();
            }

            //next, append the closing brace for interface
            builder.AppendLine(getSeparater(2));
            builder.AppendLine($"{separator}}}");

            //last, append the closing brace for namespace
            builder.Append('}');

            return builder.ToString();
        }
    }
}
