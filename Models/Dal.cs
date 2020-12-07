using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ApplicationTest1.Models
{
    public class Dal : IDal
    {
        private List<User> users;
        private XElement usersRecords;
        string  usersFilepath = "";

        public Dal()
        {
            users = new List<User>();
            var filename = "users.xml";
             usersFilepath = HttpContext.Current.Server.MapPath(System.IO.Path.Combine("~/App_Data/", filename));       
            if (!File.Exists(usersFilepath))
            {
                //create file
                XDocument doc = new XDocument(
                    new XElement("Users","")
                    );
                doc.Save(usersFilepath);
            }
            usersRecords = XElement.Load(usersFilepath);
        }

        public Dal(string filename) // testing purpose
        {
            users = new List<User>();
            usersFilepath =  filename;
            if (!File.Exists(usersFilepath))
            {
                //create file
                XDocument doc = new XDocument(
                    new XElement("Users", "")
                    );
                doc.Save(usersFilepath);
            }
            usersRecords = XElement.Load(usersFilepath);
        }

        public User find(int id)
        {
            var ex = getNode(id);
            if (ex == null)
                return null;
            return new User { UserId = (int) ex.Element("UserId"),
                            Username = (string) ex.Element("Username"), 
                            Usersurname = (string)ex.Element("Usersurname"), 
                            UserCellPhone = (string)ex.Element("UserCellPhone") };
        }
        public void CreateUser(string username, string usersurname, string cellphone)
        {
           
            int userID = getLastId() + 1;

            XElement user = new XElement("User",
                            new XElement("UserId", userID),
                            new XElement("Username", username),
                            new XElement("Usersurname", usersurname),
                            new XElement("UserCellPhone", cellphone)
                        );

            //insert data in file
            usersRecords.Add(user);
            usersRecords.Save(usersFilepath);
        }

        public void EditUser(int userId, string username, string usersurname, string cellphone)
        {
            var node = from elt in usersRecords.Descendants("User")
                       where (int)elt.Element("UserId") == userId
                       select elt;

            if (node != null)
            {
                foreach(var elt in node)
                {
                    elt.Element("Username").SetValue(username);
                    elt.Element("Usersurname").SetValue(usersurname);
                    elt.Element("UserCellPhone").SetValue(cellphone);
                }
                usersRecords.Save(usersFilepath);
            }
        }

        public void DeleteUser(int userId)
        {
            var node = getNode(userId);
            if (node != null)
                node.Remove();
            usersRecords.Save(usersFilepath);
        }

        public XElement getNode(int id)
        {
            var node = (from elt in usersRecords.Descendants("User")
                        where (int)elt.Element("UserId") == id
                        select elt).ToList().FirstOrDefault();
            return node;
        }

        public List<User> getAlltUser()
        {
            var users = new List<User>();
            var nodes = usersRecords.Descendants("User").ToList();
            if (nodes != null)
            {
                foreach(var elt in nodes)
                {
                    users.Add(new User { UserId = (int)elt.Element("UserId"), 
                        Username = (string) elt.Element("Username"), 
                        Usersurname = (string) elt.Element("Usersurname"), 
                        UserCellPhone = (string)elt.Element("UserCellPhone")
                    });
                }
                return users;
            }

            return null;
        }


        private int getLastId()
        {
            var maxId = (from elt in usersRecords.Descendants("User")
                         orderby (int) elt.Element("UserId") descending
                         select (int) elt.Element("UserId")).ToList().FirstOrDefault();

             return (int) maxId;
        }

    }
}