using Microsoft.Data.SqlClient;
using UmLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace UmLogic.DAL
{
    public class Users_DAL
    {
        private readonly IConfiguration _configuration;

        public Users_DAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Users> GetAllUsers()
        {
            List<Users> usersList = new List<Users>();
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[GETUSERS]";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    Users user = new Users();
                    user.Id = Convert.ToInt32(dr["Id"]);
                    user.Ques = dr["Ques"].ToString();
                    user.Ans = dr["Ans"].ToString();
                    usersList.Add(user);
                }
                _connection.Close();
            }
            return usersList;
        }

        public List<StudQuestion> GetAllStudQues()
        {
            List<StudQuestion> studQuesList = new List<StudQuestion>();
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[GETSTUDQUESTIONS]";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    StudQuestion studQues = new StudQuestion();
                    studQues.Id = Convert.ToInt32(dr["Id"]);
                    studQues.Name = dr["Name"].ToString();
                    studQues.Email = dr["Email"].ToString();
                    studQues.StudQues = dr["StudQues"].ToString();

                    studQuesList.Add(studQues);
                }
                _connection.Close();
            }
            return studQuesList;
        }

        public bool CreateUser(Users user)
        {
            int val = 0;
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[CREATEUSER]";
                _command.Parameters.AddWithValue("@Ques", user.Ques);
                _command.Parameters.AddWithValue("@Ans", user.Ans);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0;
        }

        public bool CreateStudQues(StudQuestion studques)
        {
            int val = 0;
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[CREATESTUDQUES]";
                _command.Parameters.AddWithValue("@Name", studques.Name);
                _command.Parameters.AddWithValue("@Email", studques.Email);
                _command.Parameters.AddWithValue("@StudQues", studques.StudQues);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0;
        }

        public Users GetById(int Id)
        {
            Users user = new Users();
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[GETBYID]";
                _command.Parameters.AddWithValue("@ID", Id);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    user.Id = Convert.ToInt32(dr["Id"]);
                    user.Ques = dr["Ques"].ToString();
                    user.Ans = dr["Ans"].ToString();
                }
                _connection.Close();
            }
            return user;
        }

        public bool UpdateUser(Users user)
        {
            int val = 0;
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[UPDATEUSER]";
                _command.Parameters.AddWithValue("@Id", user.Id);
                _command.Parameters.AddWithValue("@Ques", user.Ques);
                _command.Parameters.AddWithValue("@Ans", user.Ans);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0;
        }

        public bool DeleteUser(int Id)
        {
            int val = 0;
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[DELETEUSER]";
                _command.Parameters.AddWithValue("@Id", Id);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0;
        }

        public bool DeleteStudQues(int Id)
        {
            int val = 0;
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                using SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[DELETESTUDQUES]";
                _command.Parameters.AddWithValue("@Id", Id);
                _connection.Open();
                val = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return val > 0;
        }
    }
}
