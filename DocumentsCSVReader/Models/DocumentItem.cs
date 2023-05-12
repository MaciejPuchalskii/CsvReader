namespace DocumentsCSVReader.Models
{
    public class DocumentItem
    {
        public int DocumentId { get; set; }
        public int Ordinal { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }  
        public double Price { get; set;}
        public int TaxRate { get; set; }
        public DocumentItem() { }
        public DocumentItem(int documentId, int ordinal, string product, int quantity, double price, int taxRate)
        {
            DocumentId = documentId;
            Ordinal = ordinal;
            Product = product;
            Quantity = quantity;
            Price = price;
            TaxRate = taxRate;
        }
    }
}
