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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Messages = new HashSet<Message>();
            this.VacancyEmployees = new HashSet<VacancyEmployee>();
        }
    
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string industry { get; set; }
        public string positions { get; set; }
        public string interests { get; set; }
        public string languages { get; set; }
        public string skills { get; set; }
        public string educations { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string location { get; set; }
        public Nullable<int> hours { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string experience { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VacancyEmployee> VacancyEmployees { get; set; }
    }
}
