using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Xml.Serialization;

namespace ISBN
{
    [SoapInclude(typeof(WSBook))]
    [XmlInclude(typeof(WSBook))]
    [Serializable()]
    public class WSBook
    {

        private int id;
        private string isbn;
        private string bookName;
        private string author;
        private string publisher;
        private string synopsis;
        private int numPages;
        private int numChapters;
        private DateTime bookRelease;
        private List<int> genres;
        public int Id { get => id; set => id = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string BookName { get => bookName; set => bookName = value; }
        public string Author { get => author; set => author = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public string Synopsis { get => synopsis; set => synopsis = value; }
        public int NumPages { get => numPages; set => numPages = value; }
        public int NumChapters { get => numChapters; set => numChapters = value; }
        public DateTime BookRelease { get => bookRelease; set => bookRelease = value; }
        public List<int> Genres { get => genres; set => genres = value; }
        public WSBook()
        {
            genres = new List<int>();
        }
        public WSBook(DataRow book)
        {
            id = int.Parse(book.ItemArray[0].ToString());
            isbn = book.ItemArray[1].ToString();
            bookName = book.ItemArray[2].ToString();
            author = book.ItemArray[3].ToString();
            publisher = book.ItemArray[4].ToString();
            synopsis = book.ItemArray[5].ToString();
            numPages = int.Parse(book.ItemArray[6].ToString());
            NumChapters = int.Parse(book.ItemArray[7].ToString());
            bookRelease = DateTime.Parse(book.ItemArray[8].ToString());
            genres = new List<int>();
            LoadGenres();
        }

        public static WSBook GetBookByISBN(string isbn)
        {
            DataRow bookRow = BookHelper.GetBookByISBN(isbn);
            if(bookRow == null)
                return null;
            WSBook newBook = new WSBook(bookRow);
            return newBook;
        }
        private bool LoadGenres()
        {
            DataTable genres = BookHelper.GetBookGenres(Id);
            if (genres == null)
                return false;
            foreach(DataRow genre in genres.Rows)
            {
                this.genres.Add(int.Parse(genre.ItemArray[0].ToString()));
            }
            return true;
        }
    }
}