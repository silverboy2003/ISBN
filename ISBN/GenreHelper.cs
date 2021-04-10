using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ISBN
{
    public class GenreHelper
    {
        public static DataTable GetGenresTable()
        {
            string sql = "SELECT * FROM Genres";
            DataTable genres = DBHelper.GetDataTable(sql);
            return genres;
        }//gets all genres from ws database
        public static bool InsertGenres(int bookID, List<int> genres)
        {
            if (genres.Count == 0)
                return false;
            string sql = $"INSERT INTO GenresToBooks (bookID, genreID) VALUES ({bookID}, {genres[0]})";
            bool success = DBHelper.WriteData(sql) == 1;
            genres.RemoveAt(0);
            return InsertGenres(bookID, genres) || success;
        }//inserts new rows to genrestobooks while updating book 
    }
}