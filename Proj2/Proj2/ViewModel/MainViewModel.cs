using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Proj2.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Model.Product _product;  // Product 모델 객체, 제품 관련 데이터를 다루는 객체
        private Repository.Repo _repo;    // Repository 객체, 데이터베이스 작업을 처리하는 객체

        private DataRowView _selectedProduct;  // 선택된 제품 (UI에서 선택된 데이터)

        private string _connstring = "Server=localhost;Database=Product;User Id=project;Password=wlghks8941@;Encrypt=False;";  // DB 연결 문자열

        // MainViewModel 생성자. ViewModel 초기화 및 데이터 로딩, 커맨드 바인딩을 설정
        public MainViewModel()
        {
            _product = new Model.Product();    // Product 모델 객체 초기화
            _repo = new Repository.Repo(_connstring);  // Repository 객체 초기화

            // 각 버튼에 해당하는 커맨드를 초기화하고 해당 메서드 연결
            SearchCommand = new Command.Command(FilterData);
            AddCommand = new Command.Command(AddProduct);
            UpdateCommand = new Command.Command(UpdateProduct);
            DelCommand = new Command.Command(DeleteProduct);

            LoadData();  // 애플리케이션 실행 시 데이터 로딩
        }

        // 제품 목록을 나타내는 DataView 속성 (UI와 바인딩)
        public DataView DV_1
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

        // 데이터를 DB에서 로드하는 메서드
        private void LoadData()
        {
            var dataTable = _repo.GetData();  // Repository의 GetData() 메서드로 DB에서 데이터 가져오기
            if (dataTable != null)
            {
                DV_1 = dataTable.DefaultView;  // 데이터 뷰 설정하여 UI에 반영
            }
        }

        // 선택된 제품을 나타내는 속성 (UI에서 선택된 제품을 표시)
        public DataRowView SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;   
                OnPropertyChanged(nameof(SelectedProduct));  
                OnPropertyChanged(nameof(SelectedProductImagePath)); 
            }
        }

        // 검색할 제품명을 나타내는 속성 (사용자가 검색할 제품명 입력)
        public string SearchProduct { get; set; }

        // Command 속성들 (각 버튼에 해당하는 동작을 처리하는 커맨드)
        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DelCommand { get; set; }

        // 제품 목록을 필터링하는 메서드 (SearchProduct로 검색)
        private void FilterData(object parameter)
        {
            if (DV_1 != null)
            {
                // SearchProduct가 비어 있으면 모든 데이터를 표시
                if (string.IsNullOrWhiteSpace(SearchProduct))
                {
                    DV_1.RowFilter = string.Empty;  // 필터를 해제하여 모든 제품 표시
                }
                else
                {
                    // SearchProduct와 일치하는 제품을 필터링 (제품명, 코드, 설명에서 검색)
                    DV_1.RowFilter = $"ProductName LIKE '%{SearchProduct}%' OR Code LIKE '%{SearchProduct}%' OR Explanation LIKE '%{SearchProduct}%' OR Brand LIKE '%{SearchProduct}%'";
                }
            }
        }

        // 제품 추가 메서드 (새로운 제품을 DB에 추가)
        private void AddProduct(object parameter)
        {
            if (SelectedProduct != null)  // 선택된 제품이 있을 때
            {
                string productName = SelectedProduct["ProductName"].ToString();
                string productCode = SelectedProduct["Code"].ToString();
                int quantity = int.Parse(SelectedProduct["Quantity"].ToString());
                string explanation = SelectedProduct["Explanation"].ToString();
                string brand = SelectedProduct["Brand"].ToString();
                string nowUser = SelectedProduct["NowUser"].ToString();

                // Repository의 AddtData 메서드 호출하여 제품 추가
                bool result = _repo.AddtData(productName, productCode, quantity, explanation, brand, nowUser);

                if (result)
                {
                    MessageBox.Show("제품이 추가되었습니다.");
                    LoadData();  // 데이터를 다시 로드하여 UI 업데이트
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

        // 제품 수정 메서드 (선택된 제품의 정보를 DB에서 수정)
        private void UpdateProduct(object parameter)
        {
            if (SelectedProduct != null)  // 데이터그리드에서 제품이 선택된 경우
            {
                string productCode = SelectedProduct["Code"].ToString(); // 제품 코드
                string productName = SelectedProduct["ProductName"].ToString(); // 제품 이름
                int quantity = int.Parse(SelectedProduct["Quantity"].ToString()); // 수량
                string explanation = SelectedProduct["Explanation"].ToString(); // 설명
                string brand = SelectedProduct["Brand"].ToString(); // 브랜드
                string nowUser = SelectedProduct["NowUser"].ToString(); // 사용자

                // Repository의 UpdateData 메서드를 호출하여 DB에서 제품 수정
                bool result = _repo.UpdateData(productCode, productName, quantity, explanation, brand, nowUser);

                if (result)
                {
                    MessageBox.Show("제품 정보가 수정되었습니다.");
                    LoadData();  // 데이터를 다시 로드하여 UI 업데이트
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

        // 제품 삭제 메서드 (선택된 제품을 DB에서 삭제)
        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)  // 선택된 제품이 있을 경우
            {
                var productCode = SelectedProduct["Code"].ToString();  // 삭제할 제품의 고유 코드

                bool result = _repo.DeleteData(productCode);  // Repository의 DeleteData 메서드를 호출하여 DB에서 제품 삭제

                if (result)
                {
                    MessageBox.Show("제품이 삭제되었습니다.");
                    LoadData();  // 데이터를 다시 로드하여 UI 업데이트
                }
                else
                {
                    MessageBox.Show("제품 삭제에 실패했습니다.");
                }
            }
            else
            {
                MessageBox.Show("삭제할 제품을 선택해주세요.");
            }
        }

        // 선택된 제품에 해당하는 이미지 파일 경로 반환
        public string SelectedProductImagePath
        {
            get
            {
                if (SelectedProduct != null)
                {
                    string productCode = SelectedProduct["Code"].ToString();
                    string imagePath = $@"C:\Users\User\Documents\Dev\PilotProjrct2\Proj2\Proj2\Images\{productCode}.png";  // 제품 코드에 해당하는 이미지 경로
                    return imagePath;  // 이미지 경로 반환
                }
                return string.Empty;  // 선택된 제품이 없으면 빈 문자열 반환
            }
        }

        // PropertyChanged 이벤트는 데이터 바인딩을 위해 사용
        public event PropertyChangedEventHandler? PropertyChanged;

        // PropertyChanged 이벤트 호출 메서드 (UI에 속성 변경을 알리기 위한 메서드)
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
