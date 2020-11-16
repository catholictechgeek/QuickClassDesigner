using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickClassDesigner;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPropertyPrintout()
        {
            var prop = new QuickClassDesigner.PropertyData();
            prop.accessmodifer = AccessLevel.@public;
            prop.name = "namez";
            prop.type = typeof(List<string>);
            prop.customgetter = false;
            prop.getteraccessmodifer = AccessLevel.@public;
            prop.customsetter = true;
            prop.setteraccessmodifer = AccessLevel.@public;
            var src = prop.generateSourceCode();
            Console.WriteLine(src);
            prop.setteraccessmodifer = AccessLevel.@private;
            src = prop.generateSourceCode();
            Console.WriteLine(src);
            prop.customgetter = true;
            src = prop.generateSourceCode();
            Console.WriteLine(src);

        }

        [TestMethod]
        public void TestFunctionPrintout()
        {
            var func = new QuickClassDesigner.FunctionData();
            func.accessmodifer = AccessLevel.@public;
            func.returnType = typeof(void);
            func.name = "makeMeAFunction";
            var src = func.generateSourceCode();
            Console.WriteLine(src);
            var param1 = new ParameterData();
            param1.name = "s";
            param1.type = typeof(string);
            func.parameters.Add(param1);
            src = func.generateSourceCode();
            Console.WriteLine(src);
            var param2 = new ParameterData();
            param2.name = "num";
            param2.type = typeof(int);
            func.parameters.Add(param2);
            src = func.generateSourceCode();
            Console.WriteLine(src);
        }

        [TestMethod]
        public void TestInterfacePrintout()
        {
            var protocol = new QuickClassDesigner.InterfaceData();
            protocol.@namespace = "TestableClasses";
            protocol.accessmodifier = AccessLevel.@public;
            protocol.name = "ITestable";
            var func = new QuickClassDesigner.FunctionData();
            func.accessmodifer = AccessLevel.@public;
            func.returnType = typeof(void);
            func.name = "makeMeAFunction";
            protocol.functions.Add(func);
            var src = protocol.generateSourceCode();
            Console.WriteLine(src);
        }

        [TestMethod]
        public void TestClassPrintout()
        {
            var protocol = new QuickClassDesigner.ClassData();
            protocol.@namespace = "TestableClasses";
            protocol.accessmodifier = AccessLevel.@public;
            protocol.name = "TestableClass";
            var func = new QuickClassDesigner.FunctionData();
            func.accessmodifer = AccessLevel.@public;
            func.returnType = typeof(void);
            func.name = "makeMeAFunction";
            protocol.functions.Add(func);
            var src = protocol.generateSourceCode();
            Console.WriteLine(src);
            var func2 = new QuickClassDesigner.FunctionData();
            func2.accessmodifer = AccessLevel.protectedinternal;
            func2.returnType = typeof(object[]);
            func2.overrides = true;
            func2.name = "makeMeAnotherFunction";
            protocol.functions.Add(func2);
            src = protocol.generateSourceCode();
            Console.WriteLine(src);
            var implement = InterfaceData.fromExistingInterface("System.Collections.IEnumerator");
            protocol.implements.Add(implement.name, implement);
            src = protocol.generateSourceCode();
            Console.WriteLine(src);
        }

        [TestMethod]
        public void TestEventPrintout()
        {
            var eventz = new EventData();
            var src = eventz.generateSourceCode();
            Console.WriteLine(src);
        }

        [TestMethod]
        public void TestSingletonPrintout()
        {
            var protocol = new QuickClassDesigner.ClassData();
            protocol.@namespace = "TestableClasses";
            protocol.accessmodifier = AccessLevel.@public;
            protocol.name = "TestableClass";
            var func = new QuickClassDesigner.FunctionData();
            func.accessmodifer = AccessLevel.@public;
            func.returnType = typeof(void);
            func.name = "makeMeAFunction";
            protocol.functions.Add(func);
            var src = protocol.generateSourceCode();
            Console.WriteLine(src);
            var func2 = new QuickClassDesigner.FunctionData();
            func2.accessmodifer = AccessLevel.protectedinternal;
            func2.returnType = typeof(object[]);
            func2.overrides = true;
            func2.name = "makeMeAnotherFunction";
            protocol.functions.Add(func2);
            src = protocol.generateSourceCode();
            Console.WriteLine(src);
            var implement = InterfaceData.fromExistingInterface("System.Collections.IEnumerator");
            protocol.implements.Add(implement.name, implement);
            src = protocol.generateSourceCode();
            Console.WriteLine(src);
            protocol.isSingleton = true;
            src = protocol.generateSourceCode();
            Console.WriteLine(src);
        }
    }
}
