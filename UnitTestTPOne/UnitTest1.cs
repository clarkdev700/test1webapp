using System;
using ApplicationTest1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;

namespace UnitTestTPOne
{
    [TestClass]
    public class UnitTest1
    {
        private IDal dal = new Dal("users_test.xml");

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void createUserShouldRetunOneAsTotalRecords()
        {
            //initHttpContext();
            //dal = new Dal();
            int totalRecordBeforeRegistration = dal.getAlltUser().Count;
            int totalRecordsExpected = totalRecordBeforeRegistration + 1;
            string username = "Ferdinand";
            string surname = "DENIS";
            string cellPhone = "57002145";
            dal.CreateUser(username, surname, cellPhone);
            int totalRecordAfterRegistration = dal.getAlltUser().Count;
            Assert.AreEqual(totalRecordsExpected, totalRecordAfterRegistration);
        }

        [TestMethod]
        public void editUser()
        {

            int userId = 1;
            var user = dal.find(userId);
            dal.EditUser(userId, user.Username, user.Usersurname, "00123456789");
            var user2 = dal.find(userId);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Usersurname, user2.Usersurname);
            Assert.AreNotEqual(user.UserCellPhone, user2.Usersurname);
        }

        [TestMethod]
        public void removeUserShouldReturnZeroRecords()
        {
            int userId = 1;
            int totalRecordBeforeDeletion = dal.getAlltUser().Count;
            int totalRecordExpectedAfterDeletion = totalRecordBeforeDeletion - 1;
            dal.DeleteUser(userId);
            int totalRecordAfterDeletion = dal.getAlltUser().Count;
            var user = dal.find(userId);
            
            Assert.AreEqual(totalRecordExpectedAfterDeletion, totalRecordAfterDeletion);
            Assert.AreEqual(null, user);
        }
    }
}
