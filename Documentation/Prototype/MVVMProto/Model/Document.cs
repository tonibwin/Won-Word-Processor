using System;
using System.IO;
using System.Collections.Generic;



namespace WonApplication {
   public class Document {
      
      private string fileName;
      private List<Page> pages;
      private const string PAGE_DELIMETER = "%\n%\n%\n\n\t#\t#\t#****PAGE DELIMETER****\t#\t#\t#\n%\n%\n%\n";
      private const string TOSTRING_PAGE_DELIMETER = "TOSTRING METHOD: PAGE END HEADER\n\n\nTOSTRING METHOD: PAGE END FOOTER\n\n\n";
      private const int STRING_NOT_FOUND = -1;
      private const int FIRST_PAGE = 0;

      public Document() {
         fileName = "document1";
         pages = new List<Page>();
         pages.Add(new Page());
      }

      public string getFileName() { return fileName;  }

      public List<Page> getPageList() { return pages; }

        /*   Author: Zachary Lowery
         *   Date: 11/6/2017
         *   Description: A toString method meant to be used for console output. Be aware of the delimeters used in this function.
         *   Pre-Condition: N/A
         *   Post-Condition: A string containing the page contents are uploaded.
         *   Running-Time: O(n * Running-Time of PassText (Page method)) where n is the number of pages.
         *   
         */
        public override String ToString()
      {
         string stringBuffer = "";
         for (int pageNumber = FIRST_PAGE; pageNumber < pages.Count; pageNumber++)
         {
            stringBuffer += pages[pageNumber].PassText();
            stringBuffer += TOSTRING_PAGE_DELIMETER;
         }
         return stringBuffer;
      }


        /*   Author: Zachary Lowery
         *   Date: 11/6/2017
         *   Description: A mutator that empties the current page list from a document instance and 
         *                inserts a new page list as given by 'newPageList'
         *   Pre-Condition: N/A
         *   Post-Condition: The Document instance that calls this method now contains a new Page List.
         *   Running-Time: O(n * Running-Time of Insert (List method)) where n is the number of elements in the new list.
         *   
         */
        public void setPageList(List<Page> newPageList)
      {
            pages.Clear();
            for(int pageNumber = FIRST_PAGE; pageNumber < newPageList.Count; pageNumber++)
            {
                pages.Insert(pageNumber, newPageList[pageNumber]);
            }  
      }

      //setFileName
      //Precondtion: file is a string that starts with an alpha character, file doesn't end with a file type specifier
      //Postcondition: sets fileName to file + ".txt" and returns true, else returns false  
      public bool setFileName(string file) {
         if (Char.IsLetter(file[0])) {
            fileName = file + ".txt";
            return true;
         }

         return false;
      }


        /*   Author: Unknown (Probably Jordan Leibman)
         *   Contributor: Zachary Lowery
         *   Date: 11/6/2017
         *   Description: A function that takes the contents of a document and saves them to a plain text file.
         *   Pre-Condition: The private member variable of the document must be assigned to something other than the empty string.
         *   Post-Condition: The contents of the document are saved to a plain text file. A delimeter is inserted so the load function knows how to break up the 
         *                  file when loading.
         *   Implications: This function uses two calls to AppendAllText( Changed from 'WriteAllText' ) to save to the given file, once to save the contents of the page,
         *                once again to insert a delimeter. This is inefficient. It would be better to save all contents of the pages to a string buffer, insert the delimeters manually, and then
         *                call AppendAllText ONCE. I have done this the crappy way due to our tight schedule. If we call save alot for various reasons and things are slowing down, this is probably why.
         *                
         *                CAUTION: Both file functions we are using ('AppendAllText' and 'WriteAllText') both create directories/files if they were not present in the hard-drive before. This is for convienance
         *                and is a good thing. HOWEVER, if we pass it bad values or stupidly pass something too it we can make a mess of our directories on our local machines. And consequently the machines of others. 
         *                Be careful. . . 
         *   
         *   Running-Time: Unknown, heavily relies on the speed of the hard drive given this function writes to the harddrive twice per page.
         *   
         */
        public bool Save() {
         if (fileName.Equals("")) return false;

            //I have changed your original call to now create a file (even if it doesn't exist), and then continue for all 
            //pages in the list. So we can support saving documents that are more than one page long. Although the Document
            //class needs to be modified to support that.
            for (int pageNumber = 0; pageNumber < pages.Count; pageNumber++){
                System.IO.File.AppendAllText(fileName, pages[pageNumber].PassText());

                //The page delimeter must be unique enough that a person using our editor doesn't accidently enter it in our file and end up with a
                //non-sensical page for no reason.
                System.IO.File.AppendAllText(fileName, PAGE_DELIMETER);
            }

         return true;
      }

        /*   Author: Unknown (Probably Jordan Leibman)
        *   Contributor: Zachary Lowery
        *   Date: 11/6/2017
        *   Description: Uses the Save() function primarily, but also reassigns the file name of the document.
        *   Pre-Condition: The private member variable of the document must be assigned to something other than the empty string.
        *   Post-Condition: The contents of the document are saved to a plain text file. A delimeter is inserted so the load function knows how to break up the 
        *                  file when loading.
        *   Implications: This function uses two calls to AppendAllText( Changed from 'WriteAllText' ) to save to the given file, once to save the contents of the page,
        *                once again to insert a delimeter. This is inefficient. It would be better to save all contents of the pages to a string buffer, insert the delimeters manually, and then
        *                call AppendAllText ONCE. I have done this the crappy way due to our tight schedule. If we call save alot for various reasons and things are slowing down, this is probably why.
        *                
        *                CAUTION: Both file functions we are using ('AppendAllText' and 'WriteAllText') both create directories/files if they were not present in the hard-drive before. This is for convienance
        *                and is a good thing. HOWEVER, if we pass it bad values or stupidly pass something too it we can make a mess of our directories on our local machines. And consequently the machines of others. 
        *                Be careful. . . 
        *   
        *   Running-Time: Unknown, heavily relies on the speed of the hard drive given this function writes to the harddrive twice per page.
        *   
        */
      public bool SaveAs(string file) {
         if(setFileName(file)) return Save();
         return false;
      }


        /*    Author: Unknown (Probably Jordan Leibman)
          *   Contributor: Zachary Lowery
          *   Date: 11/6/2017
          *   Description: A function that takes the contents of a file that was created using the Save() function, and transforms an existing Document instance to match the specifications of the 
          *                file.
          *   Pre-Condition: The file parameter, must point to a valid file path.
          *   Post-Condition: The caller (Document instance) has its member variables changed to adhere to the specifications given in the plain text file generated by the Save() function.
          *                   If the file was NOT generated by the Save() function, this will produce a very long one-page document. This should be rectified in this function at a later date.
          *   Implications: If the referenced file was generated using our program, (by using the Save() function) then this function will work great. Otherwise, they will be treated to a document
          *                 that shows the first page while the rest of the text exists in memory but (presumably) isn't displayed by the UI. Either some section of the UI will have to detect and compensate 
          *                 for this (by taking the "hidden" text and generating substrings of appropriately sized portions by slicing, or this function will have to learn to "detect" when a document needs
          *                 a new page inserted for a given length. I will discuss this subject in greater depth with the Lead Programmer.
          *   
          *   Running-Time: This function actually does things right and uses the hard disk exactly once. After that it uses the buffer to take the information, slice it up appropriately. And transform a Document Instance.
          *                 Takes roughly O(n * Running-Time[<Page>.appendTextList] * Running-Time[<List>.add()] * Running-Time[<List>.Insert()]), where n is the number of pages in the document.
          *                 
          *                 Educated guess: Worse than O(n) but better than O(n^2)
          *   
       */
      public bool Load(string file)
      {
            if (file.Equals("")) return false;

            List<Page> temporaryContainer = new List<Page>();
            string fileContainer = "";

            fileContainer = System.IO.File.ReadAllText(file);

            //More than one page in a file.
            if (fileContainer.Contains(PAGE_DELIMETER)) {

                Int32 stringOffset = 0;
                Int32 pageIndexStart = 0;

                for (int pageNumber = FIRST_PAGE; (stringOffset = fileContainer.IndexOf(PAGE_DELIMETER, pageIndexStart)) != STRING_NOT_FOUND; pageNumber++){
                    temporaryContainer.Add(new Page());
                    temporaryContainer[pageNumber].appendTextList(fileContainer.Substring(pageIndexStart, stringOffset));
                    pageIndexStart += stringOffset + PAGE_DELIMETER.Length;
               }    
            }

            //One page in a file.
            else{
                temporaryContainer.Add(new Page());
                temporaryContainer[FIRST_PAGE].appendTextList(fileContainer);
            }

            //Did the load work?
            if (temporaryContainer.Count > 0)
            {

                //Remove all the old content that may have been in that document.
                pages.Clear();
                for (int pageNumber = FIRST_PAGE; pageNumber < temporaryContainer.Count; pageNumber++)
                {
                    pages.Insert(pageNumber, temporaryContainer[pageNumber]);
                }

                if (pages.Count > 0) { fileName = file; return true; }
                else return false;

            }
            else return false;      
      }

      
   }
}
