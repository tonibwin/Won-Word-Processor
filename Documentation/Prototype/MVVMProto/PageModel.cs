/* PageModel.cs  
 * Written By Destoni Baldwin
 * Wrriten on 10/20/2017
 * Purpose: Retrieves information form both TextModel.cs and DocumentModel.cs to hold text.
 * Controls when text is inserted or removed and should update cursor position when text is changed.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections;
namespace Won.Model {

   public class PageModel { }

   public class Page : INotifyPropertyChanged {

      //Instance Variables
      private int cursor;
      public ObservableCollection<Text> text = new ObservableCollection<Text>();

      public event PropertyChangedEventHandler PropertyChanged;

      private void RaisePropertyChanged(string property) {
         if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
         }
      }

      //Default Constructor
      public Page() { }

      //A constructor that creates a page object giving it the data
      //for a page number and the text that belongs on the page.
      //Pre-Condition: Input of pos variable and text variable.
      //Post-Condition: Creates a page object
      public Page(int cursor, ObservableCollection<Text> text) {
         this.cursor = cursor;
         this.text = text;
      }

      //Allows to input character text into the list that holds
      //text on the page.
      //Pre-Conditions: Takes in characters to input as text
      //Post-Conditions: Increments cursor position and character 
      //input is added into the ObservableCollection 'text'.
      public void inputText(char input) {
         Cursor++;
         text.Insert(Cursor, new Text { Char = input, Bold = false, Italics = false, Underline = False } );
         //Error: Unclear why I can't use Insert() or Add() for 
         //the Observable Collection
      }

      //Removes text from ObservableCollection
      //Pre-Condition: None
      //Post-Condition: Removes text from cursor position. Decrements 
      //Cursor.
      public void delText() {
         if (text.Count() != 0) {
            text.RemoveAt(Cursor);
            Cursor--;
         }
      }

      //Pre-Conditions: Takes in boolean parameter called type.
      //Post-Conditions: If true should pass the ObservableCollection<Text> 
      //to the document class. If false should take in txt file from 
      //document class and place into ObservableCollection<Text>
      public void PassText(bool type) {

      }

      //Cursor Position Getter and Setter
      public int Cursor {
         get { return cursor; }
         set {
            if (cursor != value) {
               cursor = value;
               RaisePropertyChanged("Cursor");
               RaisePropertyChanged("Text");
            }
         }
      }

      //ObservableCollection<Text> Text Getter and Setter
      public ObservableCollection<Text> Text {
         get { return text; }
         set {
            text = value;
            RaisePropertyChanged("Cursor");
            RaisePropertyChanged("Text");
         }
      }
   }
}