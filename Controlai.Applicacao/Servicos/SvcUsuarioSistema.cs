namespace Applicacao.Servicos;

using Applicacao.DTOs;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;

public class SvcUsuarioSistema
{

    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;

    public SvcUsuarioSistema (IRepoUsuarioSistema usuarioSistemaRepo)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
    }

    public async Task<bool> AutenticarUsuario(DtoUsuarioSistema dtoUsuarioSistema)
    {
        if (string.IsNullOrWhiteSpace(dtoUsuarioSistema.Senha)) throw new Exception("Senha precisa ter conteúdo");
        dtoUsuarioSistema.Busca = TipoDeBusca.PorEmail;

        var listUsuario = await Obter(dtoUsuarioSistema);
        var usuario = listUsuario.FirstOrDefault();

        if (usuario == null) throw new Exception("Usuário não encontrado");
        if (usuario.SenhaHash == null) throw new Exception("Senha não encontrada");

        return SvcCryptSenha.VerificarHash(dtoUsuarioSistema.Senha, usuario.SenhaHash);

    }

    public void CriarUsuario(DtoUsuarioSistema usuariodto)
    {
        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        if (usuariodto != null)
        {
            UsuarioSistema usuario = new UsuarioSistema
            (
                Nome: usuariodto.Nome,
                Usuario: usuariodto.Usuario,
                SenhaHash: usuariodto.Senha,
                Email: usuariodto.Email
            )
            {
                isAdministrador = usuariodto.isAdministrador,
                DataDeCadastro = usuariodto.DataDeCadastro
            };

            _usuarioSistemaRepo.CadastrarUsuario(usuario);
        }
    }

    public void EditarUsuario(DtoUsuarioSistema usuariodto)
    {
        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        UsuarioSistema usuario = new UsuarioSistema
        (
            Nome: usuariodto.Nome,
            Usuario: usuariodto.Usuario,
            SenhaHash: usuariodto.Senha,
            Email: usuariodto.Email
        )
        {
            isAdministrador = usuariodto.isAdministrador,
            DataDeCadastro = usuariodto.DataDeCadastro
        };

        _usuarioSistemaRepo.EditarUsuario(usuario);
    }

    public void ExcluirUsuario(int id)
    {
        _usuarioSistemaRepo.ExcluirUsuario(id);
    }

public Task<List<UsuarioSistema>> Obter(DtoUsuarioSistema dto)
{
    var usuarios = new List<UsuarioSistema>();

    switch (dto.Busca)
    {
        case TipoDeBusca.PorNome:
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");
            usuarios = _usuarioSistemaRepo.ObterPorNome(dto.Nome);
            break;

        case TipoDeBusca.PorUsuario:
            if (string.IsNullOrWhiteSpace(dto.Usuario))
                throw new ArgumentException("Usuário é obrigatório.");
            var usuarioPorUsuario = _usuarioSistemaRepo.ObterPorUsuario(dto.Usuario);
            if (usuarioPorUsuario != null) usuarios.Add(usuarioPorUsuario);
            break;

        case TipoDeBusca.PorEmail:
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email é obrigatório.");
            var usuarioPorEmail = _usuarioSistemaRepo.ObterPorEmail(dto.Email);
            if (usuarioPorEmail != null) usuarios.Add(usuarioPorEmail);
            break;

        case TipoDeBusca.PorId:
            if (dto.Id <= 0)
                throw new ArgumentException("ID inválido.");
            var usuarioPorId = _usuarioSistemaRepo.ObterPorId(dto.Id);
            if (usuarioPorId != null) usuarios.Add(usuarioPorId);
            break;

        case TipoDeBusca.PorDataDeCadastro:
            if (dto.DataDeCadastro == DateTime.MinValue)
                throw new ArgumentException("Data de cadastro é obrigatória.");
            usuarios = _usuarioSistemaRepo.ObterPorDataDeCadastro(dto.DataDeCadastro.Date);
            break;

        case TipoDeBusca.PorPerfil:
            usuarios = _usuarioSistemaRepo.ObterPorPerfil(dto.isAdministrador);
            break;

        case TipoDeBusca.Todos:
            usuarios = _usuarioSistemaRepo.ObterTodos();
            break;

        default:
            throw new ArgumentException("Tipo de busca inválido.");
    }

    return Task.FromResult(usuarios);
    }
}