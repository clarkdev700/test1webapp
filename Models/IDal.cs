using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest1.Models
{
    public interface IDal 
    {
        User find(int id);
        void CreateUser(string username, string usrname, string cellphone);
        void EditUser(int userId, string username, string usrname, string cellphone);
        void DeleteUser(int userId);
        List<User> getAlltUser();
    }
}
