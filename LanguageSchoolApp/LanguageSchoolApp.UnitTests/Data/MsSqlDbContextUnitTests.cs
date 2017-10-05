using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Data;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Models.Home;
using LanguageSchoolApp.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Data
{
    [TestFixture]
    public class MsSqlDbContextUnitTests
    {
        [Test]
        public void Create_ShouoldReturnNewInstanceOfMsSqlDbContextClass()
        {
            // Arrange and Act
            using (var result = MsSqlDbContext.Create())
            {
                // Assert
                Assert.IsInstanceOf<MsSqlDbContext>(result);
                Assert.AreEqual("MsSqlDbContext", result.GetType().Name);
            }
        }
    }
}
