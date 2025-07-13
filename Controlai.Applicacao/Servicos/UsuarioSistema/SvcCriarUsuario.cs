namespace Applicacao.Servicos;

using System;
using Applicacao.DTOs;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;

public class SvcCriarUsuario
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly SvcObterUsuario _obterUsuario;

    public SvcCriarUsuario(IRepoUsuarioSistema usuarioSistemaRepo, SvcObterUsuario obterusuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterUsuario = obterusuario;
    }
    public async Task CriarUsuario(DtoUsuarioSistema usuariodto)
    {
        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        else if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        else if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        else if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        usuariodto.Busca = TipoDeBusca.PorQualquer;

        var usuarioexistente = await _obterUsuario.Obter(usuariodto);

        if (usuarioexistente == null)
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

            await _usuarioSistemaRepo.CadastrarUsuario(usuario);
        }
        else
        {
            throw new Exception("Usuário existente!");
        }
    }
}
