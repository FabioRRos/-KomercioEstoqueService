using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KomercioApi
{
    public class ItensListaComprasDTO
    { 
        [Key]
        [Column("id_item_compra")]
        public int IdItemCompra { get; set; }
        [Column("id_lista")]
        public int IdLista { get; set; }
        [Column("descricao_produto")]
        public string? DescricaoProduto { get; set; }
        [Column("codbar")]
        public string? CodBar { get; set; }
        [Column("quantidade")]
        public int Quantidade { get; set; }
        [Column("status_item")]
        public bool StatusItem { get; set; }
        [Column("obs")]
        public string? Obs { get; set; }
    }
}



