//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ALFMovers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransTruck
    {
        public int TransID { get; set; }
        public string TruckPlateNo { get; set; }
        public int TransTruckID { get; set; }
    
        public virtual Transaction Transaction { get; set; }
        public virtual Truck Truck { get; set; }
    }
}