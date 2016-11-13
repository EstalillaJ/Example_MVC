using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Configuration;
using System.Web.Configuration;
using Example_MVC.Models;
namespace Example_MVC
{
    public class Database
    {

        private static readonly string NEW_POST = "INSERT INTO MESSAGES VALUES (@author, @message);";
        private static readonly string LOAD_FORUM = "SELECT * FROM MESSAGES;";
        private static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public static void SavePost(string author, string message)
        {
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(NEW_POST, connection);

                // Adding strings this way will make sure they are safe from SQL Injection
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@message", message);

                command.ExecuteNonQuery();
            }
        }

        public static Forum LoadForum()
        {
        
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(LOAD_FORUM, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                int authorColumn = reader.GetOrdinal("Author");
                int messageColumn = reader.GetOrdinal("Message");

                Forum forum = new Forum()
                {
                    Posts = new List<Post>()
                };

                while (reader.Read())
                {
                    forum.Posts.Add(
                        new Post
                        {
                            Author = reader.GetString(authorColumn),
                            Message = reader.GetString(messageColumn)
                        }
                    );
                }

                reader.Close();

                return forum;
            }
        }
    }
}