using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DatabaseFetchData.Models
{
    public class LOC_StateModel
    {

        public int? StateID { get; set; }

        [Required(ErrorMessage = "Please enter State Name")]
        [DisplayName("State Name")]
        [StringLength(10, MinimumLength = 3)]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please enter State Code")]
        [DisplayName("State Code")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public int CountryID { get; set; }

        /*[Required(ErrorMessage = "Please enter CreationDate")]*/
        public DateTime CreationDate { get; set; }

        /*[Required(ErrorMessage = "Please enter ModificationDate")]*/
        public DateTime ModificationDate { get; set; }
    }

    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }

        public String StateName { get; set; }
    }
    
}


