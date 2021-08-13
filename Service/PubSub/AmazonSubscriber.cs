using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;

namespace Service.Event
{
    public class AmazonSubscriber
    {
        private int Pid = -1;
        public void Subscribe(AmazonPublisher publisher)
        {
            publisher.MyEvent += Update;
        }

        public void UnSubscribe(AmazonPublisher publisher)
        {
            publisher.MyEvent -= Update;
        }

        public void Update(object sender, PublisherEventArguments args)
        {
            AmazonPublisher publisher = (AmazonPublisher)sender;
            Console.WriteLine("Subscriber Start -> Publisher name:" + publisher.Name + ", Received message: " + args.Date);
            switch (args.Command)
            {
                case "Start":
                    LaunchCommandLineApp(args.Date);
                    break;
                case "Stop":
                    KillProcessAndChildren(Pid);
                    break;
            }
            Console.WriteLine("Subscriber Finish -> Publisher name:"  + publisher.Name + ", Received message: " + args.Date);
        }

        public void LaunchCommandLineApp(string date)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = true;
            startInfo.FileName = $"{Directory.GetCurrentDirectory()}\\AmazonWeatherApplication\\AmazonWeatherApplication.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = date;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    Pid = exeProcess.Id;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"AmazonSubscriber error, in LaunchCommandLineApp, ex: {e}\n");
                Console.WriteLine("Click any key and enter to continue...\n");
                Console.ReadLine();
            }
        }

        private static void KillProcessAndChildren(int pid)
        {
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid.ToString());
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
    }
}
