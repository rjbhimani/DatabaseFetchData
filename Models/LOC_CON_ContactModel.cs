using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DatabaseFetchData.Models
{
    public class LOC_CON_ContactModel
    {

        public int? ContactID { get; set; }

        [Required(ErrorMessage = "Please enter Contact Name")]
        [DisplayName("Contact Name")]
        [StringLength(10, MinimumLength = 3)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Please enter ContactCategory")]
        public int ContactCategoryID { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter PinCode")]
        public int PinCode { get; set; }

        [Required(ErrorMessage = "Please enter Mobile")]
        public int Mobile { get; set; }

        [Required(ErrorMessage = "Please enter AlternetContact")]
        public int AlternetContact { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter BirthDate")]
        public DateTime BirthDate { get; set; }


        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Insta { get; set; }

        /*[Required(ErrorMessage = "Please enter CreationDate")]*/
        public DateTime CreationDate { get; set; }

        /*[Required(ErrorMessage = "Please enter ModificationDate")]*/
        public DateTime ModificationDate { get; set; }

    }
}
