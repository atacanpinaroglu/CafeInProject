using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CafeInContext>, IUserDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=CafeIn;integrated security=true");

        private void ConnectionControl()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public User GetByEmail(string email)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("SELECT * FROM Users", _connection);
            SqlDataReader reader = command.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                User u = new User
                {
                    UserId = Convert.ToInt32(reader["UserId"]),
                    Email = Convert.ToString(reader["Email"]),
                    Password = Convert.ToString(reader["Password"])
                };       
                users.Add(u);
            }

            reader.Close();
            _connection.Close();

            var user = users.SingleOrDefault(u => u.Email == email);
            return user;
        }
    }
}
