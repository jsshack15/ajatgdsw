using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;



using System.Windows.Input;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Text;

namespace PivotApp4
{
    public partial class front2 : PhoneApplicationPage
    {
        IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
        public front2()
        {
            
            InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (myIsolatedStorage.FileExists("myFile.txt"))
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
           

            //create new file
            using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream("myFile.txt", FileMode.Create, FileAccess.Write, myIsolatedStorage)))
            {
                //string someTextData = "This is some text data to be saved in a new text file in the IsolatedStorage!";
                writeFile.WriteLine(a1.Text+b1.Text);
                writeFile.WriteLine(a1.Text + b2.Text);
                writeFile.WriteLine(a1.Text + b3.Text);
                writeFile.Close();
            }



            //IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            //Open existing file
            //IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("myFile.txt", FileMode.Open, FileAccess.Write);
            //using (StreamWriter writer = new StreamWriter(fileStream))
            //{
            //    //string someTextData = "Some More TEXT Added:  !";
            //    //writer.Write(someTextData);
            //    writer.Close();
            //}

            NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
            
            //NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));





        }
    }
}