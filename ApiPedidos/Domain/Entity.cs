using System.ComponentModel.DataAnnotations;

namespace ApiPedidos.Domain
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public string EditedBy { get; set; }

        public DateTime EditedOn { get; set; }
    }
}
