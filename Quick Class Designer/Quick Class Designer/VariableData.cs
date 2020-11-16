using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using static QuickClassDesigner.MiscUtils;

#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTestProject")]
#endif

namespace QuickClassDesigner
{
    public class VariableData: IGeneratable
    {
        public VariableData()
        {
        }

        public AccessLevel accessmodifer
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public Type type
        {
            get;
            set;
        }

        public virtual string generateSourceCode(int separatorcount = 1)
        {
            return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} {getClassName(type)} {name}";
        }
    }

    public class PropertyData : VariableData, IGeneratable
    {
        public bool customgetter
        {
            get;
            set;
        }

        public bool customsetter
        {
            get;
            set;
        }

        public AccessLevel? getteraccessmodifer
        {
            get;
            set;
        }

        public AccessLevel? setteraccessmodifer
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

        public override string generateSourceCode(int separatorcount = 1)
        {
            //get first part of property definition started
            string customparams = $"(Name={customserialname})";
            string serializetext = canSerialize ? $"{getSeparater(separatorcount)}[DataMember{(customserial ? customparams : String.Empty)}]\n" : "";
            string property = $"{serializetext}{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} {getClassName(type)} {name}\n{getSeparater(separatorcount)}{{\n{getSeparater(separatorcount + 1)}";
            //next, check if we have getter
            if (getteraccessmodifer != null)
            {
                string access = (this.accessmodifer != this.getteraccessmodifer) ? $"{getAccessModifierString(getteraccessmodifer)} " : "";
                if (customgetter)
                {
                    property = $"{property}{access}get\n{getSeparater(separatorcount + 1)}{{\n{getSeparater(separatorcount + 2)}\n{getSeparater(separatorcount + 1)}}}\n";
                }
                else
                {
                    property = $"{property}{access}get;\n{getSeparater(separatorcount + 1)}";
                }
            }

            if(customgetter && setteraccessmodifer != null)
            {
                property += getSeparater(separatorcount + 1);
            }

            if (setteraccessmodifer != null)
            {
                string access = (this.accessmodifer != this.setteraccessmodifer) ? $"{getAccessModifierString(setteraccessmodifer)} " : "";
                if (customsetter)
                {
                    property = $"{property}{access}set\n{getSeparater(separatorcount + 1)}{{\n{getSeparater(separatorcount + 2)}\n{getSeparater(separatorcount + 1)}}}\n";
                }
                else
                {
                    property = $"{property}{access}set;\n{getSeparater(separatorcount + 1)}";
                }
            }

            //property += '}';
            property = String.Concat(property, getSeparater(separatorcount), "}");

            return property;
        }
    }

    public class FunctionData: IGeneratable
    {
        public FunctionData()
        {
            //parameters = new Lst<ParameterData>();
            parameters = new ObservableCollection<ParameterData>();
        }

        public AccessLevel accessmodifer
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public Type returnType
        {
            get;
            set;
        }

        public IList<ParameterData> parameters
        {
            get;
            set;
        }

        public bool isGeneric
        {
            get;
            set;
        }

        public bool isAbstract
        {
            get;
            set;
        }

        public bool isStatic
        {
            get;
            set;
        }

        internal bool overrides
        {
            get;
            set;
        }

        public string generateSourceCode(int separatorcount = 1)
        {
            if (this.isAbstract)
            {
                if (parameters.Count == 0)
                {
                    return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} abstract {getClassName(returnType)} {name}();";
                }
                else
                {
                    string paramstring = "";
                    return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} abstract {getClassName(returnType)} {name}({paramstring});"; ;
                }
            }
            else
            {
                if (parameters.Count == 0)
                {
                    return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)}{(this.overrides ? " override" : "")} {getClassName(returnType)} {name}()\n{getSeparater(separatorcount)}{{\n{getSeparater(separatorcount + 1)}\n{getSeparater(separatorcount)}}}";
                }
                else
                {
                    string block = ", ";
                    string paramstring = parameters[0].generateSourceCode();
                    for (int i = 1; i < parameters.Count; i++)
                    {
                        paramstring = String.Concat(paramstring, block, parameters[i].generateSourceCode());
                    }
                    return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)}{(this.overrides ? " override" : "")} {getClassName(returnType)} {name}({paramstring})\n{getSeparater(separatorcount)}{{\n{getSeparater(separatorcount + 1)}\n{getSeparater(separatorcount)}}}"; ;
                }
            }
        }

        public string generateSourceCodeForInterface(int separatorcount = 1)
        {
            if (parameters.Count == 0)
            {
                return $"{getSeparater(separatorcount)}{getClassName(returnType)} {name}()\n{getSeparater(separatorcount)}{{\n{getSeparater(separatorcount + 1)}\n{getSeparater(separatorcount)}}}";
            }
            else
            {
                return "";
            }
        }

        public static FunctionData getDataForExistingFunction(MethodInfo function)
        {
            FunctionData data = new FunctionData();
            data.accessmodifer = AccessLevel.@public;
            data.name = function.Name;
            data.returnType = function.ReturnType;
            if (function.IsVirtual || function.IsAbstract)
            {
                data.overrides = true;
            }
            var @params = function.GetParameters();
            if(@params.Length > 0)
            {
                foreach(var parameter in @params)
                {
                    var paramdata = new ParameterData();
                    paramdata.name = parameter.Name;
                    paramdata.type = parameter.ParameterType;
                    paramdata.isIn = parameter.IsIn;
                    paramdata.isOut = parameter.IsOut;
                    //check this next one to make sure
                    paramdata.isRef = parameter.IsRetval;
                    data.parameters.Add(paramdata);
                }
            }
            return data;
        }
    }

    public class ParameterData:IGeneratable
    {
        public string name
        {
            get;
            set;
        }

        public Type type
        {
            get;
            set;
        }

        public bool isIn
        {
            get;
            set;
        }

        public bool isOut
        {
            get;
            set;
        }

        public bool isRef
        {
            get;
            set;
        }

        internal bool optionTaken
        {
            get
            {
                return isIn || isOut || isRef;
            }
        }

        //here, separator count doesn't matter (but unforunately, I have to have it as a parameter anyway in order to fit the interface)
        public string generateSourceCode(int separatorcount = 1)
        {
            string input = "";
            //we can only have one qualifier, so we have to see if there's more than one
            bool testing = isIn;
            if((testing && isOut) || (testing && isRef))
            {
                throw new InvalidOperationException("parameters cannot have both in and out qualifiers at the same time");
            }

            if(isIn)
            {
                input = "in ";
            }
            else if(isOut)
            {
                input = "out ";
            }
            else if(isRef)
            {
                input = "ref ";
            }

            input = $"{input}{getClassName(type)} {name}";
            return input;
        }
    }

    public class EventData : IGeneratable
    {
        public EventData()
        {
            var func = new DelegateData();
            func.returnType = typeof(void);
            var sender = new ParameterData();
            sender.name = "sender";
            sender.type = typeof(object);
            func.parameters.Add(sender);
            var args = new ParameterData();
            args.name = "args";
            args.type = typeof(EventArgs);
            func.parameters.Add(args);
            @delegate = func;
        }

        public AccessLevel accessmodifer
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public DelegateData @delegate {
            get;
            set;
        }

        public bool isCustomEvent
        {
            get;
            set;
        }

        /*
        public event EventHandler handle
        {
            add
            {

            }
            remove
            {

            }
        }
        */

        public string generateSourceCode(int separatorcount = 1)
        {
            if (isCustomEvent)
            {
                StringBuilder builder = new StringBuilder($"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} event {@delegate.name} {name}");
                builder.AppendLine($"{getSeparater(separatorcount + 1)} add");
                builder.AppendLine($"{getSeparater(separatorcount + 1)} {{");
                builder.AppendLine(getSeparater(separatorcount + 2));
                builder.AppendLine($"{getSeparater(separatorcount + 1)} }}");
                builder.AppendLine($"{getSeparater(separatorcount + 1)} remove");
                builder.AppendLine($"{getSeparater(separatorcount + 1)} {{");
                builder.AppendLine(getSeparater(separatorcount + 2));
                builder.AppendLine($"{getSeparater(separatorcount + 1)} }}");
                return builder.ToString();

            }
            else
            {
                return $"{getSeparater(separatorcount)}{getAccessModifierString(accessmodifer)} event {@delegate.name} {name};";
            }
        }
    }

    public class DelegateData:IGeneratable
    {
        public DelegateData()
        {
            parameters = new List<ParameterData>();
        }

        public AccessLevel accessmodifer
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public Type returnType
        {
            get;
            set;
        }

        public List<ParameterData> parameters
        {
            get;
            set;
        }

        public string generateSourceCode(int separatorcount = 1)
        {
            string block = ", ";
            string paramstring = parameters[0].generateSourceCode();
            for (int i = 1; i < parameters.Count; i++)
            {
                paramstring = String.Concat(paramstring, block, parameters[i].generateSourceCode());
            }
            return $"{getSeparater(separatorcount)}{MiscUtils.getAccessModifierString(this.accessmodifer)} {name}({paramstring});";
        }

        public static DelegateData fromExisting(Delegate d)
        {
            DelegateData pointer = new DelegateData();
            var func = d.Method;
            pointer.name = func.Name;
            pointer.returnType = func.ReturnType;
            var param = func.GetParameters();
            foreach(var paremeter in param)
            {
                var input = new ParameterData();
                input.name = paremeter.Name;
                input.type = paremeter.ParameterType;
                pointer.parameters.Add(input);
            }
            return pointer;
        }
    }
}
