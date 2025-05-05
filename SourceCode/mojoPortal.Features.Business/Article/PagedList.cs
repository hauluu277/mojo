using System.Collections.Generic;

namespace mojoPortal.Business.WebHelpers
{
    public class PagedList<T>
    {
        private List<T> listItem = new List<T>();
        private int pageSize = 15;
        private int pageIndex = 1;
        private int totalPage = 1;
        private int totalCount = 0;
        public List<T> ListItem
        {
            get { return listItem; }
            set { listItem = value; }
        }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        public int TotalPage
        {
            get { return totalPage; }
            set { totalPage = value; }
        }
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }
    }
}
