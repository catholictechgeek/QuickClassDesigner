using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using QuickClassDesigner;
using static QuickClassDesigner.ValidateHelper;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class CodeValidationTest
    {
        [TestMethod]
        public async Task TestCodeCompletion()
        {
            //string st = "Lis";
            string st = "StringB";
            //var result = await ValidateHelper.getSuggestions(st);
            var result = await ValidateHelper.getSuggestions(st);
            Assert.IsNotNull(result);
        }
    }
}
