using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Proj2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Model.Product _product;

        private Repository.Repo _repo;

        private string _productName;
        
        private string _code;

        private int _quantity;

        private string _explanation;

        private string _brand;

        private string _nowUser;

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
            //AddCommand = new Command.Command(AddProduct);
            UpdateCommand = new Command.Command(UpdateProduct);
            DelCommand = new Command.Command(DeleteProduct);

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

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string Explanation
        {
            get => _explanation;
            set
            {
                _explanation = value;
                OnPropertyChanged(nameof(Explanation));
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        public string NowUser
        {
            get => _nowUser;
            set
            {
                _nowUser = value;
                OnPropertyChanged(nameof(NowUser));
            }
        }

        //public string SelectedImage
        //{
        //    get => _seletedImage;
        //    set
        //    {
        //        _seletedImage = value;
        //        OnPropertyChanged(nameof(SelectedImage));
        //        UpdateImage();
        //    }
        //}

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

        private void AddProduct(object parameter)
        {
            // 입력된 값 확인
            //MessageBox.Show($"ProductName: {ProductName}, Code: {Code}, Quantity: {Quantity}, Explanation: {Explanation}, Brand: {Brand}, NowUser: {NowUser}");

            //if (string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(Code) || Quantity <= 0)
            //{
            //    MessageBox.Show("모든 필드를 정확히 입력해주세요.");
            //    return;
            //}

            //bool result = _repo.AddData(ProductName, Code, Quantity, Explanation, Brand, NowUser);
            //if (result)
            //{
            //    MessageBox.Show("제품이 추가되었습니다.");
            //    LoadData();  // 데이터 갱신
            //}
            //else
            //{
            //    MessageBox.Show("데이터 추가에 실패했습니다.");
            //}
        }

        private void UpdateProduct(object parameter)
        {
            
        }


        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                var productCode = SelectedProduct["Code"].ToString();  // 삭제할 제품의 고유 코드 가져오기

                bool result = _repo.DeleteData(productCode); // Repository의 DeleteData 메서드를 호출하여 DB에서 삭제

                if (result)
                {
                    MessageBox.Show("제품이 삭제되었습니다.");
                    LoadData(); // 데이터를 다시 로드하여 UI에 반영
                }
                else
                {
                    MessageBox.Show("데이터 삭제에 실패했습니다.");
                }
            }
            else
            {
                MessageBox.Show("삭제할 제품을 선택해주세요.");
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
