namespace Applicacao.Servicos;

using System;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;



public class SvcEditarUsuarioSistema
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly ISvcObterUsuario _obterUsuario;


    public SvcEditarUsuarioSistema(IRepoUsuarioSistema usuarioSistemaRepo, ISvcObterUsuario obterUsuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterUsuario = obterUsuario;
    }
    
    
    public async Task<DtoUsuarioSistema> EditarUsuario(DtoUsuarioSistema usuariodto)
    {

        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        else if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        else if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        else if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        usuariodto.Busca = Dominio.Enums.TipoDeBusca.PorId;

        List<DtoUsuarioSistema> usuarioExistenteList = (await _obterUsuario.Obter(usuariodto)).ToList();
        var usuarioExistente = usuarioExistenteList.FirstOrDefault();

        if (usuarioExistente == null) throw new Exception("Usuário não encontrado");

        var usuario = SvcComparador.CompararObjetos(usuarioExistente, usuariodto);

        if (usuario) throw new Exception("Nenhuma alteração detectada");

        UsuarioSistema usuarioSistema = new UsuarioSistema
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

        var usuarioeditado = await _usuarioSistemaRepo.EditarUsuario(usuarioSistema);

        DtoUsuarioSistema dtoUsuarioSistema = new DtoUsuarioSistema
            (
            Id: usuarioeditado.Id,
            Nome: usuarioeditado.Nome!,
            Usuario: usuarioeditado.Usuario!,
            Senha: usuarioeditado.SenhaHash!,
            Email: usuarioeditado.Email!
            )
        {
            isAdministrador = usuarioeditado.isAdministrador,
            DataDeCadastro = usuarioeditado.DataDeCadastro
        };

        return dtoUsuarioSistema;
    }
}        

