using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NRBlogAPI.Controllers
{
    public class BlogsRestController : ApiController
    {
        private DbModel db = new DbModel();


        // GET api/blog
        public IEnumerable<Blog> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = from b in db.Blogs
                        orderby b.Name
                        select b;

            return query;
        }

        // GET api/blog/5
        public Blog Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            //return db.Blogs.Find(id);
            //return db.Blogs.FirstOrDefault(i => i.BlogId == id);

            var query = from b in db.Blogs
                        where b.BlogId == id
                        select b;

            return query.FirstOrDefault();
        }

        // POST api/blog
        public void Post([FromBody] Blog blog)
        {
            db.Blogs.Add(blog);
            db.SaveChanges();
        }

        // PUT api/blog/5
        public void Put(int id, [FromBody] Blog blog)
        {
            var b = db.Blogs.Where(s => s.BlogId == id).First();

            b.Name = blog.Name;
            b.Url = blog.Url;
            b.Posts = blog.Posts;
           
            db.SaveChanges();
        }

        // DELETE api/blog/5
        public void Delete(int id)
        {
            var query = from b in db.Blogs
                        where b.BlogId == id
                        select b;

            var blog = query.FirstOrDefault();
            db.Blogs.Remove(blog);
            db.SaveChanges();
        }
    }
}
