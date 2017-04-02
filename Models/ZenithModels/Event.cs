using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ZenithCore.Models.ZenithModels
{
    public class Event : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [Display(Name = "From")]
        public DateTime EventFrom { get; set; }
        [Display(Name = "To")]
        public DateTime EventTo { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }
        [Display(Name = "Activity Id")]
        public int ActivityId { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        public Activity Activity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EventTo < EventFrom)
            {
                yield return
                  new ValidationResult(errorMessage: "End Date/Time must be after Start Date/Time",
                                       memberNames: new[] { "EventTo" });
            }
        }
    }

    public class EventApi
    {

        public DateTime EventFrom { get; set; }
        public DateTime EventTo { get; set; }
        public string EventToFrom { get; set; }

        public bool IsActive { get; set; }
        public String ActivityDescription { get; set; }
    }
}