using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WonApplication{
    class Document{
       private String fileName;
       private List<Page> pages;

       /*
        * Author: Zachary Lowery
        * Date: 10/19/2017
        * Description: This is a constructor for the Document class.
        * Pre-Condition: There must be enough memory for the OS to instantiate another object.
        * Post-Condition: Creates a Document object with a filename assigned to the empty string.
        *                 And a List of Page objects assigned to null.
        */
       public Document(){
           fileName = "";
           pages = null;
       }


       /*
        * Author: Zachary Lowery
        * Date: 10/19/2017
        * Description: This is a constructor for the Document class. This version allows the 
        * caller to assign values to the object instance using a parameter list.
        * Pre-Condition: There must be enough memory for the OS to instantiate another object.
        * Post-Condition: Creates a Document object with a filename assigned to whatever was passed to the 
        *                 'initFileName' parameter.
        *                 And a List of Page objects assigned to the 'initPagesList' parameter.
        */
       public Document(String initFileName, List<Page> initPagesList){
           fileName = initFileName;
           pages = initPagesList;
       }


        /*
        * Author: Zachary Lowery
        * Date: 10/19/2017
        * Description: This is a method for the Document class. This function saves the Document object instance
        * to the disk, this includes accurately saving the member variables contained within, such as the Page List in 
        * addition to the text contained in each page object. This data needs to be stored in a manner that allows the 
        * load function to correctly display it to the screen. But that intermediate representation has not yet been defined.
        * 
        * Implications: This function works closely with the load() function, so both should be rigorously tested together. 
        * 
        * Pre-Condition: The path specified must be accessible by the program (i.e. the hard drive must not be damaged, or 
        *                there must be sufficient privileges to access the file.) 
        * Post-Condition: The file is saved to the disk in a (so far) undefined manner.
        *  
        * Running-Time: Undefined.
        */
        public void save(string fileNameString){
            //TODO: Save the contents of the document to disk, this includes all page objects and text objects.
            fileName = fileNameString;
            return;
       }


        /*
         * Author: Zachary Lowery
         * Date: 10/19/2017
         * Description: This is a method for the Document class. This function saves the Document object instance
         * to the disk, this includes accurately saving the member variables contained within, such as the Page List in 
         * addition to the text contained in each page object. This data needs to be stored in a manner that allows the 
         * load function to correctly display it to the screen. But that intermediate representation has not yet been defined.
         * This version differs from save in that it always prompts the user for a new path, either through a console or through a GUI.
         * 
         * Implications: This function works closely with the load() function, so both should be rigorously tested together. 
         * 
         * Pre-Condition: The path specified must be accessible by the program (i.e. the hard drive must not be damaged, or 
         *                there must be sufficient privileges to access the file.) The GUI used must be usable enough to facilitate
         *                accurate specification of a file path by the user.
         *                
         * Post-Condition: The file is saved to the disk in a (so far) undefined manner.
         *  
         * Running-Time: Undefined.
         */
        public void saveAs(string fileNameString){
            //TODO: Similiar to save, only it always prompts the user for a new place to save the information.
            fileName = fileNameString;
            return;
       }


        /*
         * Author: Zachary Lowery
         * Date: 10/19/2017
         * Description: This is a method for the Document class. This function loads a document into memory so the user can view/edit
         * the file in the console or GUI. The file is synthesized from an intermediate representation provided by the save() and saveAs()
         * functions. 
         * 
         * Implications: This function works closely with the save() and saveAs() functions, so load should be rigorously tested with each seperately.
         *               This function performs its function by synthesizing an intermediate representation generated by the save() and saveAs() functions.
         *               Ideally both save() and saveAs() will use the same or similiar intermediate representations, unless for unforseen reasons doing so would 
         *               interfere with the Post-Condition of either function.
         * 
         * Pre-Condition: The path specified must be accessible by the program (i.e. the hard drive must not be damaged, or 
         *                there must be sufficient privileges to access the file.) The filepath specified must be valid.
         * Post-Condition: The file is loaded into memory and constructed according to an undefined intermediate representation.
         *  
         * Running-Time: Undefined.
         */
        public void load(string fileNameString){
            //TODO: Implement a scheme where the information is loaded from the disk and properly displayed in the Word
            //Processor.
        }
    }
}
