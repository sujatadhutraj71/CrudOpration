using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using CrudOpration.Models;

namespace CrudOpration.Controllers
{
    public class HomeController : Controller
    {
        public readonly string cs;
        public HomeController()
        {
            var conStr = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cs = conStr.GetConnectionString("dbcs");
        }

        public IActionResult displayAll()
        {
            List<User> list = new List<User>();
            string query = @"select * from Users";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User user = new User
                {
                    Id = reader.GetInt32(0),
                    LoginUserId = reader.GetString(1),
                    UserName = reader.GetString(2),
                    Address = reader.GetString(3),
                    ContactNumber = reader.GetString(4),
                    MobileNumber = reader.GetString(5),
                    EmailId = reader.GetString(6),
                    OTPVerify = reader.GetBoolean(7),
                    Tab = reader.GetBoolean(8),
                    UserLayer = reader.GetString(9),
                    Remark = reader.GetString(10),
                    IsActive = reader.GetBoolean(11)
                };
                list.Add(user);
            }

            return View(list);
        }
        public IActionResult addUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addUser(User user)
        {
            string query = @"
            INSERT INTO Users (LoginUserId, UserName, Address, ContactNumber, MobileNumber, EmailId, OTPVerify, Tab, UserLayer, Remark, IsActive)
            VALUES (@LoginUserId, @UserName, @Address, @ContactNumber, @MobileNumber, @EmailId, @OTPVerify, @Tab, @UserLayer, @Remark, @IsActive)";

            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection connection = new SqlConnection(cs))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@LoginUserId", user.LoginUserId);
                            command.Parameters.AddWithValue("@UserName", user.UserName);
                            command.Parameters.AddWithValue("@Address", user.Address);
                            command.Parameters.AddWithValue("@ContactNumber",       user.ContactNumber);
                            command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                            command.Parameters.AddWithValue("@EmailId", user.EmailId);
                            command.Parameters.AddWithValue("@OTPVerify", user.OTPVerify);
                            command.Parameters.AddWithValue("@Tab", user.Tab);
                            command.Parameters.AddWithValue("@UserLayer", user.UserLayer);
                            command.Parameters.AddWithValue("@Remark", user.Remark);
                            command.Parameters.AddWithValue("@IsActive", user.IsActive);

                            connection.Open();
                            int res = command.ExecuteNonQuery();
                            if (res > 0)
                            {
                                return RedirectToAction("displayAll");
                            }
                            return View(user);
                        }
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return View(user);
            }
        }
        public IActionResult Edit(int id)
        {
            string query = @"select * from Users where Id=@Id";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                User user = new User
                {
                    Id = reader.GetInt32(0),
                    LoginUserId = reader.GetString(1),
                    UserName = reader.GetString(2),
                    Address = reader.GetString(3),
                    ContactNumber = reader.GetString(4),
                    MobileNumber = reader.GetString(5),
                    EmailId = reader.GetString(6),
                    OTPVerify = reader.GetBoolean(7),
                    Tab = reader.GetBoolean(8),
                    UserLayer = reader.GetString(9),
                    Remark = reader.GetString(10),
                    IsActive = reader.GetBoolean(11)
                };
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string query = @"UPDATE Users SET 
                            LoginUserId = @LoginUserId,
                            UserName = @UserName,
                            Address = @Address,
                            ContactNumber = @ContactNumber,
                            MobileNumber = @MobileNumber,
                            EmailId = @EmailId,
                            OTPVerify = @OTPVerify,
                            Tab = @Tab,
                            UserLayer = @UserLayer,
                            Remark = @Remark,
                            IsActive = @IsActive
                            WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@LoginUserId", user.LoginUserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@OTPVerify", user.OTPVerify);
                cmd.Parameters.AddWithValue("@Tab", user.Tab);
                cmd.Parameters.AddWithValue("@UserLayer", user.UserLayer);
                cmd.Parameters.AddWithValue("@Remark", user.Remark);
                cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                cmd.Parameters.AddWithValue("@Id", user.Id);

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    return RedirectToAction("displayAll");
                }
                else
                {
                    return View(user);
                }
            }

            return View(user);
        }
        public IActionResult Delete(int id)
        {
            string query = @"delete from Users where Id=@Id";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.Parameters.AddWithValue("@Id", id);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                return RedirectToAction("displayAll");
            }
            return RedirectToAction("displayAll");
        }
    }
}
