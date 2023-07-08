using System.ComponentModel.DataAnnotations.Schema;

namespace DevagramCSharp.Models
{
    public class Seguidor
    {
        //criar propriedades para seguir
        public int Id { get; set; }
        public int? IdUsuarioSeguidor { get; set; }
        public int? IdUsuarioSeguido { get; set; }

        //somente o moldel pode usar o construtor
        [ForeignKey("IdUsuarioSeguidor")]
        public virtual Usuario UsuarioSeguidor { get; private set; }

        [ForeignKey("IdUsuarioSeguido")]
        public virtual Usuario UsuarioSeguido { get; private set; }


    }
}
