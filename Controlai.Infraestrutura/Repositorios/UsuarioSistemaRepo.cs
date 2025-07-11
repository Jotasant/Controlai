using Dominio.Interfaces;
using Dominio.Models;
using Infraestrutura.Data;
using MySqlConnector;


namespace Infraestrutura.Repositorio;


public class RepoUsuarioSistema : IRepoUsuarioSistema
{
    private readonly ConexaoRepo _conexaoRepo;

    public RepoUsuarioSistema(ConexaoRepo conexaoRepo)
    {
        _conexaoRepo = conexaoRepo;
    }

    
    public async void CadastrarUsuario(UsuarioSistema usuario)
    {
        if (string.IsNullOrEmpty(usuario.Usuario)) throw new Exception("Usuario Obrigatório");
        if (string.IsNullOrEmpty(usuario.Nome)) throw new Exception("Nome Obrigatório");
        if (string.IsNullOrEmpty(usuario.Email)) throw new Exception("Email Obrigatório");
        if (string.IsNullOrEmpty(usuario.SenhaHash)) throw new Exception("Senha Obrigatório");

        using var conn = _conexaoRepo.ObterConexao();
        conn.Open();

        using var cmd = new MySqlCommand("INSERT INTO UsuarioSistema (Nome, isAdministrador, Usuario, DataDeCadastro, SenhaHash, Email) VALUES (@Nome, @isAdministrador, @Usuario, @DataDeCadastro, @SenhaHash, @Email)", conn);

    }
    public async void EditarUsuario(UsuarioSistema usuario)
    {
        if (usuario.Id <= 0) throw new Exception("ID do usuário é obrigatório");

        


        // Implementação do método para editar usuário
    }
    public async void ExcluirUsuario(int id)
    {
        // Implementação do método para excluir usuário
    }
    public async Task<UsuarioSistema> ObterPorId(int id)
    {
        // Implementação do método para obter usuário por ID
        // Exemplo de inicialização, deve ser implementado corretamente 
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<List<UsuarioSistema>> ObterPorNome(string nome)
    {
         // Implementação do método para obter usuário por nome
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<List<UsuarioSistema>> ObterPorPerfil(bool isAdministrador)
    {
        // Implementação do método para obter usuário por perfil
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<UsuarioSistema> ObterPorUsuario(string usuario)
    {
        // Implementação do método para obter usuário por nome de usuário
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<List<UsuarioSistema>> ObterPorDataDeCadastro(DateTime dataDeCadastro)
    {
        // Implementação do método para obter usuário por data de cadastro
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<UsuarioSistema> ObterPorEmail(string email)
    {
        // Implementação do método para obter usuário por email
        return null; // Exemplo de retorno, deve ser implementado corretamente
    }
    public async Task<List<UsuarioSistema>> ObterTodos()
    {
        // Implementação do método para obter todos os usuários
        return new List<UsuarioSistema>(); // Exemplo de retorno, deve ser implementado corretamente
    }

}
        
