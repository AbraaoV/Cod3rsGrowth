﻿using FluentMigrator;

namespace Cod3rsGrowth.Dominio.Migracoes
{
    [Migration(20240606135200)]
    public class _20240606135200_Editar_Coluna_Cpf_Cpnj_e_Cartao : Migration
    {
        public override void Up()
        {
            Alter.Table("Cliente")
                .AlterColumn("Cpf").AsString().Nullable()
                .AlterColumn("Cnpj").AsString().Nullable();

            Alter.Table("Pedido")
                .AlterColumn("NumeroCartao").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Cliente");
            Delete.Table("Pedido");
        }
    }
}
