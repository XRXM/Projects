using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using System.Net.Mail;

namespace Capstone.DAO
{
    public class HomeSqlDAO : IHomeDAO
    {
        private readonly string connectionString;
        public HomeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;    
        }
        public Home GetHome(int userId, int homeId)
        {
            Home home = new Home();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from home h join users u on u.user_id = h.user_id where h.user_id = @user_id and h.home_id = @home_id ", conn);
                    //SqlCommand cmd = new SqlCommand("select * from home where home_id = @home_id ", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            home.HomeId = Convert.ToInt32(reader["home_id"]);
                            home.UserId = Convert.ToInt32(reader["user_id"]);
                            home.Description = Convert.ToString(reader["description"]);
                            home.TypeOfHome = Convert.ToString(reader["type"]);
                            home.Nickname = Convert.ToString(reader["nickname"]);
                            home.StreetAddress = Convert.ToString(reader["street_address"]);
                            home.City = Convert.ToString(reader["city"]);
                            home.State = Convert.ToString(reader["state"]);
                            home.Zip = Convert.ToString(reader["zip"]);
                            home.PicUrl = Convert.ToString(reader["pic_url"]);


                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return home;


        }
        public bool DeleteHome(int userId, int homeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"Delete vendor where home_id = @home_id; 
                        Delete store where home_id = @home_id;
                        DELETE home WHERE user_id = @user_id AND home_id = @home_id ", conn);

                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;

                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
        public List<Home> GetHomes(int userId)
        {
            List<Home> listHomes = new List<Home>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from home where user_id = @user_id ", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Home home = new Home();

                            home.HomeId = Convert.ToInt32(reader["home_id"]);
                            home.UserId = Convert.ToInt32(reader["user_id"]);
                            home.Description = Convert.ToString(reader["description"]);
                            home.TypeOfHome = Convert.ToString(reader["type"]);
                            home.Nickname = Convert.ToString(reader["nickname"]);
                            home.StreetAddress = Convert.ToString(reader["street_address"]);
                            home.City = Convert.ToString(reader["city"]);
                            home.State = Convert.ToString(reader["state"]);
                            home.Zip = Convert.ToString(reader["zip"]);
                            home.PicUrl = Convert.ToString(reader["pic_url"]);

                            listHomes.Add(home);

                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return listHomes;


        }
        public int AddHome(Home home)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO home(user_id, description, type, nickname, street_address, city, state, zip, pic_url) 
                    VALUES(@user_id, @description, @type, @nickname, @street_address, @city, @state, @zip, @pic_url); SELECT @@IDENTITY", conn);

                    cmd.Parameters.AddWithValue("@user_id", home.UserId);
                    cmd.Parameters.AddWithValue("@description", home.Description);
                    cmd.Parameters.AddWithValue("@type", home.TypeOfHome);
                    cmd.Parameters.AddWithValue("@nickname", home.Nickname);
                    cmd.Parameters.AddWithValue("@street_address", home.StreetAddress);
                    cmd.Parameters.AddWithValue("@city", home.City);
                    cmd.Parameters.AddWithValue("@state", home.State);
                    cmd.Parameters.AddWithValue("@zip", home.Zip);
                    cmd.Parameters.AddWithValue("@pic_url", home.PicUrl);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    return result;

                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
        public bool UpdateHome(Home home)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE home SET description = @description, type = @type, nickname = @nickname, street_address = @street_address, city = @city, state = @state, zip = @zip where home_id = @home_id", conn);

                    cmd.Parameters.AddWithValue("@home_id", home.HomeId);
                    cmd.Parameters.AddWithValue("@description", home.Description);
                    cmd.Parameters.AddWithValue("@type", home.TypeOfHome);
                    cmd.Parameters.AddWithValue("@nickname", home.Nickname);
                    cmd.Parameters.AddWithValue("@street_address", home.StreetAddress);
                    cmd.Parameters.AddWithValue("@city", home.City);
                    cmd.Parameters.AddWithValue("@state", home.State);
                    cmd.Parameters.AddWithValue("@zip", home.Zip);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    return result > 0;

                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
        public Milestones GetMilestones(int homeId)
        {

            Milestones milestone = new Milestones();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from milestones where home_id = @home_id ", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            milestone.HomeId = Convert.ToInt32(reader["home_id"]);
                            milestone.BuildYear = Convert.ToInt32(reader["build_year"]);
                            milestone.PurchaseYear = Convert.ToInt32(reader["purchase_date"]);


                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return milestone;

        }
        public bool UpdateMilestones(int homeId, int buildYear, int purchaseYear)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"UPDATE milestones SET build_year = @build_year, purchase_date = @purchase_date WHERE home_id = @home_id", conn);

                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@build_year", buildYear);
                    cmd.Parameters.AddWithValue("@purchase_date", purchaseYear);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    return result > 0;

                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
        public bool AddMileStones(int homeId, int year, int purchaseDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO milestones (home_id, build_year, purchase_date) VALUES(@home_id, @build_year, @purchase_date)", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@build_year", year);
                    cmd.Parameters.AddWithValue("@purchase_date", purchaseDate);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }
        public bool DeleteMilestones (int homeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"DELETE milestones WHERE home_id = @home_id", conn);

                    cmd.Parameters.AddWithValue("@home_id", homeId);                   
                    int result = cmd.ExecuteNonQuery();

                    return result > 0;

                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
    }
}
