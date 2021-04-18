using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ISBN
{
    /// <summary>
    /// Summary description for ISBN
    /// </summary>
    [WebService(Namespace = "http://BullBooksWS.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ISBN : System.Web.Services.WebService
    {
        /// <summary>
        /// gets a string 'isbn' and returns null if a book with this isbn
        /// does not exists or a book object if it does exist
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        [WebMethod]
        public WSBook GetBookByISBN(string isbn)
        {
            WSBook book = WSBook.GetBookByISBN(isbn);
            return book;
        }
        /// <summary>
        /// Returns a list of all genres in the web service's database
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<WSGenre> GetGenresTable()
        {
            List<WSGenre> genres = WSGenre.GetGenresTable();
            return genres;
        }
        /// <summary>
        /// Updates the rating with double 'newRating' for book with isbn 'isbn'
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="newRating"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateRating(string isbn, double newRating)
        {
            return WSBook.UpdateBookRating(isbn, newRating);
        }
        /// <summary>
        /// creates a new wsbook object and inserts into database, if the isbn does not exist yet,
        /// as it is an index with no multiples, then a new book will be added
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="bookName"></param>
        /// <param name="author"></param>
        /// <param name="publisher"></param>
        /// <param name="synopsis"></param>
        /// <param name="numPages"></param>
        /// <param name="numChapters"></param>
        /// <param name="rating"></param>
        /// <param name="bookRelease"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddNewBook(WSBook newBook)
        {
            return newBook.InsertNewBook();
        }
    }
}
