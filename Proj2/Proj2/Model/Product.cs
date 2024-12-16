using System.ComponentModel;
using System.Data;

namespace Proj2.Model
{
    public class Product : INotifyPropertyChanged
    {
        public DataView dv_1
        {
            get; set;
        }

        public string _productName { get; set; }  // 제품 이름

        public string _code { get; set; }         // 제품 코드

        public int _quantity { get; set; }    // 제품 수량

        public string _explanation { get; set; }  // 제품 설명

        public string _brand { get; set; }        // 제품 브랜드

        public string _nowUser { get; set; }      // 현재 사용자

        public string ProductName   // 제품 이름 속성
        {
            get => _productName;
            set
            {
                _productName = value;   // 제품 이름 변경
                OnPropertyChanged(nameof(ProductName));     // 속성 변경을 UI에 알려줌
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
