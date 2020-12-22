using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ApplianceSqlDAO : IApplianceDAO
    {
        private readonly string connectionString;
        public ApplianceSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Appliance GetAppliance(int userId, int applianceId)
        {
            Appliance appliance = new Appliance();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from appliance where user_id = @user_id and appliance_id = @appliance_id ", conn);
                    cmd.Parameters.AddWithValue("@appliance_id", applianceId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            appliance.ApplianceId = Convert.ToInt32(reader["appliance_id"]);
                            appliance.HomeId = Convert.ToInt32(reader["home_id"]);
                            appliance.UserId = Convert.ToInt32(reader["user_id"]);
                            appliance.Name = Convert.ToString(reader["name"]);
                            appliance.Make = Convert.ToString(reader["make"]);
                            appliance.Cost = Convert.ToDecimal(reader["cost"]);
                            appliance.ModelNumber = Convert.ToString(reader["model_number"]);
                            appliance.SerialNumber = Convert.ToString(reader["serial_number"]);
                            appliance.WarrantyExpiration = Convert.ToString(reader["warranty_expiration"]);
                            appliance.PurchaseDate = Convert.ToDateTime(reader["purchase_date"]).ToString("MM-dd-yyyy");
                            appliance.Description = Convert.ToString(reader["description"]);
                            appliance.EstimatedDelivery = Convert.ToDateTime(reader["estimated_delivery"]).ToString("MM-dd-yyyy");
                            appliance.DeliveryDate = Convert.ToDateTime(reader["delivery_date"]).ToString("MM-dd-yyyy");
                            appliance.ReceiptUrl = Convert.ToString(reader["receipt_url"]);
                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return appliance;
        }
        public List<Appliance> GetAppliances(int userId, int homeId)
        {
            List<Appliance> appliances = new List<Appliance>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from appliance where user_id = @user_id and home_id = @home_id ", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Appliance appliance = new Appliance();

                            appliance.ApplianceId = Convert.ToInt32(reader["appliance_id"]);
                            appliance.HomeId = Convert.ToInt32(reader["home_id"]);
                            appliance.UserId = Convert.ToInt32(reader["user_id"]);
                            appliance.Name = Convert.ToString(reader["name"]);
                            appliance.Make = Convert.ToString(reader["make"]);
                            appliance.Cost = Convert.ToDecimal(reader["cost"]);
                            appliance.ModelNumber = Convert.ToString(reader["model_number"]);
                            appliance.SerialNumber = Convert.ToString(reader["serial_number"]);
                            appliance.WarrantyExpiration = Convert.ToDateTime(reader["warranty_expiration"]).ToString("MM-dd-yyyy");
                            appliance.PurchaseDate = Convert.ToDateTime(reader["purchase_date"]).ToString("MM-dd-yyyy");
                            appliance.Description = Convert.ToString(reader["description"]);
                            appliance.EstimatedDelivery = Convert.ToDateTime(reader["estimated_delivery"]).ToString("MM-dd-yyyy");
                            appliance.DeliveryDate = Convert.ToDateTime(reader["delivery_date"]).ToString("MM-dd-yyyy");
                            appliance.ReceiptUrl = Convert.ToString(reader["receipt_url"]);

                            appliances.Add(appliance);

                        }
                    }

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return appliances;
        }
        public bool AddAppliance(Appliance appliance)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO appliance ( home_id, user_id , name, make, cost, model_number, serial_number, warranty_expiration, purchase_date, description, estimated_delivery, delivery_date, receipt_url)     
                    VALUES( @home_id, @user_id , @name, @make,@cost, @model_number, @serial_number, @warranty_expiration, @purchase_date, @description, @estimated_delivery, @delivery_date, @receipt_url); SELECT @@IDENTITY", conn);



                    cmd.Parameters.AddWithValue("@home_id", appliance.HomeId);
                    cmd.Parameters.AddWithValue("@user_id", appliance.UserId);
                    cmd.Parameters.AddWithValue("@name", appliance.Name);
                    cmd.Parameters.AddWithValue("@make", appliance.Make);
                    cmd.Parameters.AddWithValue("@cost", appliance.Cost);
                    cmd.Parameters.AddWithValue("@model_number", appliance.ModelNumber);
                    cmd.Parameters.AddWithValue("@serial_number", appliance.SerialNumber);
                    cmd.Parameters.AddWithValue("@warranty_expiration", appliance.WarrantyExpiration);
                    cmd.Parameters.AddWithValue("@purchase_date", appliance.PurchaseDate);
                    cmd.Parameters.AddWithValue("@description", appliance.Description);
                    cmd.Parameters.AddWithValue("@estimated_delivery", appliance.EstimatedDelivery);
                    cmd.Parameters.AddWithValue("@delivery_date", appliance.DeliveryDate);
                    cmd.Parameters.AddWithValue("@receipt_url", appliance.ReceiptUrl);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    SqlCommand cmd2 = new SqlCommand("insert into reminders (home_id, user_id ,appliance_id ,type,  name, reminder_date) values (@home_id, @user_id, @appliance_id, @type,  @name, @reminder_date) ", conn);
                    cmd2.Parameters.AddWithValue("@home_id", appliance.HomeId);
                    cmd2.Parameters.AddWithValue("@user_id", appliance.UserId);
                    cmd2.Parameters.AddWithValue("@appliance_id", result);
                    cmd2.Parameters.AddWithValue("@type", "Appliance");
                    cmd2.Parameters.AddWithValue("@name", appliance.Name);
                    cmd2.Parameters.AddWithValue("@reminder_date", appliance.WarrantyExpiration);

                    int rowsAffected = cmd2.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }



            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool UpdateAppliance(Appliance appliance)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"Update appliance 
                    set name = @name, make = @make, cost = @cost, model_number = @model_number, serial_number = @serial_number, warranty_expiration = @warranty_expiration,
                    purchase_date = @purchase_date, description = @description, estimated_delivery = @estimated_delivery, delivery_date = @delivery_date, receipt_url = @receipt_url
                    where appliance_id = @appliance_id; UPDATE reminders set name = @name where appliance_id = @appliance_id", conn);


                    cmd.Parameters.AddWithValue("@appliance_id", appliance.ApplianceId);
                    cmd.Parameters.AddWithValue("@name", appliance.Name);
                    cmd.Parameters.AddWithValue("@make", appliance.Make);
                    cmd.Parameters.AddWithValue("@cost", appliance.Cost);
                    cmd.Parameters.AddWithValue("@model_number", appliance.ModelNumber);
                    cmd.Parameters.AddWithValue("@serial_number", appliance.SerialNumber);
                    cmd.Parameters.AddWithValue("@warranty_expiration", appliance.WarrantyExpiration);
                    cmd.Parameters.AddWithValue("@purchase_date", appliance.PurchaseDate);
                    cmd.Parameters.AddWithValue("@description", appliance.Description);
                    cmd.Parameters.AddWithValue("@estimated_delivery", appliance.EstimatedDelivery);
                    cmd.Parameters.AddWithValue("@delivery_date", appliance.DeliveryDate);
                    cmd.Parameters.AddWithValue("@receipt_url", appliance.ReceiptUrl);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                return false;
            }

        }
        public bool DeleteAppliance(int userId, int applianceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE reminders where appliance_id = @appliance_id; DELETE appliance where user_id = @user_id AND appliance_id = @appliance_id", conn);

                    cmd.Parameters.AddWithValue("@appliance_id", applianceId);
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
