using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Serialization;
namespace ISBN
{
    [SoapInclude(typeof(WSBook))]
    [XmlInclude(typeof(WSBook))]
    [Serializable()]
    public class WSGenre
    {
        int genreID;
        string genreName;
        public int GenreID { get => genreID; set => genreID = value; }
        public string GenreName { get => genreName; set => genreName = value; }
        public WSGenre(int genreID, string genreName)
        {
            this.genreID = genreID;
            this.genreName = genreName;
        }
        public WSGenre()
        {

        }
        public static List<WSGenre> GetGenresTable()
        {
            DataTable genresTable = GenreHelper.GetGenresTable();
            if (genresTable == null)
                return null;
            List<WSGenre> allGenres = new List<WSGenre>();
            foreach(DataRow genre in genresTable.Rows)
            {
                allGenres.Add(new WSGenre(int.Parse(genre.ItemArray[0].ToString()), genre.ItemArray[1].ToString()));
            }
            return allGenres;
        }
    }
}