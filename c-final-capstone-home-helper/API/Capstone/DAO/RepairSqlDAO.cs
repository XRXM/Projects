using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class RepairSqlDAO : IRepairDAO
    {
        private readonly string connectionString;
        public RepairSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Repair> GetRepairs(int userId, int homeId)
        {
            List<Repair> repairs = new List<Repair>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from repairs where user_id = @user_id and home_id = @home_id ", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Repair repair = new Repair();
                            repair.UserId = Convert.ToInt32(reader["user_id"]);
                            repair.HomeId = Convert.ToInt32(reader["home_id"]);
                            repair.ProjectedCost = Convert.ToDecimal(reader["projected_cost"]);
                            repair.Cost = Convert.ToDecimal(reader["cost"]);
                            repair.LastRepairDate = Convert.ToDateTime(reader["last_repair_date"]).ToString("MM-dd-yyyy"); ;
                            repair.ExpectedLife = Convert.ToInt32(reader["expected_life"]);
                            repair.PotentialReplacementDate = Convert.ToDateTime(reader["potential_replacement_date"]).ToString("MM-dd-yyyy"); ;
                            repair.Description = Convert.ToString(reader["description"]);
                            repair.RepairId = Convert.ToInt32(reader["repair_id"]);
                            repair.Name = Convert.ToString(reader["name"]);

                            repairs.Add(repair);

                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return repairs;


        }
        public Repair GetRepair(int userId, int repairId)
        {
            Repair repair = new Repair();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from repairs where user_id = @user_id and repair_id = @repair_id ", conn);
                    cmd.Parameters.AddWithValue("@repair_id", repairId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            repair.HomeId = Convert.ToInt32(reader["home_id"]);
                            repair.ProjectedCost = Convert.ToDecimal(reader["projected_cost"]);
                            repair.Cost = Convert.ToDecimal(reader["cost"]);
                            repair.LastRepairDate = Convert.ToDateTime(reader["last_repair_date"]).ToString("MM-dd-yyyy"); ;
                            repair.ExpectedLife = Convert.ToInt32(reader["expected_life"]);
                            repair.PotentialReplacementDate = Convert.ToDateTime(reader["potential_replacement_date"]).ToString("MM-dd-yyyy"); ;
                            repair.Description = Convert.ToString(reader["description"]);
                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return repair;


        }
        public bool AddRepair(Repair repair)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO repairs (home_id, user_id, name ,projected_cost, cost, last_repair_date, expected_life, potential_replacement_date, description) VALUES(@home_id, @user_id, @name , @projected_cost, @cost, @last_repair_date, @expected_life, @potential_replacement_date, @description); SELECT @@IDENTITY", conn);
                    cmd.Parameters.AddWithValue("@home_id", repair.HomeId);
                    cmd.Parameters.AddWithValue("@user_id", repair.UserId);
                    cmd.Parameters.AddWithValue("@name", repair.Name);
                    cmd.Parameters.AddWithValue("@projected_cost", repair.ProjectedCost);
                    cmd.Parameters.AddWithValue("@cost", repair.Cost);
                    cmd.Parameters.AddWithValue("@last_repair_date", repair.LastRepairDate);
                    cmd.Parameters.AddWithValue("@expected_life", repair.ExpectedLife);
                    cmd.Parameters.AddWithValue("@potential_replacement_date", repair.PotentialReplacementDate);
                    cmd.Parameters.AddWithValue("@description", repair.Description);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    SqlCommand cmd2 = new SqlCommand("insert into reminders (home_id, user_id, repair_id, type, name, reminder_date) values (@home_id, @user_id, @repair_id, @type, @name, @reminder_date ) ", conn);
                    cmd2.Parameters.AddWithValue("@home_id", repair.HomeId);
                    cmd2.Parameters.AddWithValue("@user_id", repair.UserId);
                    cmd2.Parameters.AddWithValue("@repair_id", result);
                    cmd2.Parameters.AddWithValue("@type", "Repair");
                    cmd2.Parameters.AddWithValue("@name", repair.Name);
                    cmd2.Parameters.AddWithValue("@reminder_date", repair.PotentialReplacementDate);



                    int rowsAffected = cmd2.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }
        public bool UpdateRepair(Repair repair)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        Update repairs set name = @name, projected_cost = @projected_cost, cost = @cost, last_repair_date = @last_repair_date, expected_life = @expected_life, 
                        potential_replacement_date = @potential_replacement_date , description = @description where repair_id = @repair_id; UPDATE reminders set name = @name where repair_id = @repair_id", conn);

                    cmd.Parameters.AddWithValue("@repair_id", repair.RepairId);
                    cmd.Parameters.AddWithValue("@name", repair.Name);
                    cmd.Parameters.AddWithValue("@projected_cost", repair.ProjectedCost);
                    cmd.Parameters.AddWithValue("@cost", repair.Cost);
                    cmd.Parameters.AddWithValue("@last_repair_date", repair.LastRepairDate);
                    cmd.Parameters.AddWithValue("@expected_life", repair.ExpectedLife);
                    cmd.Parameters.AddWithValue("@potential_replacement_date", repair.PotentialReplacementDate);
                    cmd.Parameters.AddWithValue("@description", repair.Description);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public bool DeleteRepair(int userId, int repairId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE reminders where repair_id = @repair_id; DELETE repairs where user_id = @user_id AND repair_id = @repair_id", conn);

                    cmd.Parameters.AddWithValue("@repair_id", repairId);
                    cmd.Parameters.AddWithValue("@user_id", userId);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
    }
}
