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
        [WebMethod]
        public WSBook GetBookByISBN(string isbn)
        {
            WSBook book = WSBook.GetBookByISBN(isbn);
            return book;
        }
        [WebMethod]
        public List<WSGenre> GetGenresTable()
        {
            List<WSGenre> genres = WSGenre.GetGenresTable();
            return genres;
        }
        [WebMethod]
        public bool UpdateRating(string isbn, double newRating)
        {
            return WSBook.UpdateBookRating(isbn, newRating);
        }
        [WebMethod]
        public bool AddNewBook(string isbn, string bookName, string author, string publisher, string synopsis, int numPages, int numChapters, double rating, DateTime bookRelease, int[] genres)
        {
            return (new WSBook(isbn, bookName, author, publisher, synopsis, numPages, numChapters, rating, bookRelease, genres.ToList())).InsertNewBook();
        }
    }
}
