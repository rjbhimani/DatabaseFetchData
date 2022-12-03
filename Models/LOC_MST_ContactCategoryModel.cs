using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DatabaseFetchData.Models
{
    public class LOC_MST_ContactCategoryModel
    {

        public int ContactCategoryID { get; set; }

        [Required(ErrorMessage = "Please enter ContactCategory Name")]
        [DisplayName("ContactCategory Name")]
        [StringLength(10, MinimumLength = 3)]
        public String ContactCategoryName { get; set; }

        /*[Required(ErrorMessage = "Please enter CreationDate")]*/
        public DateTime CreationDate { get; set; }

        /*[Required(ErrorMessage = "Please enter Modification")]*/
        public DateTime Modification{ get; set; }

    }

    public class LOC_MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }

        public String ContactCategoryName { get; set; }

    }
}
