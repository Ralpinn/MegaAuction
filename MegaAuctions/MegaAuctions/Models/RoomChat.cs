//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MegaAuctions.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomChat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomChat()
        {
            this.Chats = new HashSet<Chat>();
        }
    
        public int idroom { get; set; }
        public Nullable<int> idUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual User User { get; set; }
    }
}
