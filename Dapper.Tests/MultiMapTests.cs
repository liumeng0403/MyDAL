using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace Dapper.Tests
{
    public class MultiMapTests : TestBase
    {
        private class Parent
        {
            public int Id { get; set; }
            public readonly List<Child> Children = new List<Child>();
        }

        private class Child
        {
            public int Id { get; set; }
        }
        
        private class Multi1
        {
            public int Id { get; set; }
        }

        private class Multi2
        {
            public int Id { get; set; }
        }
        
        private class UserWithConstructor
        {
            public UserWithConstructor(int id, string name)
            {
                Ident = id;
                FullName = name;
            }

            public int Ident { get; set; }
            public string FullName { get; set; }
        }

        private class PostWithConstructor
        {
            public PostWithConstructor(int id, int ownerid, string content)
            {
                Ident = id;
                FullContent = content;
            }

            public int Ident { get; set; }
            public UserWithConstructor Owner { get; set; }
            public string FullContent { get; set; }
            public Comment Comment { get; set; }
        }

        //[Fact]
        //public void TestMultiMapWithConstructor()
        //{
        //    const string createSql = @"
        //        create table #Users (Id int, Name varchar(20))
        //        create table #Posts (Id int, OwnerId int, Content varchar(20))

        //        insert #Users values(99, 'Sam')
        //        insert #Users values(2, 'I am')

        //        insert #Posts values(1, 99, 'Sams Post1')
        //        insert #Posts values(2, 99, 'Sams Post2')
        //        insert #Posts values(3, null, 'no ones post')";
        //    connection.Execute(createSql);
        //    try
        //    {
        //        const string sql = @"select * from #Posts p 
        //                   left join #Users u on u.Id = p.OwnerId 
        //                   Order by p.Id";
        //        PostWithConstructor[] data = connection.Query<PostWithConstructor, UserWithConstructor, PostWithConstructor>(sql, (post, user) => { post.Owner = user; return post; }).ToArray();
        //        var p = data.First();

        //        Assert.Equal("Sams Post1", p.FullContent);
        //        Assert.Equal(1, p.Ident);
        //        Assert.Equal("Sam", p.Owner.FullName);
        //        Assert.Equal(99, p.Owner.Ident);

        //        Assert.Null(data[2].Owner);
        //    }
        //    finally
        //    {
        //        connection.Execute("drop table #Users drop table #Posts");
        //    }
        //}

        [Fact]
        public void TestMultiMapArbitraryMaps()
        {
            // please excuse the trite example, but it is easier to follow than a more real-world one
            const string createSql = @"
                create table #ReviewBoards (Id int, Name varchar(20), User1Id int, User2Id int, User3Id int, User4Id int, User5Id int, User6Id int, User7Id int, User8Id int, User9Id int)
                create table #Users (Id int, Name varchar(20))

                insert #Users values(1, 'User 1')
                insert #Users values(2, 'User 2')
                insert #Users values(3, 'User 3')
                insert #Users values(4, 'User 4')
                insert #Users values(5, 'User 5')
                insert #Users values(6, 'User 6')
                insert #Users values(7, 'User 7')
                insert #Users values(8, 'User 8')
                insert #Users values(9, 'User 9')

                insert #ReviewBoards values(1, 'Review Board 1', 1, 2, 3, 4, 5, 6, 7, 8, 9)
";
            connection.Execute(createSql);
            try
            {
                const string sql = @"
                    select 
                        rb.Id, rb.Name,
                        u1.*, u2.*, u3.*, u4.*, u5.*, u6.*, u7.*, u8.*, u9.*
                    from #ReviewBoards rb
                        inner join #Users u1 on u1.Id = rb.User1Id
                        inner join #Users u2 on u2.Id = rb.User2Id
                        inner join #Users u3 on u3.Id = rb.User3Id
                        inner join #Users u4 on u4.Id = rb.User4Id
                        inner join #Users u5 on u5.Id = rb.User5Id
                        inner join #Users u6 on u6.Id = rb.User6Id
                        inner join #Users u7 on u7.Id = rb.User7Id
                        inner join #Users u8 on u8.Id = rb.User8Id
                        inner join #Users u9 on u9.Id = rb.User9Id
";

                var types = new[] { typeof(ReviewBoard), typeof(User), typeof(User), typeof(User), typeof(User), typeof(User), typeof(User), typeof(User), typeof(User), typeof(User) };

                Func<object[], ReviewBoard> mapper = (objects) =>
                {
                    var board = (ReviewBoard)objects[0];
                    board.User1 = (User)objects[1];
                    board.User2 = (User)objects[2];
                    board.User3 = (User)objects[3];
                    board.User4 = (User)objects[4];
                    board.User5 = (User)objects[5];
                    board.User6 = (User)objects[6];
                    board.User7 = (User)objects[7];
                    board.User8 = (User)objects[8];
                    board.User9 = (User)objects[9];
                    return board;
                };

                var data = connection.Query<ReviewBoard>(sql, types, mapper).ToList();

                var p = data[0];
                Assert.Equal(1, p.Id);
                Assert.Equal("Review Board 1", p.Name);
                Assert.Equal(1, p.User1.Id);
                Assert.Equal(2, p.User2.Id);
                Assert.Equal(3, p.User3.Id);
                Assert.Equal(4, p.User4.Id);
                Assert.Equal(5, p.User5.Id);
                Assert.Equal(6, p.User6.Id);
                Assert.Equal(7, p.User7.Id);
                Assert.Equal(8, p.User8.Id);
                Assert.Equal(9, p.User9.Id);
                Assert.Equal("User 1", p.User1.Name);
                Assert.Equal("User 2", p.User2.Name);
                Assert.Equal("User 3", p.User3.Name);
                Assert.Equal("User 4", p.User4.Name);
                Assert.Equal("User 5", p.User5.Name);
                Assert.Equal("User 6", p.User6.Name);
                Assert.Equal("User 7", p.User7.Name);
                Assert.Equal("User 8", p.User8.Name);
                Assert.Equal("User 9", p.User9.Name);
            }
            finally
            {
                connection.Execute("drop table #Users drop table #ReviewBoards");
            }
        }

//        [Fact]
//        public void TestMultiMapGridReader()
//        {
//            const string createSql = @"
//                create table #Users (Id int, Name varchar(20))
//                create table #Posts (Id int, OwnerId int, Content varchar(20))

//                insert #Users values(99, 'Sam')
//                insert #Users values(2, 'I am')

//                insert #Posts values(1, 99, 'Sams Post1')
//                insert #Posts values(2, 99, 'Sams Post2')
//                insert #Posts values(3, null, 'no ones post')
//";
//            connection.Execute(createSql);

//            const string sql =
//@"select p.*, u.Id, u.Name + '0' Name from #Posts p 
//left join #Users u on u.Id = p.OwnerId 
//Order by p.Id

//select p.*, u.Id, u.Name + '1' Name from #Posts p 
//left join #Users u on u.Id = p.OwnerId 
//Order by p.Id
//";

//            var grid = connection.QueryMultiple(sql);

//            for (int i = 0; i < 2; i++)
//            {
//                var data = grid.Read<Post, User, Post>((post, user) => { post.Owner = user; return post; }).ToList();
//                var p = data[0];

//                Assert.Equal("Sams Post1", p.Content);
//                Assert.Equal(1, p.Id);
//                Assert.Equal(p.Owner.Name, "Sam" + i);
//                Assert.Equal(99, p.Owner.Id);

//                Assert.Null(data[2].Owner);
//            }

//            connection.Execute("drop table #Users drop table #Posts");
//        }

//        [Fact]
//        public void TestFlexibleMultiMapping()
//        {
//            const string sql =
//@"select 
//    1 as PersonId, 'bob' as Name, 
//    2 as AddressId, 'abc street' as Name, 1 as PersonId,
//    3 as Id, 'fred' as Name
//    ";
//            var personWithAddress = connection.Query<Person, Address, Extra, Tuple<Person, Address, Extra>>
//                (sql, Tuple.Create, splitOn: "AddressId,Id").First();

//            Assert.Equal(1, personWithAddress.Item1.PersonId);
//            Assert.Equal("bob", personWithAddress.Item1.Name);
//            Assert.Equal(2, personWithAddress.Item2.AddressId);
//            Assert.Equal("abc street", personWithAddress.Item2.Name);
//            Assert.Equal(1, personWithAddress.Item2.PersonId);
//            Assert.Equal(3, personWithAddress.Item3.Id);
//            Assert.Equal("fred", personWithAddress.Item3.Name);
//        }

        //[Fact]
        //public void TestMultiMappingWithSplitOnSpaceBetweenCommas()
        //{
        //    const string sql = @"select 
        //                1 as PersonId, 'bob' as Name, 
        //                2 as AddressId, 'abc street' as Name, 1 as PersonId,
        //                3 as Id, 'fred' as Name
        //                ";
        //    var personWithAddress = connection.Query<Person, Address, Extra, Tuple<Person, Address, Extra>>
        //        (sql, Tuple.Create, splitOn: "AddressId, Id").First();

        //    Assert.Equal(1, personWithAddress.Item1.PersonId);
        //    Assert.Equal("bob", personWithAddress.Item1.Name);
        //    Assert.Equal(2, personWithAddress.Item2.AddressId);
        //    Assert.Equal("abc street", personWithAddress.Item2.Name);
        //    Assert.Equal(1, personWithAddress.Item2.PersonId);
        //    Assert.Equal(3, personWithAddress.Item3.Id);
        //    Assert.Equal("fred", personWithAddress.Item3.Name);
        //}

        private class Extra
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        //[Fact]
        //public void TestMultiMappingWithNonReturnedProperty()
        //{
        //    const string sql = @"select 
        //                    1 as PostId, 'Title' as Title,
        //                    2 as BlogId, 'Blog' as Title";
        //    var postWithBlog = connection.Query<Post_DupeProp, Blog_DupeProp, Post_DupeProp>(sql,
        //        (p, b) =>
        //        {
        //            p.Blog = b;
        //            return p;
        //        }, splitOn: "BlogId").First();

        //    Assert.Equal(1, postWithBlog.PostId);
        //    Assert.Equal("Title", postWithBlog.Title);
        //    Assert.Equal(2, postWithBlog.Blog.BlogId);
        //    Assert.Equal("Blog", postWithBlog.Blog.Title);
        //}

        private class Post_DupeProp
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public int BlogId { get; set; }
            public Blog_DupeProp Blog { get; set; }
        }

        private class Blog_DupeProp
        {
            public int BlogId { get; set; }
            public string Title { get; set; }
        }

        //// see https://stackoverflow.com/questions/16955357/issue-about-dapper
        //[Fact]
        //public void TestSplitWithMissingMembers()
        //{
        //    var result = connection.Query<Topic, Profile, Topic>(
        //    @"select 123 as ID, 'abc' as Title,
        //             cast('01 Feb 2013' as datetime) as CreateDate,
        //             'ghi' as Name, 'def' as Phone",
        //    (T, P) => { T.Author = P; return T; },
        //    null, null, true, "ID,Name").Single();

        //    Assert.Equal(123, result.ID);
        //    Assert.Equal("abc", result.Title);
        //    Assert.Equal(new DateTime(2013, 2, 1), result.CreateDate);
        //    Assert.Null(result.Name);
        //    Assert.Null(result.Content);

        //    Assert.Equal("def", result.Author.Phone);
        //    Assert.Equal("ghi", result.Author.Name);
        //    Assert.Equal(0, result.Author.ID);
        //    Assert.Null(result.Author.Address);
        //}

        public class Profile
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            //public ExtraInfo Extra { get; set; }
        }

        public class Topic
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public DateTime CreateDate { get; set; }
            public string Content { get; set; }
            public int UID { get; set; }
            public int TestColum { get; set; }
            public string Name { get; set; }
            public Profile Author { get; set; }
            //public Attachment Attach { get; set; }
        }

        //[Fact]
        //public void TestInvalidSplitCausesNiceError()
        //{
        //    try
        //    {
        //        connection.Query<User, User, User>("select 1 A, 2 B, 3 C", (x, y) => x);
        //    }
        //    catch (ArgumentException)
        //    {
        //        // expecting an app exception due to multi mapping being bodged 
        //    }

        //    try
        //    {
        //        connection.Query<dynamic, dynamic, dynamic>("select 1 A, 2 B, 3 C", (x, y) => x);
        //    }
        //    catch (ArgumentException)
        //    {
        //        // expecting an app exception due to multi mapping being bodged 
        //    }
        //}
    }
}
