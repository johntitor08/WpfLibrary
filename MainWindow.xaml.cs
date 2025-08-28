using System.Windows;
using WpfLibrary.Data;
using WpfLibrary.Models;

namespace WpfLibrary
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context = new AppDbContext();
        private Book? selectedBook;

        public MainWindow()
        {
            InitializeComponent();
            _context.Database.EnsureCreated(); // DB oluştur
            LoadData();
        }

        private void LoadData()
        {
            BooksGrid.ItemsSource = _context.Books.OrderByDescending(b => b.Id).ToList();
            selectedBook = null;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var book = new Book
            {
                Title = TxtTitle.Text.Trim(),
                Author = TxtAuthor.Text.Trim(),
                Category = TxtCategory.Text.Trim(),
                IsRead = ChkRead.IsChecked ?? false,
                IsFavorite = ChkFavorite.IsChecked ?? false
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            ClearForm();
            LoadData();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook == null)
            {
                MessageBox.Show("Önce bir kayıt seçin.");
                return;
            }

            selectedBook.Title = TxtTitle.Text.Trim();
            selectedBook.Author = TxtAuthor.Text.Trim();
            selectedBook.Category = TxtCategory.Text.Trim();
            selectedBook.IsRead = ChkRead.IsChecked ?? false;
            selectedBook.IsFavorite = ChkFavorite.IsChecked ?? false;

            _context.SaveChanges();
            ClearForm();
            LoadData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBook == null)
            {
                MessageBox.Show("Önce bir kayıt seçin.");
                return;
            }

            _context.Books.Remove(selectedBook);
            _context.SaveChanges();
            ClearForm();
            LoadData();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedBook = null;
            TxtTitle.Text = "";
            TxtAuthor.Text = "";
            TxtCategory.Text = "";
            ChkRead.IsChecked = false;
            ChkFavorite.IsChecked = false;
            BooksGrid.SelectedIndex = -1;
        }

        private void BooksGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BooksGrid.SelectedItem is Book book)
            {
                selectedBook = book;
                TxtTitle.Text = book.Title;
                TxtAuthor.Text = book.Author;
                TxtCategory.Text = book.Category;
                ChkRead.IsChecked = book.IsRead;
                ChkFavorite.IsChecked = book.IsFavorite;
            }
        }
    }
}
