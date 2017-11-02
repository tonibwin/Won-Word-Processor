using System;
using System.IO;
using System.Collections.Generic;


namespace WonApplication {
   public class Document {
      private string fileName;
      private List<Page> pages;

      public Document() {
         fileName = "";
         pages = new List<Page>();
         pages.Add(new Page());
      }

      public string getFileName() { return fileName;  }

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

      public bool Save() {
         if (fileName.Equals("")) return false;
         System.IO.File.WriteAllText(fileName, pages[0].PassText());
         return true;
      }

      public bool SaveAs(string file) {
         if(setFileName(file)) return Save();
         return false;
      }

      
   }
}
