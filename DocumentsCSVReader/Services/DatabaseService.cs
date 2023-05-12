using DocumentsCSVReader.Models;
using DocumentsCSVReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;


namespace DocumentsCSVReader.Services
{
    public class DatabaseService : IDatabaseService
    {
        public SQLiteConnection myConnnection;

      
        public DatabaseService()
        {
            if (!File.Exists("././Documentsdb"))
            {
                myConnnection = new SQLiteConnection("Data Source=Documents.db");
            }
        }

        public void OpenConnection()
        {
            if (myConnnection.State != System.Data.ConnectionState.Open)
            {
                myConnnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnnection.State != System.Data.ConnectionState.Closed)
            {
                myConnnection.Close();
            }
        }


        public void SaveDocuments(IEnumerable<Document> documents)
        {
            OpenConnection();

            string insertQuery = "INSERT INTO Documents (Id,Type, Date, FirstName, LastName, City) VALUES (@Id,@Type, @Date, @FirstName, @LastName, @City)";
            foreach (var document in documents)
            {
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, myConnnection);
                insertCommand.Parameters.AddWithValue("@Id", document.Id);
                insertCommand.Parameters.AddWithValue("@Type", document.Type);
                insertCommand.Parameters.AddWithValue("@Date", document.Date);
                insertCommand.Parameters.AddWithValue("@FirstName", document.FirstName);
                insertCommand.Parameters.AddWithValue("@LastName", document.LastName);
                insertCommand.Parameters.AddWithValue("@City", document.City);
                insertCommand.ExecuteNonQuery();
            }

            CloseConnection();

        }

        public void SaveDocumentItems(IEnumerable<DocumentItem> documentItems)
        {
            OpenConnection();

            foreach (var documentItem in documentItems)
            {
                string insertQuery = "INSERT INTO DocumentItems (DocumentId, Ordinal, Product,Quantity,Price,TaxRate) VALUES (@DocumentId, @Ordinal, @Product,@Quantity,@Price,@TaxRate)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, myConnnection);
                insertCommand.Parameters.AddWithValue("@DocumentId", documentItem.DocumentId);
                insertCommand.Parameters.AddWithValue("@Ordinal", documentItem.Ordinal);
                insertCommand.Parameters.AddWithValue("@Product", documentItem.Product);
                insertCommand.Parameters.AddWithValue("@Quantity", documentItem.Quantity);
                insertCommand.Parameters.AddWithValue("@Price", documentItem.Price);
                insertCommand.Parameters.AddWithValue("@TaxRate", documentItem.TaxRate);
                insertCommand.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public IEnumerable<Document> GetDocuments()
        {
            OpenConnection();

            string selectQuery = "SELECT * FROM Documents";
            SQLiteCommand command = new SQLiteCommand(selectQuery, myConnnection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<Document> documents = new List<Document>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string Type = Convert.ToString(reader["Type"]);
                string Date = Convert.ToString(reader["Date"]);
                string FirstName = Convert.ToString(reader["FirstName"]);
                string LastName = Convert.ToString(reader["LastName"]);
                string City = Convert.ToString(reader["City"]);



                Document document = new Document(id, Type, Date, FirstName, LastName,City);
             
                documents.Add(document);
            }

            reader.Close();
            CloseConnection();

            return documents;
        }

        public IEnumerable<DocumentItem> GetDocumentItems()
        {
            OpenConnection();

            string selectQuery = "SELECT * FROM DocumentItems";
            SQLiteCommand command = new SQLiteCommand(selectQuery, myConnnection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<DocumentItem> documentItems = new List<DocumentItem>();
            while (reader.Read())
            {
                int DocumentId = Convert.ToInt32(reader["DocumentId"]);
                int Ordinal = Convert.ToInt32(reader["Ordinal"]);
                string ProductName = Convert.ToString(reader["Product"]);
                int Quantity = Convert.ToInt32(reader["Quantity"]);
                double Price = Convert.ToDouble(reader["Price"]);
                int TaxRate = Convert.ToInt32(reader["TaxRate"]);


                DocumentItem documentItem = new DocumentItem(DocumentId,Ordinal,ProductName,Quantity, Price, TaxRate);
                documentItems.Add(documentItem);
            }

            reader.Close();
            CloseConnection();

            return documentItems;
        }

       

    }
}
