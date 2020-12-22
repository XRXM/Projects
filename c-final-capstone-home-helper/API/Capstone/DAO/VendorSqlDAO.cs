using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class VendorSqlDAO : IVendorDAO
    {
        private readonly string connectionString;
        public VendorSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Vendor> GetVendors(int repId)
        {

            List<Vendor> vendors = new List<Vendor>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from vendor v JOIN repairs r on r.home_id = v.home_id JOIN home h on h.home_id = r.home_id where r.repair_id = @repair_id", conn);
                    cmd.Parameters.AddWithValue("@repair_id", repId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vendor vendor = new Vendor();

                            vendor.HomeId = Convert.ToInt32(reader["home_id"]);
                            vendor.VendorId = Convert.ToInt32(reader["service_id"]);
                            vendor.VendorName = Convert.ToString(reader["vendor_name"]);
                            vendor.Phone = Convert.ToString(reader["phone"]);
                            vendor.Email = Convert.ToString(reader["email"]);
                            vendor.Specialty = Convert.ToString(reader["specialty"]);
                            vendor.Notes = Convert.ToString(reader["notes"]);
                            vendor.Zip = Convert.ToInt32(reader["zip"]);
                            vendor.Name = Convert.ToString(reader["name"]);

                            vendors.Add(vendor);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return vendors;
        }
        public List<Vendor> GetStores(int appId)
        {
            List<Vendor> vendors = new List<Vendor>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from store s JOIN appliance a on a.home_id = s.home_id JOIN home h on h.home_id = a.home_id where a.appliance_id = @appliance_id", conn);
                    cmd.Parameters.AddWithValue("@appliance_id", appId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vendor vendor = new Vendor();

                            vendor.HomeId = Convert.ToInt32(reader["home_id"]);
                            vendor.VendorId = Convert.ToInt32(reader["store_id"]);
                            vendor.VendorName = Convert.ToString(reader["store_name"]);
                            vendor.Phone = Convert.ToString(reader["phone"]);
                            vendor.Notes = Convert.ToString(reader["notes"]);
                            vendor.Zip = Convert.ToInt32(reader["zip"]);
                            vendor.Name = Convert.ToString(reader["name"]);

                            vendors.Add(vendor);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return vendors;
        }
        public List<Vendor> GetStoreList(int homeId)
        {
            List<Vendor> vendors = new List<Vendor>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from store where home_id = @home_id", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vendor vendor = new Vendor();

                            vendor.HomeId = Convert.ToInt32(reader["home_id"]);
                            vendor.VendorName = Convert.ToString(reader["store_name"]);
                            vendor.Phone = Convert.ToString(reader["phone"]);
                            vendor.Notes = Convert.ToString(reader["notes"]);
                            vendor.VendorId = Convert.ToInt32(reader["store_id"]);

                            vendors.Add(vendor);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return vendors;
        }
        public List<Vendor> GetServiceList(int homeId)
        {
            List<Vendor> services = new List<Vendor>();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from vendor where home_id = @home_id", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vendor service = new Vendor();

                            service.HomeId = Convert.ToInt32(reader["home_id"]);
                            service.VendorName = Convert.ToString(reader["vendor_name"]);
                            service.Email = Convert.ToString(reader["email"]);
                            service.Phone = Convert.ToString(reader["phone"]);
                            service.Specialty = Convert.ToString(reader["specialty"]);
                            service.Notes = Convert.ToString(reader["notes"]);
                            service.VendorId = Convert.ToInt32(reader["service_id"]);

                            services.Add(service);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return services;
        }
        public bool AddVendor(Home home, int homeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into vendor(home_id, vendor_name, phone, email, specialty, notes) VALUES(@home_id, @vendor_name, @phone, @email, @specialty, @notes)", conn);


                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@vendor_name", home.VendorName);
                    cmd.Parameters.AddWithValue("@phone", home.VendorPhone);
                    cmd.Parameters.AddWithValue("@email", home.VendorEmail);
                    cmd.Parameters.AddWithValue("@specialty", home.VendorSpecialty);
                    cmd.Parameters.AddWithValue("@notes", home.VendorNotes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool AddStore(Home home, int homeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into store(home_id, store_name, phone, notes) VALUES(@home_id, @store_name, @phone, @notes)", conn);

                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    cmd.Parameters.AddWithValue("@store_name", home.StoreName);
                    cmd.Parameters.AddWithValue("@phone", home.StorePhone);
                    cmd.Parameters.AddWithValue("@notes", home.StoreNotes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool AddNewStore(Vendor vendor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into store(home_id, store_name, phone, notes) VALUES(@home_id, @store_name, @phone, @notes)", conn);

                    cmd.Parameters.AddWithValue("@home_id", vendor.HomeId);
                    cmd.Parameters.AddWithValue("@store_name", vendor.VendorName);
                    cmd.Parameters.AddWithValue("@phone", vendor.Phone);
                    cmd.Parameters.AddWithValue("@notes", vendor.Notes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool AddNewService(Vendor service)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into vendor(home_id, vendor_name, email, specialty, phone, notes) VALUES(@home_id, @vendor_name, @email, @specialty, @phone, @notes)", conn);

                    cmd.Parameters.AddWithValue("@home_id", service.HomeId);
                    cmd.Parameters.AddWithValue("@vendor_name", service.VendorName);
                    cmd.Parameters.AddWithValue("@email", service.Email);
                    cmd.Parameters.AddWithValue("@phone", service.Phone);
                    cmd.Parameters.AddWithValue("@specialty", service.Specialty);
                    cmd.Parameters.AddWithValue("@notes", service.Notes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool UpdateStore(Vendor vendor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update store SET store_name = @store_name, phone = @phone, notes = @notes WHERE store_id = @store_id", conn);

                    cmd.Parameters.AddWithValue("@store_id", vendor.VendorId);
                    cmd.Parameters.AddWithValue("@store_name", vendor.VendorName);
                    cmd.Parameters.AddWithValue("@phone", vendor.Phone);
                    cmd.Parameters.AddWithValue("@notes", vendor.Notes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool UpdateService(Vendor service)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update vendor SET vendor_name = @vendor_name, email = @email, specialty = @specialty, phone = @phone, notes = @notes WHERE service_id = @vendor_id", conn);

                    cmd.Parameters.AddWithValue("@vendor_id", service.VendorId);
                    cmd.Parameters.AddWithValue("@vendor_name", service.VendorName);
                    cmd.Parameters.AddWithValue("@email", service.Email);
                    cmd.Parameters.AddWithValue("@phone", service.Phone);
                    cmd.Parameters.AddWithValue("@specialty", service.Specialty);
                    cmd.Parameters.AddWithValue("@notes", service.Notes);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool DeleteStore(int storeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete store WHERE store_id = @store_id", conn);

                    cmd.Parameters.AddWithValue("@store_id", storeId);


                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public bool DeleteService(int serviceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete vendor WHERE service_id = @service_id", conn);

                    cmd.Parameters.AddWithValue("@service_id", serviceId);


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
