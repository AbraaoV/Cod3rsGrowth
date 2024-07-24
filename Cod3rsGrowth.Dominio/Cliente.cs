using System.ComponentModel;
using System.Reflection;
using LinqToDB.Mapping;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace Cod3rsGrowth.Dominio
{
    [Table]
    public class Cliente
    {
        [Column(Length = 250)]
        public string? Nome { get; set; }
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column(Length = 11)]
        public string? Cpf { get; set; }
        [Column(Length = 14)]
        public string? Cnpj { get; set; }
        [Column]
        public TipoDeCliente Tipo { get; set; }
        public enum TipoDeCliente 
        {
            [Description("Pessoa Física")]
            Fisica = 1,
            [Description("Pessoa Jurídica")]
            Juridica = 2
        }
        public static string capturarDescricaoEnum(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])field.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
