using System;
using System.Collections.Generic;
/// <summary>
/// Summary description for Class1
/// </summary>
namespace WonApplication {
    public class testDoc
    {
        public const string TEST_PATH = "D:\\Users\\Zach\\Desktop\\Course_Syllabus\\Fall 2017\\CS371_Cao\\GroupWon\\Documentation\\Prototype\\MVVMProto\\Model\testTextDocs\\test1";
        public testDoc(int testCase)
        {
            switch (testCase)
            {
                case 0:
                    Document testDocument = new Document();
                    List<Page> pageHierarchy = new List<Page>();

                    Page page1 = new Page();
                    Page page2 = new Page();
                    Page page3 = new Page();
                    page1.appendTextList("This is the theoretical contents of the first page.");
                    page2.appendTextList("This is the contents of the second page. . . ");
                    page3.appendTextList("This is the contents of the third page. . . ");

                    pageHierarchy.Add(page1);
                    pageHierarchy.Add(page2);
                    pageHierarchy.Add(page3);

                    testDocument.setPageList(pageHierarchy);
                    //testDocument.Save(TEST_PATH);

                    testDocument.toString();

                    testDocument.Load(TEST_PATH + "test1.txt");

                    testDocument.toString();
                    break;

            }
        }


        static void Main(string[] args)
        {
            testDoc firstTest = new testDoc(1);
            return;
        }
    }
}
