namespace DocumentsCSVReader.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Type { get; set;}
        public string Date{ get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set;  }

        public string FullName => FirstName + " " + LastName;
        public Document(){}
        public Document(int id, string type, string date, string firstName, string lastName, string city)
        {
            Id = id;
            Type = type;
            Date = date;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}
