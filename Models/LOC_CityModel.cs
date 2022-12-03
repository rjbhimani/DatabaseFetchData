using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DatabaseFetchData.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required(ErrorMessage = "Please enter City Name")]
        [DisplayName("City Name")]
        [StringLength(10, MinimumLength = 3)]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please enter PinCode")]
        public int PinCode { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        public int StateID { get; set; }

        /*[Required(ErrorMessage = "Please enter CreationDate")]*/
        public DateTime CreationDate { get; set; }

        /*[Required(ErrorMessage = "Please enter ModificationDate")]*/
        public DateTime ModificationDate { get; set; }

    }

    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }

        public String CityName { get; set; }
    }

}
