namespace Applicacao.Servicos;

using System;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;
using System.Text.RegularExpressions;

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
        if (string.IsNullOrWhiteSpace(usuariodto.Usuario))
            throw new Exception("Nome de usuario Obrigatório");
        else if (string.IsNullOrWhiteSpace(usuariodto.Nome))
            throw new Exception("Nome obrigatório");
        else if (string.IsNullOrWhiteSpace(usuariodto.Email))
            throw new Exception("Email obrigatório");
        else if (string.IsNullOrWhiteSpace(usuariodto.Senha))
            throw new Exception("Senha obrigatória");

    
        var usuario = SvcTransformacao.DtoForUsuarioSistema(usuariodto);

        var usuarioExistentePorNomeUsuario = await _usuarioSistemaRepo.ObterPorUsuario(usuariodto.Usuario);
        if (usuarioExistentePorNomeUsuario != null)
            throw new Exception("Nome de usuário já existe. Por favor, escolha outro.");


        var usuarioExistentePorEmail = await _usuarioSistemaRepo.ObterPorEmail(usuariodto.Email);
        if (usuarioExistentePorEmail != null)
            throw new Exception("Email já cadastrado. Por favor, use outro email.");


        //var usuario = SvcTransformacao.DtoForUsuarioSistema(usuariodto);

        if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
            throw new Exception("Erro no processo de criação");

        usuario.SenhaHash = SvcCryptSenha.GerarHash(usuario.SenhaHash);


        var usuariocriado = await _usuarioSistemaRepo.CadastrarUsuario(usuario);

        if (usuariocriado == null || usuariocriado.Id == 0) throw new Exception("Usuário não foi criado!");

        var usuarioDto = SvcTransformacao.UsuarioSistemaforDto(usuariocriado);

        return usuarioDto;

    }
}
