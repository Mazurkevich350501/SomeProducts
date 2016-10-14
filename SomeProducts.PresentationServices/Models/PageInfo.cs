

namespace SomeProducts.PresentationServices.Models
{
    public class PageInfo
    {
        private int _page;
        private int _itemsCount;
        private int _totalItemsCount;
        private string _sortingOption;

        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value < 1 ? 1 : value;
            }
        }

        public int ItemsCount
        {
            get
            {
                return _itemsCount;
            }
            set
            {
                _itemsCount = value < 0 || value > 20
                ? 5
                : value;
            }
        }

        public int TotalItemsCount {
            get
            {
                return _totalItemsCount;
            }
            set
            {
                _totalItemsCount = value < 1 ? 1 : value;
            }
        }

        public string SortingOption
        {
            get { return _sortingOption; }
            set { _sortingOption = _sortingOption ?? value; }
        }

        public PageInfo(int? page, int? itemsCount, string sortingOption)
        {
            Page = page ?? 1;
            ItemsCount = itemsCount ?? 5;
            SortingOption = sortingOption;
        }
    }
}
