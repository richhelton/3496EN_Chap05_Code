using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NServiceBus;
using NServiceBus.Installation.Environments;
using TimerMessages;

namespace TimePickerTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private static IBus _bus;

        public MainWindow()
		{
			this.InitializeComponent();

            _bus = Configure.With()
           .DefaultBuilder()
           .UseTransport<Msmq>()
           .UnicastBus()
           .CreateBus();

            Configure.Instance.ForInstallationOn<Windows>().Install();

			// Insert code required on object creation below this point.
		}
        private void Button_SubmitClick(object sender, System.Windows.RoutedEventArgs e)
        {

            SendTime s_Time = new SendTime();

            /*
             *  SFTP Info
             */ 
            s_Time.sftpServer = sftpServer.Text;
            s_Time.fileName = fileName.Text;
            s_Time.username = username.Text;
            s_Time.password = password.Text;
            /*
             * Timer Info
             */ 
            s_Time.HrTxt = HrTxt.Text;
            s_Time.MinTxt = MinTxt.Text;
            s_Time.SecTxt = SecTxt.Text;
            s_Time.AmPmTxt = AmPmTxt.Text;
            // Send it
            _bus.Send(s_Time);

        }
		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (HrBtn.IsChecked == true)
			{
				int hour = int.Parse(HrTxt.Text);
				
				if (hour == 12)
					hour = hour - hour;
				
				hour++;
				
				HrTxt.Text = hour.ToString();
			}
			else if (MinBtn.IsChecked == true)
			{
				int min = int.Parse(MinTxt.Text);
				
				if (min == 59)
					min = -1;
				
				min++;
				
				if (min.ToString().Length == 1)
				{
					MinTxt.Text = "0" + min.ToString();
				}
				else
				{
					MinTxt.Text = min.ToString();
				}
			}
			else if (SecBtn.IsChecked == true)
			{
				int sec = int.Parse(SecTxt.Text);
				
				if (sec == 59)
					sec = -1;
				
				sec++;
				
				if (sec.ToString().Length == 1)
				{
					SecTxt.Text = "0" + sec.ToString();
				}
				else
				{
					SecTxt.Text = sec.ToString();
				}
			}
			else if (AmPmBtn.IsChecked == true)
			{
				if(AmPmTxt.Text == "AM")
				{
					AmPmTxt.Text = "PM";
				}
				else
				{
					AmPmTxt.Text = "AM";
				}
			}
   
        }

		private void btn2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (HrBtn.IsChecked == true)
			{
				int hour = int.Parse(HrTxt.Text);
				
				if (hour == 1)
					hour = 13;
				
				hour--;
				
				HrTxt.Text = hour.ToString();
			}
			else if (MinBtn.IsChecked == true)
			{
				int min = int.Parse(MinTxt.Text);
				
				if (min == 0)
					min = 60;
				
				min--;
				
				
				if (min.ToString().Length == 1)
				{
					MinTxt.Text = "0" + min.ToString();
				}
				else
				{
					MinTxt.Text = min.ToString();
				}
			}
			else if (SecBtn.IsChecked == true)
			{
				int sec = int.Parse(SecTxt.Text);
				
				if (sec == 0)
					sec = 60;
				
				sec--;
				
				
				if (sec.ToString().Length == 1)
				{
					SecTxt.Text = "0" + sec.ToString();
				}
				else
				{
					SecTxt.Text = sec.ToString();
				}
			}
			else if (AmPmBtn.IsChecked == true)
			{
				if(AmPmTxt.Text == "AM")
				{
					AmPmTxt.Text = "PM";
				}
				else
				{
					AmPmTxt.Text = "AM";
				}
			}
        }

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
//			time.Text = DateTime.Now.ToLongTimeString();
		}
	}
}