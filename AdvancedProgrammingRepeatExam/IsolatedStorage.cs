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
        //isolated store - folder - will hold our own named folders and isolated storage files
        private IsolatedStorageFile store;

        //create the name of a named folder
        string folderName;

        //path to an isolated storage file
        string pathToFile;

        public IsolatedStorage()
        {
            //setting up the store to be isolated by user and assembly
            //option 1 
            //store = IsolatedStorageFile.GetUserStoreForAssembly();
            //option 2
            // store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            //change the isolation type to user, assembly and domain
            store = IsolatedStorageFile.GetUserStoreForDomain();

            folderName = "FolderForIsolatedStorage";

            pathToFile = String.Format("{0}\\IsolatedStorage.txt", folderName);

        }

        //method which writes to the isolated storage file
        public Boolean WriteToStorage(string s)
        {
            //check if the store was correctly created
            if (store == null)
            {
                return false;
            }

            try
            {
                //check if the folder exists or not. If it does not exist then create it
                if (!store.DirectoryExists(folderName))
                    //create the named folder inside the store
                    store.CreateDirectory(folderName);

                //create the isolated storage file
                using (IsolatedStorageFileStream IsoStoragefile =
                    store.OpenFile(pathToFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(IsoStoragefile))
                    {
                        writer.Write(s);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //method to read from the isolated storage file
        public String ReadFromStorage()
        {
            //check if the store was correctly created
            if (store == null)
            {
                return string.Empty;
            }

            try
            {
                //access the IsolatedStorageFileStream - our isolated storage file
                using (IsolatedStorageFileStream IsoStoragefile =
                    store.OpenFile(pathToFile, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(IsoStoragefile))
                    {
                        return reader.ReadToEnd();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }

        }
    }
}
