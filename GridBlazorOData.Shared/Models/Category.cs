//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace GridBlazorOData.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public partial class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public byte[] Picture { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
