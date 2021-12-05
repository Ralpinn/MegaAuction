using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;

namespace MegaAuctions.Models
{
    public class BlogModel
    {
        //DBEntitiesAuction db = new DBEntitiesAuction();
        //public IEnumerable<Blog> ListBlog()
        //{
        //    return db.Blogs.ToList();
        //}
        //public Blog OneBlog(int id)
        //{
        //    return db.Blogs.First(m => m.idblog == id);
        //}
        //public ImageBlog BlogImg(int id)
        //{
        //    return db.ImageBlogs.First(m => m.idimgblog == id);
        //}
        //public IEnumerable<TypeBlog> ListTypeBlog()
        //{
        //    return db.TypeBlogs.ToList();
        //}

        //public void add_blog(Blog blog, /*ImageBlog imageBlog,*/ TypeBlog typeblog)
        //{
        //    Blog p = new Blog();
        //    p.titleblog = blog.titleblog; //titleblog => đúng ***
        //    p.contentblog = blog.contentblog; //???? bên kia đâu có name contentblog đâu ?
        //    p.dateblog = blog.dateblog; //???? bên kia đâu có name dateblog đâu ?
        //    p.imageb1 = blog.imageb1;
        //    p.imageb2 = blog.imageb2;
        //    p.imageb3 = blog.imageb3;
        //    string[] idtype = typeblog.nametb.Split('.');
        //    p.idtypeblog = int.Parse(idtype[0]);
        //    p.idUser = Content.userinformation.idUser;
        //    db.Blogs.Add(p);
        //    db.SaveChanges();



        //    ImageBlog img = new ImageBlog();
        //    img.image1 = MegaAuctions.Models.Content.ran + BlogImg.image1;
        //    img.image2 = MegaAuctions.Models.Content.ran + imgpro.image2;
        //    img.image3 = MegaAuctions.Models.Content.ran + imgpro.image3;
        //    db.ImageProducts.Add(img);
        //    db.SaveChanges(); //1 id img dc tao

        //} //m tham khảo lại cái phần add product ák, t dựa theo product á
        //tên là truyền bên kia qua mà,, giống chỗ ***
        //tạo thêm mấy casiinput nữa, cho contentblog, dateblog
        //còn cái nametb thì hiển thị list typeblog lên cho người thêm chọn

        //OK chưa?, Ok để t làm có gì t nhắn, à mà còn phần deatilpost sao actionlink qua blog ko được

        //public void delete_blog(int id)
        //{
        //    Blog blog = OneBlog(id);
        //    ImageBlog imageBlog = BlogImg(blog.I.idimgblog);
        //    db.Blogs.Remove(blog);
        //    db.ImageBlogs.Remove(BlogImg);
        //    db.SaveChanges();
        //}
        DBEntitiesAuction db = new DBEntitiesAuction();
        public IEnumerable<Blog> ListBlog()
        {
            return db.Blogs.ToList();
        }
        public Blog OneBlog(int id)
        {
            return db.Blogs.First(m => m.idblog == id);
        }
        //public ImageBlog OneImgBlog(int id)
        //{
        //    return db.ImageBlogs.First(m => m.idimgblog == id);
        //}
        public IEnumerable<TypeBlog> ListTypeBlog()
        {
            return db.TypeBlogs.ToList();
        }

        public void add_blog(Blog blog, /*ImageBlog imgblog,*/ TypeBlog typeBlog)
        {

            //ImageBlog img = new ImageBlog();
            //img.imageb1 = MegaAuctions.Models.Content.ran + imgblog.imageb1;
            //img.imageb2 = MegaAuctions.Models.Content.ran + imgblog.imageb2;
            //img.imageb3 = MegaAuctions.Models.Content.ran + imgblog.imageb3;
            //db.ImageBlog.Add(img);
            //db.SaveChanges(); //1 id img dc tao

            Blog b = new Blog();
            b.titleblog = blog.titleblog;
            b.contentblog = blog.contentblog;
            b.dateblog = blog.dateblog;
            b.imageb1 = blog.imageb1;
            b.imageb2 = blog.imageb2;
            b.imageb3 = blog.imageb3;

            string[] idtype = typeBlog.nametb.Split('.');
            b.idtypeblog = int.Parse(idtype[0]);
            //b.idimgblog = img.idimgblog;
            b.idUser = Content.userinformation.idUser;
            db.Blogs.Add(b);
            db.SaveChanges();
        }
        public void delete_blog(int id)
        {
            Blog blog = OneBlog(id);
            //ImageBlog imgblog = OneImgBlog(blog.ImageBlog.idimgblog);
            db.Blogs.Remove(blog);
            //db.ImageBlogs.Remove(imgblog);
            db.SaveChanges();
        }
    }
}