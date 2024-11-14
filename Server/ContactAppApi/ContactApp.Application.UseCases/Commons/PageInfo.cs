namespace ContactApp.Application.UseCases.Commons
{
    public class PagingInfo
    {
        public int EndIndex { get; set; }

        public int ItemsPerPage { get; set; }

        public int StartIndex { get; set; }

        public int TotalItemCount { get; set; }


        public PagingInfo()
        {
        }

        public PagingInfo(int pageIndex, int itemsPerPage, int totalItemCount)
        {
            EndIndex = Math.Min((pageIndex + 1) * itemsPerPage, totalItemCount) - 1;
            ItemsPerPage = itemsPerPage;
            StartIndex = pageIndex * itemsPerPage;
            TotalItemCount = totalItemCount;
        }
    }
}
