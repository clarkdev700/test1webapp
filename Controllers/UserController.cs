using ApplicationTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationTest1.Controllers
{
    public class UserController : Controller
    {
        private IDal dal;

        public UserController()
        {
            dal = new Dal();
        }

        // GET: User
        [Route("user/index", Name = "_indexUser")]
        public ActionResult Index()
        {
            var users = dal.getAlltUser();
            ViewBag.Response = Session["response"]??null;
            Session.Remove("response");
            return View(users);
        }



        // GET: User/Create
        [Route("user/create", Name = "_addUser")]
        [HttpGet]
        public ActionResult Create()
        { 
            return View(new User());
        }

        // POST: User/Create
        [Route("user/create", Name = "_addPostUser")]
        [HttpPost]
        public ActionResult Create(User user)
        {
            //FormCollection collection,
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    dal.CreateUser(user.Username, user.Usersurname, user.UserCellPhone);
                    Session["response"] = new FlashMessage { status = "alert alert-success", message = "User successfully created!" };
                    return RedirectToAction("Index");
                }
                return View(user);

            }
            catch
            {
                return View(user);
            }
        }

        // GET: User/Edit/5
        [Route("user/edit/{id}", Name = "_editUser")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = dal.find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("create", user);
        }

        // POST: User/Edit/5
        [Route("user/edit/{id}", Name = "_editPostUser")]
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    dal.EditUser(id, user.Username, user.Usersurname, user.UserCellPhone);
                    Session["response"] = new FlashMessage { status = "alert alert-success", message = "User successfully updated!" };
                    return RedirectToAction("Index");
                }
                return View("create", user);
            }
            catch
            {
                return View("create", user);
            }
        }

        // GET: User/Delete/5
        [Route("user/delete/{id}", Name = "_deleteUser")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = dal.find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [Route("user/delete/{id}", Name = "_deletePostUser")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                dal.DeleteUser(id);
                Session["response"] = new FlashMessage { status = "alert alert-success", message = "User successfully deleted!" };
                return RedirectToAction("Index");
            }
            catch
            {
                Session["response"] = new FlashMessage { status = "alert alert-danger", message = "Unable to delete the user!" };
                return RedirectToAction("Index");
            }
        }
    }
}
