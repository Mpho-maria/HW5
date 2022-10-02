using HW5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW5.Controllers
{
    public class HomeController : Controller
    {
       
        private Service DataService = new Service();
        public ActionResult Index()
        {
             
            BooksVM booksVM = new BooksVM();
            booksVM.Books = DataService.GetAllBooks();
            booksVM.Authors = DataService.GetAllAuthors();
            booksVM.Types = DataService.GetAllTypes();
            return View(booksVM);
        }

        public ActionResult BookData(int id)
        {
           
            BookDataVM bookDataVM = new BookDataVM();  
            bookDataVM.BorrowedBooks = DataService.GetAllBorowedBooks(id);
            bookDataVM.Book = DataService.GetAllBooks().Where(b => b.ID == id).FirstOrDefault();
            return View(bookDataVM);
        }
        public ActionResult Search( int type = 0, int author = 0, string name = null)
        {
            
            BooksVM booksVM = new BooksVM();
            booksVM.Books = DataService.Search(name,type, author);
            booksVM.Authors = DataService.GetAllAuthors();
            booksVM.Types = DataService.GetAllTypes();               
            return View("Index", booksVM);
        }

        public ActionResult Students(int bookId)
        {
             
            StudentVM studentVM = new StudentVM();
            List<Student> students = DataService.GetAllStudents();
            List<BorrowedBook> books = DataService.GetAllBorowedBooks(bookId);
            foreach(var student in students)
            {
                for (int i = 0; i < books.Count(); i++)
                {
                    string name = student.Name + " " + student.Surname;
                    if (books[i].StudentName == name && (books[i].BroughtDate == "" || books[i].BroughtDate == null))
                    {
                        student.Book = true;
                    }
                    else
                    {
                        student.Book = false;
                             
                    }
                }   
            }
            studentVM.Students = students;
            studentVM.Book = DataService.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            studentVM.Class = DataService.GetAllClases();
            return View(studentVM);
        }

     
        public ActionResult ReturnBook(int bookId, int studentId)
        {
            DataService.ReturnBook(bookId, studentId);

            BookDataVM bookDataVM = new BookDataVM();
            bookDataVM.BorrowedBooks = DataService.GetAllBorowedBooks(bookId);
            bookDataVM.Book = DataService.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            return View("BookData", bookDataVM);
            
        }

        
        public ActionResult BorrowBook(int bookId, int studentId)
        {
            DataService.BorrowBook(bookId, studentId);
            BookDataVM bookDataVM = new BookDataVM();
            bookDataVM.BorrowedBooks = DataService.GetAllBorowedBooks(bookId);
            bookDataVM.Book = DataService.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault();
            return View("BookData", bookDataVM);
        }
        
        public ActionResult StudentSearch(int bookId, string name = "none", string _class = "none")
        {
            
            StudentVM studentVM = new StudentVM
            {
                Students = DataService.SearchStudent(name, _class),
                Book = DataService.GetAllBooks().Where(b => b.ID == bookId).FirstOrDefault(),
                Class = DataService.GetAllClases()

            };
            return View("Students", studentVM);
        }



    }
}