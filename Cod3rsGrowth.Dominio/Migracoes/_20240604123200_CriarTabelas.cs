﻿using FluentMigrator;

namespace Cod3rsGrowth.Dominio
{
    [Migration(20240604123200)]
    public class _20240604123200_CriarTabelas : Migration
    {
        public override void Up()
        {
            Create.Table("Cliente")
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Cpf").AsString()
                .WithColumn("Cnpj").AsString()
                .WithColumn("Tipo").AsInt32().NotNullable();

            Create.Table("Pedido")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("ClienteId").AsInt32().ForeignKey("Cliente", "Id").NotNullable()
                .WithColumn("Data").AsDateTime().NotNullable()
                .WithColumn("NumeroCartao").AsString()
                .WithColumn("Valor").AsDecimal().NotNullable()
                .WithColumn("FormaPagamento").AsInt32().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Cliente");
            Delete.Table("Pedido");
        }
    }
}
