namespace DatabaseFetchData.Models
{
    public class LOC_MST_ContactCategoryModel
    {

        public int ContactCategoryID { get; set; } 

        public String ContactCategoryName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Modification{ get; set; }

    }

    public class LOC_MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }

        public String ContactCategoryName { get; set; }

    }
}
