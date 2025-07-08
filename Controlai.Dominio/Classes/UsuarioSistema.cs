namespace Dominio.Models;

public class UsuarioSistema
{
    public DateTime DataDeNascimento { get; set; }
    public string? Condominio { get; set; }
    public string? bloco { get; set; }
    public string? apartamento { get; set; }
    public string? Rua { get; set; }
    public string? NumeroResidencial { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }
    public string? Whatsapp { get; set; }
    public string? Telefone { get; set; }
    public string? Instagram { get; set; }
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public int Id { get; set; } // ID do usuário
    public string? Nome { get; set; }  // Nome completo do usuário
    public bool isCliente { get; set; } // Indica se o usuário é um cliente
    public bool isTecnico { get; set; } // Indica se o usuário é um técnico
    public bool isAdministrador { get; set; } // Perfil do usuário: Administrador, Usuário Comum, etc.
    public string? Usuario { get; set; } // Nome de usuário
    public DateTime DataDeCadastro { get; set; } // Data de cadastro do usuário
    public string? SenhaHash { get; set; } // Senha do usuário
    public string? Email { get; set; } // Email do usuário
    private UsuarioSistema() { }

    public UsuarioSistema(string Nome, string Usuario, string Rua, string NumeroResidencial, string Bairro, string Cidade, string Estado, string CEP, string Email = "" ) 
    {
        this.Nome = Nome;
        this.Usuario = Usuario;
        this.Rua = Rua;
        this.NumeroResidencial = NumeroResidencial;
        this.Bairro = Bairro;
        this.Cidade = Cidade;
        this.Estado = Estado;
        this.CEP = CEP;
        this.Email = Email;


        DataDeCadastro = DateTime.Now;

        SenhaHash = string.Empty; // Inicializa SenhaHash como vazio
    }
    public UsuarioSistema(string Nome, string Usuario, string SenhaHash, string Email)
    {
        this.Nome = Nome;
        this.Usuario = Usuario;
        this.Email = Email;

        DataDeCadastro = DateTime.Now;

        this.SenhaHash = SenhaHash;
    }
}
