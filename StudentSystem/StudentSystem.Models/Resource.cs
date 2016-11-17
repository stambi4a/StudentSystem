namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Url { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course {  get; set; }

        public virtual ICollection<License> Licenses
        {
            get
            {
                return this.licenses;
            }
            set
            {
                this.licenses = value;
            }
        }
    }
}
