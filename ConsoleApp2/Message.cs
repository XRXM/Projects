using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ConsoleApp2
{
    public class Message
    {

       public void SendingMessage(string str)
        {
            string address1 = "3304073556@vzwpix.com";
            string address2 = "3304070795@vzwpix.com";
            string email = "DicksDogFoodScales@gmail.com";
            string hash = "rabsntsdzppjiqhy";



            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, "Dicks Dog FoodScales");
                mail.Bcc.Add(address1);
                mail.Bcc.Add(address2);
               // mail.Subject = "Test Mail";
                mail.Body = str;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email,hash);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
               // MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }

        }


    }
}
