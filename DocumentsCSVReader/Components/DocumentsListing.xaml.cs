using DocumentsCSVReader.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DocumentsCSVReader.Components
{

    public partial class DocumentsListing : UserControl
    {
        private List<Document> _documents;
        private List<DocumentItem> _documentItems;
        private List<DocumentItem> _selectedOrderItems;
        
        public DocumentsListing()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Document)DocumentsListView.SelectedItem;

            if (selectedItem != null)
            {
                int selectedId= selectedItem.Id;
                _selectedOrderItems = _documentItems.Where(item =>item.DocumentId == selectedId).ToList();
                UpdateSelectedDocuemntInfo(selectedItem);
                UpdateDocumentItemsListView(_selectedOrderItems);
            }
        }


        public void ReceiveData(List<Document> documents, List<DocumentItem> documentItems)
        {
            
            _documents = documents;
            _documentItems = documentItems;
            DocumentsListView.ItemsSource = documents;

        }

        public void UpdateDocumentItemsListView(List<DocumentItem> documentItems)
        {
            DocumentItemsListView.ItemsSource = null;
            _selectedOrderItems = documentItems;
            DocumentItemsListView.ItemsSource= _selectedOrderItems;
        }

        private void UpdateSelectedDocuemntInfo(Document selectedItem)
        {
            TypeText.Text = selectedItem.Type;
            DateText.Text = selectedItem.Date;
            CityText.Text = selectedItem.City;
            ClientNameText.Text = selectedItem.FullName;
        }


    }
}
