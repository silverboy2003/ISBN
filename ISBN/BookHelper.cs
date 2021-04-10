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
        }//returns a book by it's isbn
        public static DataTable GetBookGenres(int bookID)
        {
            string sql = $"SELECT genreID FROM GenresToBooks WHERE bookID = {bookID}";
            DataTable genres = DBHelper.GetDataTable(sql);
            return genres;
        }//gets all genres for a certain book
        public static bool UpdateRating(string isbn, double rating)
        {
            string sql = $"UPDATE Books SET rating = {rating} WHERE ISBN = '{isbn}'";
            return DBHelper.WriteData(sql) == 1;
        }//updates a books rating
        public static int InsertBook(List<object> inputs, string author, string publisher, int numPages, int numChapters, DateTime releaseDate, string isbn)
        {
            string sql = $@"INSERT INTO Books (bookName, author, publisher, bookSynopsis, numPages, numChapters, bookReleaseDate, ISBN) VALUES (@Text1, '{author}', '{publisher}', @Text2, {numPages}, {numChapters}, '{releaseDate}', '{isbn}')";
            int newID = DBHelper.InsertWithAutoNumKey(sql, inputs);
            return newID;
        }//inserts new books into the ws database if it does not exist
    }
}