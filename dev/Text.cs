//Text.cs
//Controls the Text features of the Won Word Processor
//Last Updated by Jordan Leibman
//Last Updated on 10/9/2017
using System;
namespace WonApplication{
   class Text{
      //Instance Variables
      private char c; //Text character
      private bool bold; //Text Bold Setting (True = on, False = off)
      private bool italics; //Text Italics Setting (True = on, False = off)
      private bool underline; //Text Underline Setting (True = on, False = off)

      //Constructors
      //Character Constructor
      //Sets the character parameter only
      //Precondition: none;
      //Postcondition: Text object with c = cIn, bold = false, italics = false, and underline = false
      public Text(char cIn){
         setText(cIn);
         setBold(false);
         setItalics(false);
         setUnderline(false);
      }
      
      //Full Constructor
      //Sets the character and formatting parameters
      //Precondition: none
      //Postcondition: Text object with c = cIn, bold = bIn, italics = iIn, underline = uIn
      public Text(char cIn, bool bIn, bool iIn, bool uIn){
         setText(cIn);
         setBold(bIn);
         setItalics(iIn);
         setUnderline(uIn);
      }
      //End Constructors

      //Mutators
      //setText
      //Sets the c component of a Text object
      //Precondition: cIn is a valid character
      //Postcondition: c = cIn
      public void setText(char cIn) {
         c = cIn;
      }

      //setBold
      //Sets the bold component of a Text object
      //Preconditon: bIn is valid boolean expression
      //Postcondtion: bold = bIn
      public void setBold(bool bIn) {
         bold = bIn;
      }

      //setItalics
      //Sets the italics component of a Text object
      //Precondition: iIn is a valid boolean expression
      //Postcondition: italics = iIn
      public void setItalics(bool iIn) {
         italics = iIn;
      }

      //setUnderline
      //Sets the underline component of a Text object
      //Precondition: uIn is a valid boolean expression
      //Postcondition: underline = uIn
      public void setUnderline(bool uIn) {
         underline = uIn;
      }
      //End Mutators

      //Accessors
      //getText
      //Returns c
      public char getText() { return c;  }

      //getBold
      //returns bold
      public bool getBold() { return bold; }

      //getItalics
      //return italics
      public bool getItalics() { return italics; }

      //getUnderline
      //return underline
      public bool getUnderline() { return underline; }


      //ToString
      //Returns Text Object as a string with the following format
      //"c b i u"
      //where c is the character in c, and b i u are the boolean values as "true" or "false" repectively
      public override String ToString(){
         return c.ToString() + " " + bold.ToString() + " " + italics.ToString() + " " + underline.ToString();
      }
      //End Accessors

      //testing method
      static void Main(string [] args) {
         Console.WriteLine("Starting Test");
         Text t1 = new Text('a');
         Console.WriteLine(t1.ToString());
         t1.setBold(true);
         t1.setItalics(true);
         t1.setUnderline(true);
         t1.setText('t');
         Console.WriteLine(t1.ToString());
         Text t2 = new Text('s', true, false, false);
         Console.WriteLine(t2.getText());
         Console.WriteLine(t2.getBold());
         Console.WriteLine(t2.getItalics());
         Console.WriteLine(t2.getUnderline());
         Console.WriteLine("Terminating Test");
      }
   }
}