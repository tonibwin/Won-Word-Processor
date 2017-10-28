/* TextModel.cs
 * Written by Jordan Leibman
 * Written on 10/19/2017
 * Purpose: Controls the formatting of individual text characters for bold, italics, and underline
 * TODO: unit and integration testing with PageModel.cs
 * Implications: Text is stored in a list on PageModel.cs and used occasionally for saving through the DocumentModel.cs
 */
using System.ComponentModel;

namespace Won.Model {
   public class TextModel { }
   public class Text : INotifyPropertyChanged {
      private char ch;
      private bool bold;
      private bool italics;
      private bool underline;
      
      /*Ch
       * The character stored in the Text object
       * Returns a non formatted character
       */
      public char Ch {
         get { return ch; }
         set {
            if (ch != value) ch = value;
            RaisePropertyChanged("Ch");
            RaisePropertyChanged("Text");
         }
      }

      /*Bold
       * The flag if Text is bold
       * returns if text is bolded
       */
      public bool Bold {
         get { return bold; }
         set {
            if (bold != value) bold = value;
            RaisePropertyChanged("Bold");
            RaisePropertyChanged("Text");
         }
      }

      /*Italics
       * The flag if Text is italics
       * returns if text is italicized
       */
      public bool Italics {
         get { return italics; }
         set {
            if (italics != value) italics = value;
            RaisePropertyChanged("Italics");
            RaisePropertyChanged("Text");
         }
      }

      /*Underline
       * The flag if Text is underline
       * returns if text is underlined
       */
      public bool Underline {
         get { return underline; }
         set {
            if (underline != value) underline = value;
            RaisePropertyChanged("Underline");
            RaisePropertyChanged("Text");
         }
      }

      /* ToString
       * Written by Jordan Leibman
       * returns the Text object as a string with the following format
       * Ch|Bold|Italics|Underline
       * Used as a standard format for the saving and loading of files
       */
      public string ToString() {
         return ch.ToString() + "|" + bold.ToString() + "|" + itlaics.ToString() + "|" + underline.ToString();
      }
      //Event and property handlers
      public event PropertyChangedEventHandler PropertyChanged;

      private void RaisePropertyChanged(string property) {
         if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
   }
}