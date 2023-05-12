using DocumentsCSVReader.Models;
using DocumentsCSVReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Data;


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
            if (myConnnection.State != ConnectionState.Open)
            {
                myConnnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnnection.State != ConnectionState.Closed)
            {
                myConnnection.Close();
            }
        }


        public void SaveDocuments(IEnumerable<Document> documents)
        {
            OpenConnection();

            string insertQuery = "INSERT INTO Documents (Id,Type, Date, FirstName, LastName, City) VALUES (@Id,@Type, @Date, @FirstName, @LastName, @City)";
            SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, myConnnection);
            foreach (var document in documents)
            {
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

            string insertQuery = "INSERT INTO DocumentItems (DocumentId, Ordinal, Product,Quantity,Price,TaxRate) VALUES (@DocumentId, @Ordinal, @Product,@Quantity,@Price,@TaxRate)";
            SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, myConnnection);
            foreach (var documentItem in documentItems)
            {
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
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, myConnnection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();

            List<Document> documents = new List<Document>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string type = Convert.ToString(reader["Type"]);
                string date = Convert.ToString(reader["Date"]);
                string firstName = Convert.ToString(reader["FirstName"]);
                string lastName = Convert.ToString(reader["LastName"]);
                string city = Convert.ToString(reader["City"]);



                Document document = new Document(id, type, date, firstName, lastName, city);
             
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
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, myConnnection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();

            List<DocumentItem> documentItems = new List<DocumentItem>();
            while (reader.Read())
            {
                int documentId = Convert.ToInt32(reader["DocumentId"]);
                int ordinal = Convert.ToInt32(reader["Ordinal"]);
                string productName = Convert.ToString(reader["Product"]);
                int quantity = Convert.ToInt32(reader["Quantity"]);
                double price = Convert.ToDouble(reader["Price"]);
                int taxRate = Convert.ToInt32(reader["TaxRate"]);


                DocumentItem documentItem = new DocumentItem(documentId,ordinal,productName,quantity, price, taxRate);
                documentItems.Add(documentItem);
            }

            reader.Close();
            CloseConnection();

            return documentItems;
        }

       

    }
}
