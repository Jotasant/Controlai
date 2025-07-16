namespace Applicacao.Servicos;

using System;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;

public class SvcCriarUsuario
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly ISvcObterUsuario _obterUsuario;

    public SvcCriarUsuario(IRepoUsuarioSistema usuarioSistemaRepo, ISvcObterUsuario obterusuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterUsuario = obterusuario;
    }
    

    public async Task<DtoUsuarioSistema> CriarUsuario(DtoUsuarioSistema usuariodto)
    {
        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        else if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        else if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        else if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        var usuarioExistentePorNomeUsuario = await _usuarioSistemaRepo.ObterPorUsuario(usuariodto.Usuario);
        if (usuarioExistentePorNomeUsuario != null)
        {
            throw new Exception("Nome de usuário já existe. Por favor, escolha outro.");
        }

        var usuarioExistentePorEmail = await _usuarioSistemaRepo.ObterPorEmail(usuariodto.Email);
        if (usuarioExistentePorEmail != null)
        {
            throw new Exception("Email já cadastrado. Por favor, use outro email.");
        }

        //var usuarioexistente = await _usuarioSistemaRepo.ObterPorEmail(usuariodto.Email);

        var usuario = SvcTransformacao.DtoForUsuarioSistema(usuariodto);

        if (string.IsNullOrWhiteSpace(usuario.SenhaHash)) throw new Exception("Erro no processo de criação");

        usuario.SenhaHash = SvcCryptSenha.GerarHash(usuario.SenhaHash);

        //if (!SvcCryptSenha.RegexVerify(usuario.SenhaHash)) SvcCryptSenha.GerarHash(usuario.SenhaHash);
/*
                        UsuarioSistema usuario = new UsuarioSistema
                        (
                            Nome: usuariodto.Nome,
                            Usuario: usuariodto.Usuario,
                            SenhaHash: SvcCryptSenha.GerarHash(usuariodto.Senha),
                            Email: usuariodto.Email
                        )
                        {
                            isAdministrador = usuariodto.isAdministrador,
                            DataDeCadastro = usuariodto.DataDeCadastro
                        };
                */
        var usuariocriado = await _usuarioSistemaRepo.CadastrarUsuario(usuario);

        if (usuariocriado == null || usuariocriado.Id == 0) throw new Exception("Usuário não foi criado!");

        var usuarioDto = SvcTransformacao.UsuarioSistemaforDto(usuariocriado);
/*
        DtoUsuarioSistema usuarioDto = new DtoUsuarioSistema
        (
            Id: usuariocriado.Id,
            Nome: usuariocriado.Nome!,
            Usuario: usuariocriado.Usuario!,
            Email: usuariocriado.Email!
        )
        {
            isAdministrador = usuariocriado.isAdministrador,
            DataDeCadastro = usuariocriado.DataDeCadastro
        };
*/
        return usuarioDto;

        //throw new Exception("Erro de execução!");
    }
}
