using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ZenithCore.Models.ZenithModels
{
    public class Activity
    {
        [Key]
        [Display(Name = "Activity Id")]
        public int ActivityId { get; set; }

        [MaxLength(250)]
        [MinLength(2)]
        [Required]
        [Display(Name = "Activity Description")]
        public string ActivityDescription { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        public List<Event> Events { get; set; }

    }
}