//Text.cs
//Controls the Text features of the Won Word Processor
//Last Updated by Jordan Leibman
//Last Updated on 10/9/2017
using System.ComponentModel;

namespace Won.Model {
   public class TextModel { }
   public class Text : INotifyPropertyChanged {
      private char ch;
      private bool bold;
      private bool italics;
      private bool underline;
      
      public char Ch {
         get { return ch; }
         set {
            if (ch != value) ch = value;
            RaisePropertyChanged("Ch");
            RaisePropertyChanged("Text");
         }
      }

      public bool Bold {
         get { return bold; }
         set {
            if (bold != value) bold = value;
            RaisePropertyChanged("Bold");
            RaisePropertyChanged("Text");
         }
      }

      public bool Italics {
         get { return italics; }
         set {
            if (italics != value) italics = value;
            RaisePropertyChanged("Italics");
            RaisePropertyChanged("Text");
         }
      }

      public bool Underline {
         get { return underline; }
         set {
            if (underline != value) underline = value;
            RaisePropertyChanged("Underline");
            RaisePropertyChanged("Text");
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      private void RaisePropertyChanged(string property) {
         if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
   }
}