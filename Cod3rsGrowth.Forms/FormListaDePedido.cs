﻿using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cod3rsGrowth.Forms
{

    public partial class FormListaDePedido : Form
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly int _clienteId;
        public FormListaDePedido(ServicoPedido servicoPedido, int clienteId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;

            InitializeComponent();
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, clienteId);
        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            using (FormAdicionarPedido novoPedido = new FormAdicionarPedido(_servicoPedido, _clienteId) { })
            {
                if (novoPedido.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId);
                }
            }
        }

        private void dataGridViewPedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewPedido.Columns[Constantes.COLUNA_VALOR_TABELA_PEDIDO].DefaultCellStyle.Format = Constantes.DUAS_CASAS_APOS_VIRGULA;
            if (e.ColumnIndex == 3)
            {
                if (e.Value is string && e.Value != string.Empty)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 4) + " " + valor.Substring(4, 4) + " " + valor.Substring(8, 4) + " " + valor.Substring(12, 4);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}