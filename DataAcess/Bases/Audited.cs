using System;

namespace BlazorApp.DataAcess.Bases
{
    public abstract class Audited 
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}