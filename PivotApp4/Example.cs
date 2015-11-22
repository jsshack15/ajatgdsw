using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections;
using System.Text;

namespace PivotApp4
{ 

class Example
{
    public static void Demo(TextBlock outputBlock)
    {
        // Obtain an isolated store for an application.
        try
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Use a StringBuilder to construct output.
                StringBuilder sb = new StringBuilder();

                // Create three directories in the root.
                store.CreateDirectory("MyApp1");
                store.CreateDirectory("MyApp2");
                store.CreateDirectory("MyApp3");

                // Create three subdirectories under MyApp1.
                string subdirectory1 = Path.Combine("MyApp1", "SubDir1");
                string subdirectory2 = Path.Combine("MyApp1", "SubDir2");
                string subdirectory3 = Path.Combine("MyApp1", "SubDir3");
                store.CreateDirectory(subdirectory1);
                store.CreateDirectory(subdirectory2);
                store.CreateDirectory(subdirectory3);

                // Create a file in the root.
                IsolatedStorageFileStream rootFile = store.CreateFile("InTheRoot.txt");
                rootFile.Close();

                // Create a file in a subdirectory.
                IsolatedStorageFileStream subDirFile =
                    store.CreateFile(Path.Combine(subdirectory1, "MyApp1A.txt"));
                subDirFile.Close();

                // Gather file information
                string[] directoriesInTheRoot = store.GetDirectoryNames();

                string[] filesInTheRoot = store.GetFileNames();

                string searchpath = Path.Combine(subdirectory1, "*.*");
                string[] filesInSubDirs = store.GetFileNames(searchpath);

                // Find subdirectories within the MyApp1
                // directory using the multi character '*' wildcard.
                string[] subDirectories =
                    store.GetDirectoryNames(Path.Combine("MyApp1", "*"));

                // List file information

                // List the directories in the root.
                sb.AppendLine("Directories in root:");
                foreach (string dir in directoriesInTheRoot)
                {
                    sb.AppendLine(" - " + dir);
                }
                sb.AppendLine();

                // List the subdirectories under MyApp1.
                sb.AppendLine("Directories under MyApp1:");
                foreach (string sDir in subDirectories)
                {
                    sb.AppendLine(" - " + sDir);
                }
                sb.AppendLine();

                // List files in the root.
                sb.AppendLine("Files in the root:");
                foreach (string fileName in filesInTheRoot)
                {
                    sb.AppendLine(" - " + fileName);
                }
                sb.AppendLine();

                // List files in MyApp1\SubDir1.
                sb.AppendLine(@"Files in MyApp1\SubDir1:");
                foreach (string fileName in filesInSubDirs)
                {
                    sb.AppendLine(" - " + fileName);
                }
                sb.AppendLine();

                // Write to an existing file: MyApp1\SubDir1\MyApp1A.txt

                // Determine if the file exists before writing to it.
                string filePath = Path.Combine(subdirectory1, "MyApp1A.txt");

                if (store.FileExists(filePath))
                {
                    try
                    {
                        using (StreamWriter sw =
                            new StreamWriter(store.OpenFile(filePath,
                                FileMode.Open, FileAccess.Write)))
                        {
                            sw.WriteLine("To do list:");
                            sw.WriteLine("1. Buy supplies.");
                        }
                    }
                    catch (IsolatedStorageException ex)
                    {

                        sb.AppendLine(ex.Message);
                    }
                }

                // Read the contents of the file: MyApp1\SubDir1\MyApp1A.txt
                try
                {
                    using (StreamReader reader =
                        new StreamReader(store.OpenFile(filePath,
                            FileMode.Open, FileAccess.Read)))
                    {
                        string contents = reader.ReadToEnd();
                        sb.AppendLine(filePath + " contents:");
                        sb.AppendLine(contents);
                    }
                }
                catch (IsolatedStorageException ex)
                {

                    sb.AppendLine(ex.Message);
                }

                // Delete a file.
                try
                {
                    if (store.FileExists(filePath))
                    {
                        store.DeleteFile(filePath);
                    }
                }
                catch (IsolatedStorageException ex)
                {
                    sb.AppendLine(ex.Message);
                }

                // Delete a specific directory.
                string dirDelete = Path.Combine("MyApp1", "SubDir3");
                try
                {
                    if (store.DirectoryExists(dirDelete))
                    {
                        store.DeleteDirectory(dirDelete);
                    }
                }
                catch (IsolatedStorageException ex)
                {
                    sb.AppendLine(ex.Message);
                }


                sb.AppendLine();


                // remove the store
                store.Remove();

                sb.AppendLine("Store removed.");

                outputBlock.Text = sb.ToString();
            }
        }
        catch (IsolatedStorageException)
        {
            // TODO: Handle that store was unable to be accessed.

        }
    }
}

// Quota status and increase quota examples
class StoreQuota
{

    // Assumes an event handler for the MouseLeftbuttonUp
    // event is defined for a control named 'IncreaseQuota'
    // In the control's XAML: MouseLeftButtonUp="IncreaseQuota_OnLeftMouseButtonUp"

    // User first selects UI to increase the quota.
    public void IncreaseQuota_OnClick(object sender, MouseEventArgs e)
    {

        // Obtain an isolated store for an application.
        try
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Request 5MB more space in bytes.
                Int64 spaceToAdd = 5242880;
                Int64 curAvail = store.AvailableFreeSpace;

                // If available space is less than
                // what is requested, try to increase.
                if (curAvail < spaceToAdd)
                {

                    // Request more quota space.
                    if (!store.IncreaseQuotaTo(store.Quota + spaceToAdd))
                    {
                        // The user clicked NO to the
                        // host's prompt to approve the quota increase.
                    }
                    else
                    {
                        // The user clicked YES to the
                        // host's prompt to approve the quota increase.
                    }
                }
            }
        }

        catch (IsolatedStorageException)
        {
            // TODO: Handle that store could not be accessed.

        }
    }

    public static void ShowIsoStoreStatus(TextBlock inputBlock)
    {

        // Obtain an isolated store for an application.
        try
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string spaceUsed = (store.Quota - store.AvailableFreeSpace).ToString();
                string spaceAvailable = store.AvailableFreeSpace.ToString();
                string curQuota = store.Quota.ToString();
                inputBlock.Text =
                    String.Format("Quota: {0} bytes, Used: {1} bytes, Available: {2} bytes",
                            curQuota, spaceUsed, spaceAvailable);
            }
        }

        catch (IsolatedStorageException)
        {
            inputBlock.Text = "Unable to access store.";

        }
    }

}
    }
// This example's Example.Demo method
// produces the following output:
// -----
// Directories in root:
// - MyApp1
// - MyApp2
// - MyApp3

// Directories under MyApp1:
// - SubDir1
// - SubDir2
// - SubDir3

// Files in the root:
// - InTheRoot.txt

// Files in MyApp1\SubDir1:
// - MyApp1A.txt

// MyApp1\SubDir1\MyApp1A.txt contents:
// To do list:
// 1. Buy supplies.

// Store removed.
// ----
