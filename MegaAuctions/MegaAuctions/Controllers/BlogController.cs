using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using MegaAuctions.Models;

namespace MegaAuctions.Controllers
{
    public class BlogController : Controller
    {
        //Manage Blogs
        User userseller = MegaAuctions.Models.Content.userinformation;
        BlogModel blogModel = new BlogModel();
        public ActionResult Index()
        {
            ViewBag.ListBlog = blogModel.ListBlog();
            return View(userseller);
        }
        public ActionResult Create()
        {
            ViewBag.TypeBlog = blogModel.ListTypeBlog(); // rồi nè !!!, truyền qua b
            return View(userseller);
        }
        [HttpPost]
        public ActionResult Create(Blog blog, /*ImageBlog imageBlog,*/ TypeBlog typeblog)
        {
            //try
            //{
            //    MegaAuctions.Models.Content.ran = MegaAuctions.Models.Content.RandomString(5);
            //    blogmodel.add_blog(blog, typeblog);
            //    MegaAuctions.Models.Content.ran = "";
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    blogmodel.add_blog(blog, typeblog);
            //    MegaAuctions.Models.Content.ran = "";
            //    return RedirectToAction("Index");
            //}
            try
            {
                MegaAuctions.Models.Content.ran = MegaAuctions.Models.Content.RandomString(5);
                //if (blog.upfiles.Length > 0)  //nhớ lưu hình nữa, t thấy m hình như chưa lưu hình
                //{   //iterating through multiple file collection   
                //    for (int i = 0; i < blog.upfiles.Length; i++)
                //    {
                //        string file = Path.GetFileNameWithoutExtension(blog.upfiles[i].FileName);
                //        string exten = Path.GetExtension(blog.upfiles[i].FileName);
                //        file = MegaAuctions.Models.Content.ran + file + exten;
                //        blog.upfiles[i].SaveAs(Path.Combine(Server.MapPath("~/img/blogs/BlogOfUser/"), file));
                //    }
                //}
                blogModel.add_blog(blog, /*imgblog,*/ typeblog);
                MegaAuctions.Models.Content.ran = "";
                return RedirectToAction("Index");
            }
            catch
            {
                blogModel.add_blog(blog, /*imgblog,*/ typeblog);
                MegaAuctions.Models.Content.ran = "";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                blogModel.delete_blog(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            ////Return the Post to the View
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Blog blog, string UserID)
        //{
        //    //Get the First File Uploaded
        //    HttpPostedFileBase file = Request.Files[0];

        //    // If the Post passed to the Edit is not Null
        //    if (ModelState.IsValid && file != null && UserID != null)
        //    {
        //        // For File Upload
        //        var fileName = Path.GetFileName(file.FileName);
        //        var path = Path.Combine(Server.MapPath("/img"), fileName);
        //        string newImg = "/img/" + fileName;

        //        //Save the File
        //        file.SaveAs(path);

        //        //Redirect to Index
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public ActionResult DetailPost()
        {
            return View();
        }
    }
}