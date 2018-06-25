using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Exam_Gallery;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_DbModel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_AddPassword()
        {
            bool pasted = false;
            using (ImageGalleryEntities2 db = new ImageGalleryEntities2())
            {
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        string login = GetRandomString(20);
                        string password = GetRandomString(16);
                        db.insert_new_password(login, password);
                        List<PASSWORD> res = db.PASSWORD.Select(t => t).ToList();
                        foreach (PASSWORD i in res)
                        {
                            if(i.Login == login && i.Password1==password)
                            {
                                pasted = true;
                                break;
                            }
                        }

                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            Assert.AreEqual(true, pasted);
        }


        private string GetRandomString(int size)
        {
            StringBuilder sb = new StringBuilder(size);
            for (int i = 0; i < size; i++)
                sb.Append(GetRandomChar());
            return sb.ToString();
        }
        private char GetRandomChar() => (char)GetRandom().Next(48, 123);
        private Random GetRandom() => new Random(Guid.NewGuid().GetHashCode());
    }
}
