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

            var usuariocriado = await _usuarioSistemaRepo.CadastrarUsuario(usuario);
            if (usuariocriado.Id == 0) throw new Exception("Usuario não foi criado!");

            DtoUsuarioSistema usuarioDto = new DtoUsuarioSistema
            (
                Id: usuariocriado.Id,
                Nome: usuariocriado.Nome!,
                Usuario: usuariocriado.Usuario!,
                Senha: usuariocriado.SenhaHash!,
                Email: usuariocriado.Email!
            )
            {
                isAdministrador = usuariocriado.isAdministrador,
                DataDeCadastro = usuariocriado.DataDeCadastro
            };
            return usuarioDto;
        }
        else
        {
            throw new Exception("Usuário existente!");
        }
    }
}
