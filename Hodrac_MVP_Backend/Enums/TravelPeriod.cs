using System.ComponentModel.DataAnnotations;

namespace Hodrac_MVP_Backend.Enums
{
    public enum TravelPeriod
    {
        [Display(Name = "Jan - Mar")]
        JanToMarch,

        [Display(Name = "April - Jun")]
        AprToJun,

        [Display(Name = "July - Sept")]
        JulToSep,

        [Display(Name = "Oct - Dec")]
        OctToDec,

        [Display(Name = "Year Round")]
        YearRound,

        [Display(Name = "Event Based")]
        EventBased,
    }
}
