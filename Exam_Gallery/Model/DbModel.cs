using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Gallery.Model
{
    public class DbModel
    {
        public List<USER> getUsers()
        {
            using (ImageGalleryEntities2 db = new ImageGalleryEntities2())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var res = db.USER.Select(t => t).ToList();
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }           
        }
        public List<PASSWORD> GetPasswords()
        {
            using (ImageGalleryEntities2 db = new ImageGalleryEntities2())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var res = db.PASSWORD.Select(t => t).ToList();
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void InsertNewUser(string name, string surname, string login, string password)
        {
            using (ImageGalleryEntities2 db = new ImageGalleryEntities2())
            {
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.insert_new_password(login, password);
                        int insertedIdx = GetPasswords().Where(t => (t.Login == login && t.Password1 == password)).Select(t => t.Id).FirstOrDefault();
                        db.insert_new_user(name, surname, insertedIdx);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool IsUserValid(string login, string password)
        {
            foreach (PASSWORD i in GetPasswords())
                if ((i.Login == login && i.Password1 == password) == true)
                    return true;
            return false;
        }
    }
}