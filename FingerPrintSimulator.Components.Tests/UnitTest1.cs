using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FingerPrintSimulator.Components.Model;
using FingerPrintSimulator.Components.ServicesImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpRepository.Repository;
using Shouldly;

namespace FingerPrintSimulator.Components.Tests
{
    [TestClass]
    public class FingerPrintEngineTests
    {
        private IDictionary<string, UserEntry> data;

        [TestInitialize]
        public void Setup()
        {
            var key = string.Empty;
            var user = default(UserEntry);
            this.data = new Dictionary<string, UserEntry>();

            for (int i = 1; i <= 3; i++)
            {
                key = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{i}"));
                user = new UserEntry() { Name = $"Demo{i}", FingerPrint = key };
                data.Add(key, user);
            }


        }

        [TestMethod]
        public void EnrollData()
        {
            // Arrange
            var md5 = MD5.Create();
            var repositoryMock = new Mock<IRepository<UserEntry, string>>();
            repositoryMock.Setup(m => m.GetAll()).Returns(this.data.Values);
            repositoryMock.Setup(m => m.Add(It.IsAny<UserEntry>())).Callback<UserEntry>(u => this.data.Add(u.FingerPrint, u));
            var fingerPrintEngine = new FingerPrintEngine(repositoryMock.Object);
            var bytes = Encoding.ASCII.GetBytes("4");
            var expectedFingerPrint = Convert.ToBase64String(bytes);

            // Act
            var userEntry = fingerPrintEngine.EnrollFingerPrint("Demo4", bytes);

            // Assert
            data.Count.ShouldBe(4);
            data.Values.Last().FingerPrint.ShouldBe(expectedFingerPrint);
        }

        [TestMethod]
        public void GetInfoForFingerPrint_ShouldReturn_ExistingData()
        {
            // Arrange
            var md5 = MD5.Create();
            var repositoryMock = new Mock<IRepository<UserEntry, string>>();
            repositoryMock.Setup(m => m.Get(It.IsAny<string>())).Returns<string>(key => data[key]);
            var fingerPrintEngine = new FingerPrintEngine(repositoryMock.Object);
            var expectedFingerPrint = Convert.ToBase64String(Encoding.ASCII.GetBytes("1"));

            // Act
            var user = fingerPrintEngine.GetInfoForFingerPrint(Encoding.ASCII.GetBytes("1"));

            // Assert
            user.Name.ShouldBe("Demo1");
        }
    }
}
