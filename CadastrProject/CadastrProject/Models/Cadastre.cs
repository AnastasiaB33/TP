//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CadastrProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cadastre
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Value { get; set; }
        public double Square { get; set; }
        public System.DateTime Date_application { get; set; }
        public string IDUser { get; set; }
        public int IDGroup { get; set; }
        public int IDStatus { get; set; }
        public Nullable<System.DateTime> Date_registration { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Group Group { get; set; }
        public virtual Status Status { get; set; }
    }
}
