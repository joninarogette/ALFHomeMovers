//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ALFMovers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Truck
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truck()
        {
            this.TransTrucks = new HashSet<TransTruck>();
        }
    
        public string TruckPlateNo { get; set; }
        public string TruckModel { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<System.DateTime> TruckAdded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransTruck> TransTrucks { get; set; }
    }
}
