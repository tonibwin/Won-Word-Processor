using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System;

namespace Won.Model{
   public class DocumentModel { }
   public class Document : INotifyPropertyChanged {
      private string fileName;
      public ObservableCollection<Page> pages;

      public string FileName {
         get { return fileName;  }
         set {
            //filename check before setting
            //update filename
            //cannot contain @[^\/:*?"<>|]+
            Regex prevent = new Regex("[^\\/:*?\"<>\\|]");
            if (!prevent.IsMatch(value)) {
               fileName = value;
               RaisePropertyChanged("FileName");
            }
            else Console.WriteLine("FileName invalid, Cannot contain \\/:*?\"<>\\| ");
         }
      }

      public bool Save() {
         //save to a text file using the ToString Method from Text
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName)) ;
            
            return false;
      } // saves file by using text.toString to a text file into a specified format
      public bool SaveAs(string file) {
         FileName = file; //attemps to change filename, if successful saves
         if (FileName == file) return Save();
         else Console.WriteLine("SaveAs Failed");
         return false;

      } 
      public bool Load(string file) {
         return false;
      } //loads opposite of the specified format and adds text object to page's Text collection

      public event PropertyChangedEventHandler PropertyChanged;

      private void RaisePropertyChanged(string property) {
         if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
   }
}
