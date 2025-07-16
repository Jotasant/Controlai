namespace Infraestrutura.Repositorio;

using Dominio.Interfaces;
using Dominio.Models;
using Infraestrutura.Data;
using MySqlConnector;
using System.Text.RegularExpressions;


public class RepoUsuarioSistema : IRepoUsuarioSistema
{
    private readonly ConexaoRepo _conexaoRepo;

    public RepoUsuarioSistema(ConexaoRepo conexaoRepo)
    {
        _conexaoRepo = conexaoRepo;
    }


    //MAPEAR USUÁRIO
    private UsuarioSistema MapearUsuario(MySqlDataReader reader)
    {
        return new UsuarioSistema
        (
            Nome: reader["Nome"].ToString()!,
            Usuario: reader["Usuario"].ToString()!,
            SenhaHash: reader["SenhaHash"].ToString()!,
            Email: reader["Email"].ToString()!
        )
        {
            Id = Convert.ToInt32(reader["Id"]),
            isAdministrador = Convert.ToBoolean(reader["isAdministrador"]),
            DataDeCadastro = Convert.ToDateTime(reader["DataDeCadastro"])
        };
    }
    
    //CRIAR USUÁRIO

    public async Task<UsuarioSistema> CadastrarUsuario(UsuarioSistema usuario)
    {
        if (string.IsNullOrEmpty(usuario.Usuario)) throw new Exception("Usuario Obrigatório");
        if (string.IsNullOrEmpty(usuario.Nome)) throw new Exception("Nome Obrigatório");
        if (string.IsNullOrEmpty(usuario.Email)) throw new Exception("Email Obrigatório");
        if (string.IsNullOrEmpty(usuario.SenhaHash)) throw new Exception("Senha Obrigatório");

        if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        throw new ArgumentException("Email inválido");


        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(@"INSERT INTO UsuarioSistema (Nome, isAdministrador, Usuario, DataDeCadastro, SenhaHash, Email) VALUES (@Nome, @isAdministrador, @Usuario, @DataDeCadastro, @SenhaHash, @Email)", conn);

        cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
        cmd.Parameters.AddWithValue("@isAdministrador", usuario.isAdministrador);
        cmd.Parameters.AddWithValue("@Usuario", usuario.Usuario);
        cmd.Parameters.AddWithValue("@DataDeCadastro", usuario.DataDeCadastro);
        cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
        cmd.Parameters.AddWithValue("@Email", usuario.Email);
        
        int linhasAfetadas = await cmd.ExecuteNonQueryAsync();

        usuario.Id = (int)cmd.LastInsertedId;

        if (linhasAfetadas == 0)
            return null!;

        return usuario;
    }
    

    //EDITAR USUARIO

    public async Task<UsuarioSistema> EditarUsuario(UsuarioSistema usuario)
    {
        if (usuario.Id <= 0) throw new Exception("ID do usuário é obrigatório");

        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();
        int linhasAfetadas = 0;

        if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
        {
            using var cmd = new MySqlCommand(
                @"UPDATE UsuarioSistema 
            SET Nome = @Nome, isAdministrador = @isAdministrador, Usuario = @Usuario, DataDeCadastro = @DataDeCadastro, Email = @Email 
            WHERE Id = @Id", conn);


            cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@isAdministrador", usuario.isAdministrador);
            cmd.Parameters.AddWithValue("@Usuario", usuario.Usuario);
            cmd.Parameters.AddWithValue("@DataDeCadastro", usuario.DataDeCadastro);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Id", usuario.Id);

            linhasAfetadas = await cmd.ExecuteNonQueryAsync();
        }
        if (!string.IsNullOrWhiteSpace(usuario.SenhaHash))
        {
            using var cmd = new MySqlCommand(
            @"UPDATE UsuarioSistema 
            SET Nome = @Nome, isAdministrador = @isAdministrador, Usuario = @Usuario, DataDeCadastro = @DataDeCadastro, SenhaHash = @SenhaHash, Email = @Email 
            WHERE Id = @Id", conn);


            cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@isAdministrador", usuario.isAdministrador);
            cmd.Parameters.AddWithValue("@Usuario", usuario.Usuario);
            cmd.Parameters.AddWithValue("@DataDeCadastro", usuario.DataDeCadastro);
            cmd.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Id", usuario.Id);
            
            linhasAfetadas = await cmd.ExecuteNonQueryAsync();
        }

        if (linhasAfetadas == 0)
            return null!;

        return usuario;
    }


    //EXCLUIR USUÁRIO
    public async Task ExcluirUsuario(int Id)
    {
        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"DELETE FROM UsuarioSistema WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id", Id);

        int linhasAfetadas = await cmd.ExecuteNonQueryAsync();

        if (linhasAfetadas == 0)
            throw new Exception("Erro ao excluir usuário.");

    }
    

    //OBTER USUÁRIO POR ID
    public async Task<UsuarioSistema> ObterPorId(int Id)
    {
        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE Id = @Id", conn);

        cmd.Parameters.AddWithValue("@Id", Id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var usuario = MapearUsuario(reader);

            return usuario;
        }

        return null!;
    }

    //OBTER USUÁRIO POR NOME

    public async Task<List<UsuarioSistema>> ObterPorNome(string nome)
    {
        List<UsuarioSistema> listaUsuario = new List<UsuarioSistema>();

        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE Nome LIKE @Nome", conn);

        cmd.Parameters.AddWithValue("@Nome", $"%{nome}%");

        using var reader = await cmd.ExecuteReaderAsync();


        while (await reader.ReadAsync())
        {
            listaUsuario.Add(MapearUsuario(reader));
        }

        if (!listaUsuario.Any())
            return null!;

        return listaUsuario;
    }


    //OBTER USUÁRIO POR PERFIL

    public async Task<List<UsuarioSistema>> ObterPorPerfil(bool isAdministrador)
    {
        List<UsuarioSistema> listaUsuario = new List<UsuarioSistema>();

        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE isAdministrador = @isAdministrador", conn);

        cmd.Parameters.AddWithValue("@isAdministrador", isAdministrador);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            listaUsuario.Add(MapearUsuario(reader));
        }

        if (!listaUsuario.Any())
            return null!;

        return listaUsuario;
    }

    //OBTER USUÁRIO POR USUARIO

    public async Task<UsuarioSistema> ObterPorUsuario(string usuario)
    {
        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE Usuario = @Usuario", conn);

        cmd.Parameters.AddWithValue("@Usuario", usuario);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var usuarioreader = MapearUsuario(reader);

            return usuarioreader;
        }

        return null!;
    }

    //OBTER USUÁRIO POR DATA DE CADASTRO

    public async Task<List<UsuarioSistema>> ObterPorDataDeCadastro(DateTime DataDeCadastro)
    {
        List<UsuarioSistema> listaUsuario = new List<UsuarioSistema>();

        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE DataDeCadastro = @DataDeCadastro", conn);

        cmd.Parameters.AddWithValue("@DataDeCadastro", DataDeCadastro);

        using var reader = await cmd.ExecuteReaderAsync();


        while (await reader.ReadAsync())
        {
            listaUsuario.Add(MapearUsuario(reader));
        }

        if (!listaUsuario.Any())
            return null!;


        return listaUsuario;
    }
    
    //OBTER USUÁRIO POR EMAIL

    public async Task<UsuarioSistema> ObterPorEmail(string email)
    {
        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();
        
    

        using var cmd = new MySqlCommand(
            @"SELECT * FROM UsuarioSistema
            WHERE Email = @Email", conn);

        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var usuario = MapearUsuario(reader);

            return usuario;
        }

        return null!;
    }
    

    //OBTER TODOS OS USUÁRIOS

    public async Task<List<UsuarioSistema>> ObterTodos()
    {
        var listaUsuario = new List<UsuarioSistema>();

        using var conn = _conexaoRepo.ObterConexao();
        await conn.OpenAsync();

        using var cmd = new MySqlCommand("SELECT * FROM UsuarioSistema", conn);
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            listaUsuario.Add(MapearUsuario(reader));
        }

        if (!listaUsuario.Any())
            return null!;

        return listaUsuario;
    }

}
        
