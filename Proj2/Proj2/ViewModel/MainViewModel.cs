using System.ComponentModel;
using System.Data;
using System.Timers;
using System.Windows.Input;

namespace Proj2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Model.Product _product;

        private Repository.Repo _repo;

        private string _seletedImage;

        private DataRowView _selectedProduct;

        private string _connstring = "Server=localhost;Database=Product;User Id=project;Password=wlghks8941@;Encrypt=False;";

        public MainViewModel()
        {
            _product = new Model.Product();
            _repo = new Repository.Repo(_connstring);

            // 초기 이미지 설정
            // SelectedImage = "No_Picture";

            SearchCommand = new Command.Command(FilterData);

            LoadData();
        }

        public DataView DV_1
        {
            get { return _product.dv_1; }
            set 
            { 
                if (_product.dv_1 != value)
                {
                    _product.dv_1 = value;
                    OnPropertyChanged(nameof(DV_1));
                }
            }
        }

        private void LoadData()
        {
            var dataTable = _repo.GetData();
            if (dataTable != null)
            {
                DV_1 = dataTable.DefaultView;
            }
        }

        public DataRowView SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                UpdateImage();
            }
        }

        public string SelectedImage
        {
            get => _seletedImage;
            set
            {
                _seletedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
                UpdateImage();
            }
        }

        private void UpdateImage()
        {
            //if (SelectedImage != null)
            //{
            //    var imagePath = SelectedProduct["ImagePath"]?.ToString();
            //    if (!string.IsNullOrEmpty(imagePath))
            //    {
            //        SelectedImage = imagePath;
            //    }
            //    else
            //    {
            //        SelectedImage = "No_Picture.png";
            //    }
            //}
            //else
            //{
            //    SelectedImage = "No_Picture.png";
            //}
        }

        public string SearchProduct { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand UpdateCommand { get; set; }

        public ICommand DelCommand { get; set; }

        private void FilterData(object parameter)
        {
            if (DV_1 != null)
            {
                // SearchText가 비어 있으면 모든 데이터를 표시
                if (string.IsNullOrWhiteSpace(SearchProduct))
                {
                    DV_1.RowFilter = string.Empty;
                }
                else
                {
                    // ProductName, Code, Explanation 등의 열을 검색
                    DV_1.RowFilter = $"ProductName LIKE '%{SearchProduct}%' OR Code LIKE '%{SearchProduct}%' OR Explanation LIKE '%{SearchProduct}%'";
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // PropertyChanged 이벤트 호출 메서드
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
