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
            using (ImageGalleryEntities1 db = new ImageGalleryEntities1())
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
            using (ImageGalleryEntities1 db = new ImageGalleryEntities1())
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


        public bool IsUserValid(string login, string password)
        {
            foreach (PASSWORD i in GetPasswords())
                if ((i.Login == login && i.Password1 == password) == true)
                    return true;
            return false;
        }
    }
}