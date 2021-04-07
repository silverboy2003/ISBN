using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ISBN
{
    public class BookHelper
    {
        public static DataRow GetBookByISBN(string isbn)
        {
            string sql = $"SELECT bookID, ISBN, bookName, author, publisher, bookSynopsis, numPages, numChapters, bookReleaseDate FROM Books WHERE ISBN = '{isbn}'";
            DataTable bookTable =  DBHelper.GetDataTable(sql);
            if (bookTable.Rows.Count != 0)
                return bookTable.Rows[0];
            return null;
        }
        public static DataTable GetBookGenres(int bookID)
        {
            string sql = $"SELECT genreID FROM GenresToBooks WHERE bookID = {bookID}";
            DataTable genres = DBHelper.GetDataTable(sql);
            return genres;
        }
    }
}