using KomercioApi.Models;
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



        public static ServiceResponse<ItensListaComprasDTO>ValidaItemListaCompras(ItensListaComprasDTO itemDaLista)
        {
            var retorno = new ServiceResponse<ItensListaComprasDTO>();


            if (itemDaLista.IdLista <= 0)
            {
                throw new ArgumentException("Id invalido, nulo ou inexistente");
            }
            if (itemDaLista.DescricaoProduto == null) throw new ArgumentNullException("Descrição invalida");
            if (itemDaLista.CodBar == null) throw new ArgumentNullException("Código de barras invalido!");
            if (itemDaLista.Quantidade <= 0) throw new ArgumentException("Quantidade invalida!");

            retorno.Sucesso = true;
            retorno.Mensagem = "ok";

            return retorno;
            
        }
    }
}



