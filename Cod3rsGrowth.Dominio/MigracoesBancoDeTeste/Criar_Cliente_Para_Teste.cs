using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Dominio.MigracoesBancoDeTeste
{
    [Profile(ConstantesMigracao.PERFIL_POPULAR_BANCO_DE_TESTES)]
    public class MigracaoBancoDeTestes : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Sql("DELETE FROM Cliente");

            Execute.Sql("SET IDENTITY_INSERT Cliente ON");

            Execute.Sql(
                @"INSERT INTO Cliente (Id, Nome, Cpf, Cnpj, Tipo) VALUES
                (1, 'João Silva', '12345678901', '', 1),
                (2, 'Empresa A', '', '12345678000195', 2),
                (3, 'Maria Oliveira', '23456789012', '', 1),
                (4, 'Empresa B', '', '23456789000196', 2),
                (5, 'Pedro Santos', '34567890123', '', 1),
                (6, 'Indústria C', '', '34567890000197', 2),
                (7, 'Ana Costa', '45678901234', '', 1),
                (8, 'Serviços D', '', '45678901000198', 2),
                (9, 'Carlos Lima', '56789012345', '', 1),
                (10, 'Comércio E', '', '56789012000199', 2),
                (11, 'Fernanda Pereira', '67890123456', '', 1),
                (12, 'Empresa F', '', '67890123000100', 2),
                (13, 'Ricardo Almeida', '78901234567', '', 1),
                (14, 'Consultoria G', '', '78901234000101', 2),
                (15, 'Lucia Martins', '89012345678', '', 1),
                (16, 'Transportes H', '', '89012345000102', 2),
                (17, 'Juliana Rocha', '90123456789', '', 1),
                (18, 'Eletrodomésticos I', '', '90123456000103', 2),
                (19, 'Tiago Santos', '01234567890', '', 1),
                (20, 'Tecnologia J', '', '01234567000104', 2),
                (21, 'Camila Fernandes', '12345678098', '', 1),
                (22, 'Marcos Lima', '23456789109', '', 1),
                (23, 'Tatiane Souza', '34567890210', '', 1),
                (24, 'Daniela Campos', '45678901321', '', 1),
                (25, 'Roberto Castro', '56789012432', '', 1)");

            Execute.Sql("SET IDENTITY_INSERT Cliente OFF");
        }
    }
}
