using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Proj2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Model.Product _product;  // Product 모델 객체
        private Repository.Repo _repo;    // Repository 객체, 데이터베이스 작업을 위한 객체

        private DataRowView _selectedProduct;  // 선택된 제품

        private string _connstring = "Server=localhost;Database=Product;User Id=project;Password=wlghks8941@;Encrypt=False;";  // DB 연결 문자열

        public MainViewModel()  // MainViewModel 생성자, 초기화 작업을 수행
        {
            _product = new Model.Product();    // Product 모델 객체 초기화
            _repo = new Repository.Repo(_connstring);  // Repository 객체 초기화

            // Command 객체 초기화 (각 버튼에 해당하는 동작 지정)
            SearchCommand = new Command.Command(FilterData);
            AddCommand = new Command.Command(AddProduct);
            UpdateCommand = new Command.Command(UpdateProduct);
            DelCommand = new Command.Command(DeleteProduct);

            LoadData();  // 데이터 로딩
        }

        public DataView DV_1    // DV_1은 제품 목록을 나타내는 DataView 속성. 변경 시 UI에 반영
        {
            get { return _product.dv_1; }
            set 
            { 
                if (_product.dv_1 != value)
                {
                    _product.dv_1 = value;   // 데이터 뷰를 새로 설정
                    OnPropertyChanged(nameof(DV_1));  // 속성 변경을 UI에 알려줌
                }
            }
        }

        private void LoadData() // LoadData() 메서드는 데이터를 DB에서 로드하여 DataView에 반영합니다.
        {
            var dataTable = _repo.GetData();  // Repository의 GetData() 메서드로 DB에서 데이터 가져오기
            if (dataTable != null)
            {
                DV_1 = dataTable.DefaultView;  // 데이터 뷰 설정
            }
        }

        public DataRowView SelectedProduct      // 선택된 제품을 나타내는 속성
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;   // 선택된 제품 변경
                OnPropertyChanged(nameof(SelectedProduct));  // 속성 변경을 UI에 알려줌
                OnPropertyChanged(nameof(SelectedProductImagePath)); // 이미지 경로도 변경된 것을 알려줌
            }
        }

        public string SearchProduct { get; set; }       // 검색할 제품명을 나타내는 속성

        // Search, Add, Update, Delete 작업을 위한 ICommand 속성들
        public ICommand SearchCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand UpdateCommand { get; set; }

        public ICommand DelCommand { get; set; }

        private void FilterData(object parameter)   // FilterData 메서드는 검색어를 바탕으로 제품 목록을 필터링합니다.
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
                    // 검색어를 포함하는 제품을 필터링 (제품명, 코드, 설명에서 검색)
                    DV_1.RowFilter = $"ProductName LIKE '%{SearchProduct}%' OR Code LIKE '%{SearchProduct}%' OR Explanation LIKE '%{SearchProduct}%' OR Brand LIKE '%{SearchProduct}%'";
                }
            }
        }

        private void AddProduct(object parameter)
        {
            // 새로운 제품 데이터를 사용하여 AddData 메서드 호출
            if (SelectedProduct != null)
            {
                string productName = SelectedProduct["ProductName"].ToString();
                string productCode = SelectedProduct["Code"].ToString();
                int quantity = int.Parse(SelectedProduct["Quantity"].ToString());
                string explanation = SelectedProduct["Explanation"].ToString();
                string brand = SelectedProduct["Brand"].ToString();
                string nowUser = SelectedProduct["NowUser"].ToString();

                // Repository의 AddtData 메서드 호출
                bool result = _repo.AddtData(productName, productCode, quantity, explanation, brand, nowUser);

                if (result)
                {
                    MessageBox.Show("제품이 추가되었습니다.");
                    LoadData(); // 데이터 로딩하여 데이터그리드 갱신
                }
                else
                {
                    MessageBox.Show("제품 추가에 실패했습니다.");
                }
            }
            else
            {
                MessageBox.Show("추가할 제품 정보를 입력해주세요.");
            }
        }

        private void UpdateProduct(object parameter)
        {
            if (SelectedProduct != null) // 데이터그리드에서 제품이 선택된 경우
            {
                string productCode = SelectedProduct["Code"].ToString(); // 제품 코드
                string productName = SelectedProduct["ProductName"].ToString(); // 제품 이름
                int quantity = int.Parse(SelectedProduct["Quantity"].ToString()); // 제품 수량
                string explanation = SelectedProduct["Explanation"].ToString(); // 제품 설명
                string brand = SelectedProduct["Brand"].ToString(); // 제품 브랜드
                string nowUser = SelectedProduct["NowUser"].ToString(); // 현재 사용자

                // Repository의 UpdateData 메서드 호출
                bool result = _repo.UpdateData(productCode, productName, quantity, explanation, brand, nowUser);

                if (result)
                {
                    MessageBox.Show("제품 정보가 수정되었습니다.");
                    LoadData(); // 데이터 로드하여 데이터그리드 갱신
                }
                else
                {
                    MessageBox.Show("제품 정보 수정에 실패했습니다.");
                }
            }
            else
            {
                MessageBox.Show("수정할 제품을 선택해주세요.");
            }
        }

        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)    // 선택된 제품이 있을 경우
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

        public string SelectedProductImagePath
        {
            get
            {
                if (SelectedProduct != null)
                {
                    string productCode = SelectedProduct["Code"].ToString();
                    string imagePath = $@"C:\Users\User\Documents\Dev\PilotProjrct2\Proj2\Proj2\Images\{productCode}.png"; // 제품 코드에 해당하는 이미지 파일 경로
                    return imagePath; // 이미지 경로 반환
                }
                return string.Empty; // 선택된 제품이 없으면 빈 문자열 반환
            }
        }


        // PropertyChanged 이벤트는 데이터 바인딩을 위해 사용
        public event PropertyChangedEventHandler? PropertyChanged;

        // PropertyChanged 이벤트 호출 메서드
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
