namespace Applicacao.Servicos;

using System;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using System.Text.RegularExpressions;




public class SvcEditarUsuario
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly ISvcObterUsuario _obterUsuario;


    public SvcEditarUsuario(IRepoUsuarioSistema usuarioSistemaRepo, ISvcObterUsuario obterUsuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterUsuario = obterUsuario;
    }


    public async Task<DtoUsuarioSistema> EditarUsuario(DtoUsuarioSistema usuariodto)
    {

        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        else if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        else if (usuariodto.Email == null) throw new Exception("Email obrigatório");

        bool MsmSenha = default;

        usuariodto.Busca = Dominio.Enums.TipoDeBusca.PorId;

        List<DtoUsuarioSistema> usuarioExistenteList = (await _obterUsuario.Obter(usuariodto)).ToList();
        var usuarioExistente = usuarioExistenteList.FirstOrDefault();

        if (usuarioExistente == null) throw new Exception("Usuário não encontrado");
        
        if (!string.IsNullOrWhiteSpace(usuariodto.Senha))
        {
            MsmSenha = SvcCryptSenha.VerificarHash(usuariodto.Senha, usuarioExistente.Senha!);
            if (MsmSenha)
                usuariodto.Senha = "";
        }

        if (!MsmSenha && !string.IsNullOrWhiteSpace(usuariodto.Senha))
            usuariodto.Senha = SvcCryptSenha.GerarHash(usuariodto.Senha);
        


        var usuario = SvcComparador.CompararObjetos(usuarioExistente, usuariodto);

        if (usuario) throw new Exception("Nenhuma alteração detectada");

        var usuarioSistema = SvcTransformacao.DtoForUsuarioSistema(usuariodto);


        /*
                                if (!(!MsmSenha && string.IsNullOrWhiteSpace(usuariodto.Senha)))
                                {
                                    usuariodto.Senha = SvcCryptSenha.GerarHash(usuariodto.Senha);

                                    UsuarioSistema usuarioSistema = new UsuarioSistema
                                        (
                                        Nome: usuariodto.Nome,
                                        Usuario: usuariodto.Usuario,
                                        Email: usuariodto.Email
                                        )
                                    {

                                        isAdministrador = usuariodto.isAdministrador,
                                        DataDeCadastro = usuariodto.DataDeCadastro,
                                        Id = usuariodto.Id,
                                    };
                                }
                        *//*
                                if (string.IsNullOrWhiteSpace(usuariodto.Senha))
                                {
                                    usuariodto.Senha = SvcCryptSenha.GerarHash(usuariodto.Senha);

                                    UsuarioSistema usuarioSistema = new UsuarioSistema
                                        (
                                        Nome: usuariodto.Nome,
                                        Usuario: usuariodto.Usuario,
                                        Email: usuariodto.Email
                                        )
                                    {

                                        isAdministrador = usuariodto.isAdministrador,
                                        DataDeCadastro = usuariodto.DataDeCadastro,
                                        Id = usuariodto.Id,
                                    };
                                }  
                        */
        var usuarioeditado = await _usuarioSistemaRepo.EditarUsuario(usuarioSistema);
        var dtoUsuarioSistema = SvcTransformacao.UsuarioSistemaforDto(usuarioeditado);
        /*
                        DtoUsuarioSistema dtoUsuarioSistema = new DtoUsuarioSistema
                            (
                            Id: usuarioeditado.Id,
                            Nome: usuarioeditado.Nome!,
                            Usuario: usuarioeditado.Usuario!,
                            Email: usuarioeditado.Email!
                            )
                        {
                            isAdministrador = usuarioeditado.isAdministrador,
                            DataDeCadastro = usuarioeditado.DataDeCadastro,
                            Id = usuariodto.Id
                        };
                */
        return dtoUsuarioSistema;
    }
}        

