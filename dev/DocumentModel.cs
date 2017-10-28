using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 Author: Michael Curtis
 Date: 10/20/17
*/
namespace Won.Model
{
    class DocumentModel
    { }
    class Document { 
        private String fileName;
        private ObservableCollection<PageModel> pages;
        private event EventHandler PropertyChangedHandler;

        public Document()
        {
            fileName = "";
            pages = null;
        }

        public Document(String createFileName, ObservableCollection<PageModel> pages)
        {
            this.fileName = createFileName;
            this.pages = pages;
        }

        public void save()
        {
            
        }

        public void saveAs(string newFileName)
        {
            this.fileName = newFileName;
            return this.fileName;
        }

        public void load(string fileName)
        {

        }
    }
}