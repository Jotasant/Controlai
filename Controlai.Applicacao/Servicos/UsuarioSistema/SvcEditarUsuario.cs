namespace Applicacao.Servicos;

using System;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Dominio.Interfaces;
using Dominio.Models;



public class SvcEditarUsuarioSistema
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly SvcObterUsuario _obterUsuario;


    public SvcEditarUsuarioSistema(IRepoUsuarioSistema usuarioSistemaRepo, SvcObterUsuario obterUsuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterUsuario = obterUsuario;
    }
    public async Task<UsuarioSistema> EditarUsuario(DtoUsuarioSistema usuariodto)
    {

        if (usuariodto.Usuario == null) throw new Exception("Nome de usuario Obrigatório");
        else if (usuariodto.Nome == null) throw new Exception("Nome obrigatório");
        else if (usuariodto.Email == null) throw new Exception("Email obrigatório");
        else if (usuariodto.Senha == null) throw new Exception("Senha obrigatório");

        usuariodto.Busca = Dominio.Enums.TipoDeBusca.PorId;

        List<UsuarioSistema> usuarioExistenteList = (await _obterUsuario.Obter(usuariodto)).ToList();
        var usuarioExistente = usuarioExistenteList.FirstOrDefault();
        
        if (usuarioExistente == null) throw new Exception("Usuário não encontrado");

        var usuario = SvcComparador.CompararObjetos<UsuarioSistema, DtoUsuarioSistema>(usuarioExistente, usuariodto);

        if (usuario) throw new Exception("Nenhuma alteração detectada");

        else
        {
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

            await _usuarioSistemaRepo.EditarUsuario(usuarioSistema);

            return usuarioSistema;
        }
    }        
}
