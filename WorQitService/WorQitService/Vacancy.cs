//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorQitService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacancy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacancy()
        {
            this.VacancyEmployee = new HashSet<VacancyEmployee>();
        }
    
        public int ID { get; set; }
        public Nullable<int> employerID { get; set; }
        public string jobfunction { get; set; }
        public string description { get; set; }
        public Nullable<int> salary { get; set; }
        public Nullable<int> hours { get; set; }
        public string requirements { get; set; }
        public string tags { get; set; }
    
        public virtual Employer Employer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VacancyEmployee> VacancyEmployee { get; set; }
    }
}
