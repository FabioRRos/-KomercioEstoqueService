using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KomercioApi.Models
{
    public class ListaComprasDTO
    {
        [Key]
        [Column("id_lista")]
        public int IdListaCompra { get; set; }
        [Column("nome_lista")]
        public string? NomeDaLista { get; set; }
        [Column("data_criacao")]
        public DateTime DataCriacaoLista { get; set; }
        [Column("status_lista")]
        public bool StatusLista { get; set; }

         
        /// <summary>
        /// Valida a lista de compra (statico para não precisar instanciar).
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ServiceResponse<ListaComprasDTO> ValidaLista(ListaComprasDTO lista)
        {
            var response = new ServiceResponse<ListaComprasDTO>();


            if (lista == null) throw new ArgumentNullException("Lista invalida!");
            if (lista.NomeDaLista == null) throw new ArgumentNullException("Nome da lista invalida");
            if (lista.DataCriacaoLista == null) throw new ArgumentNullException("Data da criação invalida ou inexistente. tente novamente.");


            response.Sucesso = true;
            return response;

        }


    }
}
