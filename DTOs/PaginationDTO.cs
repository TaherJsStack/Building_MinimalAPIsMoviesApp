namespace Building_MinimalAPIsMoviesApp.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int recordesPerPage { get; set; } = 10;
        public int recordesPerPageMax { get; set; } = 50;

        public int RecordesPerPage {
            get { return recordesPerPage; } 
            set 
            {
                if (value > recordesPerPageMax)
                { 
                    recordesPerPage = recordesPerPageMax;
                }
                else
                {
                    recordesPerPage = value;
                }
            } 
        }


    }
}
