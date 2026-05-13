using FullstackTestCS2.model;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace FullstackTestCS2
{
    public partial class MainWindow : Window
    {
        private readonly HelpDeskContext _context;
        
        // Objetos para controle de edição (selecionados no grid)
        private Cliente? _clienteSelecionado;
        private Tecnico? _tecnicoSelecionado;
        private Equipamento? _equipamentoSelecionado;
        private Chamado? _chamadoSelecionado;

        public MainWindow(HelpDeskContext context)
        {
            InitializeComponent();
            _context = context;
            AtualizarTodosGrids();
        }

        private void AtualizarTodosGrids()
        {
            DgClientes.ItemsSource = _context.Clientes.ToList();
            DgTecnicos.ItemsSource = _context.Tecnicos.ToList();
            DgEquipamentos.ItemsSource = _context.Equipamentos.ToList();
            DgChamados.ItemsSource = _context.Chamados.ToList();
        }

        #region CRUD CLIENTE
        private void BtnClienteSalvar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = _clienteSelecionado ?? new Cliente();
            cliente.Nome = TxtClienteNome.Text;
            cliente.Email = TxtClienteEmail.Text;
            cliente.Empresa = TxtClienteEmpresa.Text;
            cliente.Telefone = TxtClienteTelefone.Text;

            if (cliente.Id == 0) _context.Clientes.Add(cliente);
            _context.SaveChanges();
            BtnClienteLimpar_Click(sender, e);
        }

        private void BtnClienteBuscar_Click(object sender, RoutedEventArgs e)
        {
            string busca = TxtClienteBusca.Text;
            IQueryable<Cliente> query = _context.Clientes;

            if (int.TryParse(busca, out int id))
            {
                DgClientes.ItemsSource = query.Where(c => c.Id == id).ToList();
            }
            else
            {
                DgClientes.ItemsSource = query.Where(c => c.Nome.Contains(busca) || c.Empresa.Contains(busca)).ToList();
            }
        }

        private void BtnClienteExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (_clienteSelecionado != null)
            {
                _context.Clientes.Remove(_clienteSelecionado);
                _context.SaveChanges();
                BtnClienteLimpar_Click(sender, e);
            }
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _clienteSelecionado = DgClientes.SelectedItem as Cliente;
            if (_clienteSelecionado != null)
            {
                TxtClienteNome.Text = _clienteSelecionado.Nome;
                TxtClienteEmail.Text = _clienteSelecionado.Email;
                TxtClienteEmpresa.Text = _clienteSelecionado.Empresa;
                TxtClienteTelefone.Text = _clienteSelecionado.Telefone;
            }
        }

        private void BtnClienteLimpar_Click(object sender, RoutedEventArgs e)
        {
            _clienteSelecionado = null;
            TxtClienteNome.Clear(); TxtClienteEmail.Clear(); 
            TxtClienteEmpresa.Clear(); TxtClienteTelefone.Clear(); TxtClienteBusca.Clear();
            DgClientes.ItemsSource = _context.Clientes.ToList();
        }
        #endregion

        #region LÓGICA SIMILAR PARA TÉCNICOS (Resumida para brevidade)
        private void BtnTecnicoSalvar_Click(object sender, RoutedEventArgs e)
        {
            var t = _tecnicoSelecionado ?? new Tecnico();
            t.Nome = TxtTecnicoNome.Text;
            t.Especialidade = TxtTecnicoEspecialidade.Text;
            t.Email = TxtTecnicoEmail.Text;
            t.Disponivel = ChkTecnicoDisponivel.IsChecked ?? false;

            if (t.Id == 0) _context.Tecnicos.Add(t);
            _context.SaveChanges();
            AtualizarTodosGrids();
        }

        private void BtnTecnicoBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtTecnicoBusca.Text, out int id))
                DgTecnicos.ItemsSource = _context.Tecnicos.Where(x => x.Id == id).ToList();
            else
                DgTecnicos.ItemsSource = _context.Tecnicos.Where(x => x.Nome.Contains(TxtTecnicoBusca.Text)).ToList();
        }

        private void DgTecnicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _tecnicoSelecionado = DgTecnicos.SelectedItem as Tecnico;
            if (_tecnicoSelecionado != null)
            {
                TxtTecnicoNome.Text = _tecnicoSelecionado.Nome;
                TxtTecnicoEmail.Text = _tecnicoSelecionado.Email;
                TxtTecnicoEspecialidade.Text = _tecnicoSelecionado.Especialidade;
                ChkTecnicoDisponivel.IsChecked = _tecnicoSelecionado.Disponivel;
            }
        }
        
        private void BtnTecnicoExcluir_Click(object sender, RoutedEventArgs e)
        {
             if (_tecnicoSelecionado != null) { _context.Tecnicos.Remove(_tecnicoSelecionado); _context.SaveChanges(); AtualizarTodosGrids(); }
        }
        #endregion

        #region LÓGICA EQUIPAMENTOS E CHAMADOS
        private void BtnEquipamentoSalvar_Click(object sender, RoutedEventArgs e)
        {
            var eq = _equipamentoSelecionado ?? new Equipamento();
            eq.Patrimonio = TxtEquipPatrimonio.Text;
            eq.Modelo = TxtEquipModelo.Text;
            eq.Tipo = TxtEquipTipo.Text;
            eq.Status = TxtEquipStatus.Text;
            if (eq.Id == 0) _context.Equipamentos.Add(eq);
            _context.SaveChanges();
            AtualizarTodosGrids();
        }

        private void BtnEquipamentoBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtEquipamentoBusca.Text, out int id))
                DgEquipamentos.ItemsSource = _context.Equipamentos.Where(x => x.Id == id).ToList();
            else
                DgEquipamentos.ItemsSource = _context.Equipamentos.Where(x => x.Patrimonio.Contains(TxtEquipamentoBusca.Text)).ToList();
        }

        private void DgEquipamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _equipamentoSelecionado = DgEquipamentos.SelectedItem as Equipamento;
            if (_equipamentoSelecionado != null) {
                TxtEquipPatrimonio.Text = _equipamentoSelecionado.Patrimonio;
                TxtEquipModelo.Text = _equipamentoSelecionado.Modelo;
                TxtEquipTipo.Text = _equipamentoSelecionado.Tipo;
                TxtEquipStatus.Text = _equipamentoSelecionado.Status;
            }
        }

        private void BtnChamadoSalvar_Click(object sender, RoutedEventArgs e)
        {
            var ch = _chamadoSelecionado ?? new Chamado();
            ch.Titulo = TxtChamadoTitulo.Text;
            ch.Descricao = TxtChamadoDesc.Text;
            ch.Status = TxtChamadoStatus.Text;
            ch.NomeCliente = TxtChamadoCliente.Text;
            if (ch.Id == 0) _context.Chamados.Add(ch);
            _context.SaveChanges();
            AtualizarTodosGrids();
        }

        private void BtnChamadoBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtChamadoBusca.Text, out int id))
                DgChamados.ItemsSource = _context.Chamados.Where(x => x.Id == id).ToList();
            else
                DgChamados.ItemsSource = _context.Chamados.Where(x => x.Titulo.Contains(TxtChamadoBusca.Text)).ToList();
        }

        private void DgChamados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _chamadoSelecionado = DgChamados.SelectedItem as Chamado;
            if (_chamadoSelecionado != null) {
                TxtChamadoTitulo.Text = _chamadoSelecionado.Titulo;
                TxtChamadoDesc.Text = _chamadoSelecionado.Descricao;
                TxtChamadoStatus.Text = _chamadoSelecionado.Status;
                TxtChamadoCliente.Text = _chamadoSelecionado.NomeCliente;
            }
        }
        #endregion

        // Extras para UX
        private void DgClientes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => DgClientes_SelectionChanged(sender, null!);
    }
}