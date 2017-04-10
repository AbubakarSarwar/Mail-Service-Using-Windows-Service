using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Xml;
namespace Question1_Assignment2_
{
    //A refresher that my assignment is checking the folder named 'email' (every 15 mins) which is in my documents and 
    //if there are any 'xml' files it will send emails to all the files separately but at one go Then it will run after
    //15 minutes again and check again and so on. 
    //You can go to app.config to change the mailing address and password and host    
    //I know there is an issue which is that the files are not being deleted, so it means if I have 1 file in my folder it will send
    // email to that file now after 15 minutes if i have 3 more files, it will send mail to those 3 files and also the 1 it sent before
    // I wasn't sure if i should delete the past one or not. So I choosed not to, because if i would, you wouldnt be able to see the previous file.
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;
        public Service1()
        {
            InitializeComponent();
        }
        public void onDebug(){
            OnStart(null);
        }
        protected override void OnStart(string[] args){
            _timer = new Timer(15 * 60 * 1000); // It will now run after every 15 mins
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            _timer.Start();

        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\k132212\Documents\Emails");
            FileInfo[] TXTFiles = di.GetFiles("*.xml");
            if (TXTFiles.Length == 0)
            {
                Console.WriteLine("File does not exist.");
            }
            else
            {
                String[] xmlvalues = new String[4];
   
                Console.WriteLine("File exist.");
                foreach (string file in Directory.EnumerateFiles(@"C:\Users\k132212\Documents\Emails", "*.xml"))
                {

                    XmlTextReader reader = new XmlTextReader(file);
                    int i = 0;
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Text:
                                xmlvalues[i] = reader.Value;
                                Console.WriteLine(xmlvalues[i]);
                                i++;
                                break;
                        }
                    }


                    try
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(xmlvalues[0]);
                        mail.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["Username"]);
                        mail.Subject = xmlvalues[1];
                        mail.Body = xmlvalues[2];
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = System.Configuration.ConfigurationManager.AppSettings["Host"];
                        smtp.Credentials = new System.Net.NetworkCredential
                             (System.Configuration.ConfigurationManager.AppSettings["Username"], System.Configuration.ConfigurationManager.AppSettings["Password"]);
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        Console.WriteLine("Successful");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                
                   }


            }
        
        protected override void OnStop()
        {
            
        }
    }
}
