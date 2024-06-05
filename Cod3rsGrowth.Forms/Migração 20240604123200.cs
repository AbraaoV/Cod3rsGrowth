using FluentMigrator;

namespace Cod3rsGrowth.Forms
{
    [Migration(20240604123200)]
    public class AdicionarTabelas : Migration
    {
        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Nome").AsString()
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Cpf").AsString()
                .WithColumn("Cnpj").AsString()
                .WithColumn("Tipo").AsInt32();

            Create.Table("Pedido")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("ClienteId").AsInt32().ForeignKey("Cliente", "Id")
                .WithColumn("Data").AsDateTime()
                .WithColumn("NumeroCartao").AsString()
                .WithColumn("Valor").AsDecimal()
                .WithColumn("FormaPagamento").AsInt32();
        }
        public override void Down()
        {
            Delete.Table("Cliente");
            Delete.Table("Pedido");
        }
    }
}
