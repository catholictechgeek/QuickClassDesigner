using System;
using System.Collections.Generic;

namespace QuickClassDesigner
{
    public static class MiscUtils
    {
        internal const string separator = "    ";
        public static string getSeparater(int count = 1)
        {
            if(count < 1)
            {
                throw new ArgumentException("separator multiplier cannot be less than 1");
            }
            else if (count == 1)
            {
                return separator;
            }
            else
            {
                return new String(' ', (separator.Length * count));
            }
        }

        public static string getClassName(Type t)
        {
#if DEBUG
            //string name = (t.IsGenericType) ? t.Name.Remove(t.Name.LastIndexOf('`')): t.Name;
            Console.WriteLine(getTrueClassName(t));
#endif

            if (t.IsGenericType)
            {
                var generics = t.GetGenericArguments();
                if (generics.Length == 1)
                {
                    return $"{getTrueClassName(t)}<{getClassName(generics[0])}>";
                }
                string longstring = $"{getTrueClassName(t)}<{getClassName(generics[0])}";
                for (int i = 1; i < generics.Length; i++)
                {
                    longstring = $"{longstring}, {getClassName(generics[i])}";

                }
                longstring += '>';
                return longstring;
            }
            else
            {
                return getTrueClassName(t);
            }
        }

        private static string getTrueClassName(Type t)
        {
            //take into account special case of "void" and "object" since it is always lowercase
            if (t == typeof(void) || t == typeof(object) || t == typeof(object[]) || t == typeof(char) || t == typeof(decimal))
            {
                return t.Name.ToLower();
            }
            //next, deal with numbers
            else if(t == typeof(int))
            {
                return "int";
            }
            else if(t == typeof(double))
            {
                return "double";
            }
            else if(t == typeof(short))
            {
                return "short";
            }
            else if(t == typeof(long))
            {
                return "long";
            }
            else if (t == typeof(bool))
            {
                return "bool";
            }
            return (t.IsGenericType) ? t.Name.Remove(t.Name.LastIndexOf('`')) : t.Name;
        }

        public static string getAccessModifierString(AccessLevel? l)
        {
            if(!l.HasValue)
            {
                return "";
            }
            else if (l == AccessLevel.protectedinternal)
            {
                return "protected internal";
            }
            else
            {
                return l.ToString();
            }
        }

        internal static AccessLevel getAccessModifierForExisting(Type t)
        {
            return AccessLevel.@public;
        }
    }
}
