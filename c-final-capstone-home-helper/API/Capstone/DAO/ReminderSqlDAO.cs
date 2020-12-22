using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Capstone.Models;
using System.Net.Mail;


namespace Capstone.DAO
{

    public class ReminderSqlDAO : IReminderDAO
    {
        private readonly string connectionString;


        public ReminderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;


        }

        public void GetMailReminders()
        {
            string sql = "Select distinct r.user_id, u.email from reminders r join users u on r.user_id = u.user_id where r.completed = 0 and (r.reminder_date BETWEEN GETDATE() AND GETDATE()+30)";
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        user.Email = Convert.ToString(reader["email"]);
                        users.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            foreach (User u in users)
            {
                GetMailReminders(u);
            }
        }
        public void GetMailReminders(User user)
        {
            List<ReminderHome> reminders = new List<ReminderHome>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select *  from home h join reminders r on r.home_id = h.home_id where r.user_id = @user_id and r.completed = 0 and (r.reminder_date BETWEEN GETDATE() AND GETDATE()+30)", conn);
                    cmd.Parameters.AddWithValue("@user_id", user.UserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ReminderHome reminder = ReminderToObject(reader);

                            reminders.Add(reminder);

                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            string email = "HomeHelperReminder@gmail.com";
            string hash = "wlsgymtmzsjblzqm";
            string message = $"";

            foreach (ReminderHome r in reminders)
            {
                message += $" Home Address: {r.Home.StreetAddress} Type: {r.Type} Item: {r.Name} Inspection Date or Warranty Expiration: {r.Date}{Environment.NewLine}";

            }

            if (reminders.Count != 0)
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, "Home Helper Reminder");
                mail.Bcc.Add(user.Email);

                mail.Subject = "Home Helper Reminder";
                mail.Body = $"Here is a Reminder for the following item(s):{Environment.NewLine}{message}";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, hash);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            
        }
        private ReminderHome ReminderToObject(SqlDataReader reader)
        {
            ReminderHome reminder = new ReminderHome();
            reminder.Home.HomeId = Convert.ToInt32(reader["home_id"]);
            reminder.Home.UserId = Convert.ToInt32(reader["user_id"]);
            reminder.Home.Description = Convert.ToString(reader["description"]);
            reminder.Home.TypeOfHome = Convert.ToString(reader["type"]);
            reminder.Home.Nickname = Convert.ToString(reader["nickname"]);
            reminder.Home.StreetAddress = Convert.ToString(reader["street_address"]);
            reminder.Home.City = Convert.ToString(reader["city"]);
            reminder.Home.State = Convert.ToString(reader["state"]);
            reminder.Home.Zip = Convert.ToString(reader["zip"]);
            reminder.Home.PicUrl = Convert.ToString(reader["pic_url"]);
            reminder.Name = Convert.ToString(reader["name"]);
            reminder.Date = Convert.ToDateTime(reader["reminder_date"]).ToString("MM-dd-yyyy");
            reminder.Type = Convert.ToString(reader["type"]);
            reminder.Completed = Convert.ToBoolean(reader["completed"]);
            if (reader["appliance_id"] is DBNull)
            {
                reminder.ApplianceId = null;
            }
            else
            {
                reminder.ApplianceId = Convert.ToInt32(reader["appliance_id"]);
            }
            if (reader["repair_id"] is DBNull)
            {
                reminder.RepairId = null;
            }
            else
            {
                reminder.RepairId = Convert.ToInt32(reader["repair_id"]);
            }
            return reminder;
        }
        private HomeReminder RowToObject(SqlDataReader reader)
        {
            HomeReminder homeReminder = new HomeReminder();


            homeReminder.HomeId = Convert.ToInt32(reader["home_id"]);
            homeReminder.UserId = Convert.ToInt32(reader["user_id"]);
            homeReminder.Description = Convert.ToString(reader["description"]);
            homeReminder.TypeOfHome = Convert.ToString(reader["type"]);
            homeReminder.Nickname = Convert.ToString(reader["nickname"]);
            homeReminder.StreetAddress = Convert.ToString(reader["street_address"]);
            homeReminder.City = Convert.ToString(reader["city"]);
            homeReminder.State = Convert.ToString(reader["state"]);
            homeReminder.Zip = Convert.ToString(reader["zip"]);
            homeReminder.BuildYear = Convert.ToInt32(reader["build_year"]);
            homeReminder.PurchaseYear = Convert.ToInt32(reader["purchase_date"]);
            homeReminder.PicUrl = Convert.ToString(reader["pic_url"]);

            return homeReminder;
        }
        public List<ReminderHome> GetReminderHomes(int homeId)
        {
            List<ReminderHome> reminders = new List<ReminderHome>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select *  from home h left join reminders r on r.home_id = h.home_id where r.home_id = @home_id and r.completed = 0 and (r.reminder_date BETWEEN GETDATE() AND GETDATE()+30)", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ReminderHome reminder = ReminderToObject(reader);

                            reminders.Add(reminder);

                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return reminders;
        }
        public HomeReminder GetReminderHome(int homeId)
        {

            HomeReminder homeReminder = new HomeReminder();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from home h join milestones m on m.home_id = h.home_id left join reminders r on r.home_id = h.home_id and r.completed = 0 and (r.reminder_date BETWEEN GETDATE() AND GETDATE()+30)  where h.home_id = @home_id  ", conn);
                    cmd.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            homeReminder = RowToObject(reader);
                        }
                    }
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("Select * from reminders where home_id = @home_id and completed = 0 and (reminder_date BETWEEN GETDATE() AND GETDATE()+30)", conn);
                    cmd2.Parameters.AddWithValue("@home_id", homeId);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Reminder reminder = new Reminder();
                            reminder.Name = Convert.ToString(reader["name"]);
                            reminder.Date = Convert.ToDateTime(reader["reminder_date"]).ToString("MM-dd-yyyy");
                            reminder.Type = Convert.ToString(reader["type"]);
                            reminder.Completed = Convert.ToBoolean(reader["completed"]);
                            if (reader["appliance_id"] is DBNull)
                            {
                                reminder.ApplianceId = null;
                            }

                            else
                            {
                                reminder.ApplianceId = Convert.ToInt32(reader["appliance_id"]);
                            }
                            if (reader["repair_id"] is DBNull)
                            {
                                reminder.RepairId = null;
                            }
                            else
                            {
                                reminder.RepairId = Convert.ToInt32(reader["repair_id"]);
                            }
                            homeReminder.Reminders.Add(reminder);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return homeReminder;
        }

      
    }
}

