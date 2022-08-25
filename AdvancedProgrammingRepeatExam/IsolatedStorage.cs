using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AdvancedProgrammingRepeatExam
{
    internal class IsolatedStorage
    {
        //create an object to be used by lock()
        public static Object synObj = new Object();
        private IsolatedStorageFile store;

        //the name of the folder we will create in each isolated store
        private string folderName;

        //the path to the file - we will create a text file for each isolated store
        private string pathToTextFile;

        //constructor method which creates the isolated storage
        public IsolatedStorage(int selIndex)
        {
            //give the folder a name
            folderName = "ColourFolder";
            //set the path to the text file
            pathToTextFile = String.Format("{0}\\____File.txt", folderName);

            //set the isolation storage type and access the store (obtain the isolated store)
            if (selIndex == 0)
                store = IsolatedStorageFile.GetUserStoreForDomain();
            else
                store = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        //method which writes a colour to ColourFile.txt in the selected Isolated store 
        public void writeToStorage(Object colourFromUser)
        {
            string colourToSaveToStorage = ____FromUser.ToString();

            //check if the isolated store was obtained successfully (the code for that is in the constructor method)
            if (store != null)
            {

                //synchronise access to the isolated storage text file 
                Monitor.Enter(synObj);
                try
                {
                    //check if the folder exists.  If it does not, then create it
                    if (!store.DirectoryExists(folderName))
                        store.CreateDirectory(folderName);

                    //create the isolated storage file (the text file ColourFile.txt)
                    //We create a new ColourFile.txt every time this method is called
                    //- the new text file will overwrite any existing text file 
                    //(notice the FileMode.Create in the code below)
                    using (IsolatedStorageFileStream isoStorageTxtFile =
                        store.OpenFile(pathToTextFile, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStorageTxtFile))
                        {
                            writer.Write(colourToSaveToStorage);
                            MessageBox.Show("All good - colour saved to ColourFile.txt");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    Monitor.Pulse(synObj);
                    Monitor.Wait(synObj);
                }
            }

        }

        //method which reads the colour from ColourFile.txt (which is placed in the selected Isolated store) 
        public void readFromStorage()
        {
            if (store != null)
            {
                Monitor.Enter(synObj);
                try
                {
                    //read from the text file ColourFile.txt
                    using (IsolatedStorageFileStream isoStorageTxtFile = store.OpenFile(pathToTextFile, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(isoStorageTxtFile))
                        {
                            string colourFromTextFile = reader.ReadLine();

                            //we have to apply the colour as the background of the main Window
                            // we use Dispatcher.Invoke in order to accesss the controls on the main Window
                            // we need to point to an instance of the MainWindow class and then call Dispatcher.Invoke() on it
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    Monitor.Pulse(synObj);
                    Monitor.Wait(synObj);
                }
            }
        }
    }
}
