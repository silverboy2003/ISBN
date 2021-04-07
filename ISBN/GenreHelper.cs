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
        }
    }
}