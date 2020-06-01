using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertNumberToWord;
using ConvertNumberToWord.Controllers;

namespace ConvertNumberToWord.Tests.Controllers
{
    [TestClass]
    public class NumToWordControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            NumToWordController controller = new NumToWordController();

            // Act
            string result = controller.Get("3258.34") as string;
            string result1 = controller.Get("123.45") as string;
            string result2 = controller.Get("325855") as string;
            string result3 = controller.Get("10000.34") as string;
            string result4 = controller.Get("-133.06") as string;

            // Assert
            Assert.AreEqual("Three Thousand Two Hundred and Fifty-Eight dollars and Thirty-Four cents", result);
            Assert.AreEqual("One Hundred and Twenty-Three dollars and Forty-Five cents", result1);
            Assert.AreEqual("Three hundred thousand Twenty-Five Thousand Eight Hundred and Fifty-Five dollars", result2);
            Assert.AreEqual("Ten Thousand dollars and Thirty-Four cents", result3);
            Assert.AreEqual("Minus One Hundred and Thirty-Three dollars and Six cents", result4);
        }
    }
}
