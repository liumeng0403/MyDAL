using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;
using EasyDAL.Exchange.AdoNet;

namespace EasyDAL.Exchange.Tests
{
    public class MultiMapTests : TestBase
    {
        //private class Parent
        //{
        //    public int Id { get; set; }
        //    public readonly List<Child> Children = new List<Child>();
        //}

        private class Child
        {
            public int Id { get; set; }
        }
        
        //private class Multi1
        //{
        //    public int Id { get; set; }
        //}

        //private class Multi2
        //{
        //    public int Id { get; set; }
        //}
        
        //private class UserWithConstructor
        //{
        //    public UserWithConstructor(int id, string name)
        //    {
        //        Ident = id;
        //        FullName = name;
        //    }

        //    public int Ident { get; set; }
        //    public string FullName { get; set; }
        //}

        //private class PostWithConstructor
        //{
        //    public PostWithConstructor(int id, int ownerid, string content)
        //    {
        //        Ident = id;
        //        FullContent = content;
        //    }

        //    public int Ident { get; set; }
        //    public UserWithConstructor Owner { get; set; }
        //    public string FullContent { get; set; }
        //    public Comment Comment { get; set; }
        //}



        //private class Extra
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}



        //private class Post_DupeProp
        //{
        //    public int PostId { get; set; }
        //    public string Title { get; set; }
        //    public int BlogId { get; set; }
        //    public Blog_DupeProp Blog { get; set; }
        //}

        //private class Blog_DupeProp
        //{
        //    public int BlogId { get; set; }
        //    public string Title { get; set; }
        //}



        //public class Profile
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }
        //    public string Phone { get; set; }
        //    public string Address { get; set; }
        //    //public ExtraInfo Extra { get; set; }
        //}

        //public class Topic
        //{
        //    public int ID { get; set; }
        //    public string Title { get; set; }
        //    public DateTime CreateDate { get; set; }
        //    public string Content { get; set; }
        //    public int UID { get; set; }
        //    public int TestColum { get; set; }
        //    public string Name { get; set; }
        //    public Profile Author { get; set; }
        //    //public Attachment Attach { get; set; }
        //}


    }
}
