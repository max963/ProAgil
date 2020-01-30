using System.Collections.Generic;

namespace ProAgil.WebAPI.Models
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Minicurriculo { get; set; }
        public string ImgUrl { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestrantesEvento { get; set; }
    }
}